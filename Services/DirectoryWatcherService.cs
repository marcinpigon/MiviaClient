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

namespace MiviaMaui.Services
{
    public class DirectoryWatcherService : IDisposable
    {
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;
        private readonly Dictionary<int, FileSystemWatcher> _watchers;
        private bool _isWatching;

        private readonly IMiviaClient _miviaClient;

        public DirectoryWatcherService(DirectoryService directoryService, HistoryService historyService, IMiviaClient miviaClient)
        {
            _directoryService = directoryService;
            _historyService = historyService;
            _watchers = new Dictionary<int, FileSystemWatcher>();
            InitializeWatchers();
            _miviaClient = miviaClient;
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

        private async void OnCreated(object sender, FileSystemEventArgs e, int watcherId)
        {
            var historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.FullPath} created";
            var record = new HistoryRecord(EventType.FileCreated, historyMessage);
            await _historyService.SaveHistoryRecordAsync(record);

            if (isFileReady(e.FullPath))
            {
                try
                {
                    // Sending image
                    var imageId = await _miviaClient.PostImageAsync(e.FullPath, false);
                    historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.FullPath} uploaded!";
                    record = new HistoryRecord(EventType.FileUploaded, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);

                    // Scheduling Job
                    var monitoredDirectory = _directoryService.MonitoredDirectories.FirstOrDefault(d => d.Id == watcherId);
                    if (monitoredDirectory != null && monitoredDirectory.ModelIds.Any())
                    {
                        foreach (var modelId in monitoredDirectory.ModelIds)
                        {
                            var jobId = await _miviaClient.ScheduleJobAsync(imageId, modelId);
                            if (jobId != null)
                            {
                                historyMessage = $"[{DateTime.Now}] Job scheduled successfully! Job ID: {jobId}";
                            }
                            else
                            {
                                historyMessage = $"[{DateTime.Now}] Failed to schedule job for Image: {imageId} with Model: {modelId}";
                            }
                            record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                            await _historyService.SaveHistoryRecordAsync(record);
                        }
                    }
                    else
                    {
                        historyMessage = $"Monitored directory not found!";
                        record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                        await _historyService.SaveHistoryRecordAsync(record);
                    }
                }
                catch (Exception ex) 
                {
                    historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.FullPath} failed to upload: {ex.Message}";
                    record = new HistoryRecord(EventType.FileError, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);
                }
            }
            else
            {
                historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.FullPath} is not ready for processing";
                record = new HistoryRecord(EventType.FileError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
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
                    // The file is not ready yet, wait and try again
                    System.Threading.Thread.Sleep(delay);
                }
            }

            return false;
        }

        private void OnChanged(object sender, FileSystemEventArgs e, int watcherId)
        {
            var logMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.FullPath}, ChangeType: {e.ChangeType}";
            Console.WriteLine(logMessage);
            LogToFile(logMessage);
        }

        private void OnRenamed(object sender, RenamedEventArgs e, int watcherId)
        {
            var logMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {e.OldFullPath} renamed to {e.FullPath}";
            Console.WriteLine(logMessage);
            LogToFile(logMessage);
        }

        private void LogToFile(string message)
        {
            //var logFilePath = @"C:\Users\Marcin\Desktop\watcher1\log\log.txt";
            var logFilePath = @"C:\Users\marci\OneDrive\Pulpit\Projekt inzynierski\log.txt";
            try
            {
                File.AppendAllText(logFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public void StartWatching()
        {
            _isWatching = true;
            // The initialization is done in the constructor, but we can ensure all watchers are active here.
            foreach (var watcher in _watchers.Values)
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopWatching()
        {
            _isWatching = false;
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
