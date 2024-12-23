using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MiviaMaui.Models;
using MiviaMaui.Interfaces;
using MiviaMaui.Dtos;
using System.Diagnostics;
using MiviaMaui.Bus;
using MiviaMaui.Queries;
using MiviaMaui.Commands;
using MiviaMaui.Handlers;

namespace MiviaMaui.Services
{
    public class DirectoryWatcherService : IDirectoryWatcherService, IDisposable
    {
        private readonly DirectoryService _directoryService;
        private readonly Dictionary<int, FileSystemWatcher> _watchers;
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IImagePathService _imagePathService;

        public DirectoryWatcherService(
        DirectoryService directoryService,
        ICommandBus commandBus,
        IQueryBus queryBus,
        IImagePathService imagePathService)
        {
            _directoryService = directoryService;
            _commandBus = commandBus;
            _queryBus = queryBus;
            _watchers = new Dictionary<int, FileSystemWatcher>();
            InitializeWatchers();
            _imagePathService = imagePathService;
        }

        private void InitializeWatchers()
        {
            foreach (var directory in _directoryService.MonitoredDirectories)
            {
                AddWatcherForDirectory(directory);
            }

            _directoryService.MonitoredDirectories.CollectionChanged += MonitoredDirectories_CollectionChanged;
        }

        private void MonitoredDirectories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MonitoredDirectory newDir in e.NewItems)
                {
                    AddWatcherForDirectory(newDir);
                }
            }

            if (e.OldItems != null)
            {
                foreach (MonitoredDirectory oldDir in e.OldItems)
                {
                    RemoveWatcherForDirectory(oldDir);
                }
            }
        }

        private void AddWatcherForDirectory(MonitoredDirectory directory)
        {
            if (string.IsNullOrEmpty(directory.Path) || !Directory.Exists(directory.Path))
                return;

            var watcher = new FileSystemWatcher(directory.Path)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite,
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };

            watcher.Created += (s, e) => OnCreated(s, e, directory.Id);
            watcher.Deleted += (s, e) => OnChanged(s, e, directory.Id);
            watcher.Changed += (s, e) => OnChanged(s, e, directory.Id);
            watcher.Renamed += (s, e) => OnRenamed(s, e, directory.Id);

            _watchers[directory.Id] = watcher;
        }

        private void RemoveWatcherForDirectory(MonitoredDirectory directory)
        {
            if (_watchers.TryGetValue(directory.Id, out var watcher))
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                _watchers.Remove(directory.Id);
            }
        }

        private bool IsAllowedFileType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension == ".jpg" || extension == ".jpeg" ||
                   extension == ".png" || extension == ".gif" ||
                   extension == ".bmp" || extension == ".tif" ||
                   extension == ".tiff";
        }

        public async void OnCreated(object sender, FileSystemEventArgs e, int watcherId)
        {
            if (!IsAllowedFileType(e.FullPath))
            {
                return;
            }

            if (isFileReady(e.FullPath))
            {
                try
                {
                    var imageId = await _commandBus.SendAsync<UploadImageCommand, string>(new UploadImageCommand 
                    { 
                        FilePath = e.FullPath, 
                        WatcherId = watcherId 
                    });

                    await _imagePathService.StoreImagePath(imageId, e.FullPath);

                    var monitoredDirectory = _directoryService.MonitoredDirectories.FirstOrDefault(d => d.Id == watcherId);

                    var jobsIds = new List<string>();
                    var modelIds = monitoredDirectory?.ModelIds;
                    var modelNames = monitoredDirectory?.ModelNames;
                    var modelsDictionary = new Dictionary<string, string>();

                    // Scheduling Job
                    if (monitoredDirectory != null && modelIds != null && modelNames != null && modelIds.Any() && modelIds.Count == modelNames.Count)
                    {
                        for (int i = 0; i < modelIds.Count; i++)
                        {
                            // Model ID / Name mapping for readability
                            var modelId = modelIds[i];
                            var modelName = modelNames[i];
                            modelsDictionary.Add(modelId, modelName);

                            // Schedule job
                            var jobId = await _commandBus.SendAsync<ScheduleJobCommand, string>(new ScheduleJobCommand
                            { 
                                ImageId = imageId,
                                ModelId = modelId
                            });

                            if (!string.IsNullOrEmpty(jobId))
                                jobsIds.Add(jobId);
                        }
                    }

                    // Getting reports
                    foreach (var jobId in jobsIds)
                    {
                        var isJobFinished = await _queryBus.SendAsync<IsJobFinishedQuery, bool>(new IsJobFinishedQuery 
                        { 
                            JobId = jobId 
                        });

                        if (isJobFinished)
                        {
                            var directoryPath = Path.GetDirectoryName(e.FullPath);

                            var modelId = monitoredDirectory.ModelIds[jobsIds.IndexOf(jobId)];
                            var modelName = modelsDictionary[modelId];
                            modelName = modelName.Replace(" ", "_");
                            var timeStamp = DateTime.Now.ToString("HH_mm");

                            var pdfFileName = $"{Path.GetFileNameWithoutExtension(e.FullPath)}_{modelName}_{timeStamp}.pdf";
                            var pdfFilePath = Path.Combine(directoryPath, pdfFileName);

                            await _commandBus.SendAsync<GenerateReportCommand, bool>(new GenerateReportCommand
                            {
                                JobId = jobId,
                                OutputPath = pdfFilePath
                            });

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

        private bool isFileReady(string fullPath)
        {
            const int maxRetries = 10;
            const int delay = 500;  // milliseconds
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using (FileStream stream = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        if (stream.Length > 0)
                        {
                            return true;
                        }
                    }
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(delay);
                }
            }

            return false;
        }

        private void OnChanged(object sender, FileSystemEventArgs e, int watcherId)
        {
            var logMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.FullPath}, ChangeType: {e.ChangeType}";
            Console.WriteLine(logMessage);
        }

        private void OnRenamed(object sender, RenamedEventArgs e, int watcherId)
        {
            var logMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.OldFullPath} renamed to {e.FullPath}";
            Console.WriteLine(logMessage);
        }

        public void StartWatching()
        {
            foreach (var watcher in _watchers.Values)
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopWatching()
        {
            foreach (var watcher in _watchers.Values)
            {
                watcher.EnableRaisingEvents = false;
            }
        }

        public void Dispose()
        {
            StopWatching();
            foreach (var watcher in _watchers.Values)
            {
                watcher.Dispose();
            }
            _watchers.Clear();
        }
    }
}
