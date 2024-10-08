using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MiviaMaui.Models;
using MiviaMaui.Interfaces;
using Android.OS;
using Java.IO;
using File = System.IO.File;
using OperationCanceledException = System.OperationCanceledException;

namespace MiviaMaui.Services
{
    public class AndroidDirectoryWatcherService : IDirectoryWatcherService, IDisposable
    {
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;
        private readonly IMiviaClient _miviaClient;
        private readonly Dictionary<int, CancellationTokenSource> _watcherTokens;
        private bool _isWatching;

        public AndroidDirectoryWatcherService(DirectoryService directoryService,
            HistoryService historyService,
            IMiviaClient miviaClient)
        {
            _directoryService = directoryService;
            _historyService = historyService;
            _miviaClient = miviaClient;
            _watcherTokens = new Dictionary<int, CancellationTokenSource>();
            InitializeWatchers();
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

            var cts = new CancellationTokenSource();
            _watcherTokens[directory.Id] = cts;

            Task.Run(async () => await WatchDirectoryAsync(directory, cts.Token), cts.Token);
        }

        private async Task WatchDirectoryAsync(MonitoredDirectory directory, CancellationToken token)
        {
            var processedFiles = new HashSet<string>();
            var directoryInfo = new DirectoryInfo(directory.Path);

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var currentFiles = Directory.GetFiles(directory.Path)
                        .Where(f => IsAllowedFileType(f))
                        .ToList();

                    foreach (var filePath in currentFiles)
                    {
                        if (!processedFiles.Contains(filePath))
                        {
                            await ProcessNewFileAsync(filePath, directory.Id);
                            processedFiles.Add(filePath);
                        }
                    }

                    await Task.Delay(1000, token); // Poll every second
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    var historyMessage = $"[{DateTime.Now}] Error watching directory {directory.Path}: {ex.Message}";
                    var record = new HistoryRecord(EventType.FileError, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);
                }
            }
        }

        private void RemoveWatcherForDirectory(MonitoredDirectory directory)
        {
            if (_watcherTokens.TryGetValue(directory.Id, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
                _watcherTokens.Remove(directory.Id);
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

        private async Task ProcessNewFileAsync(string filePath, int watcherId)
        {
            var historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {filePath} created";
            var record = new HistoryRecord(EventType.FileCreated, historyMessage);
            await _historyService.SaveHistoryRecordAsync(record);

            try
            {
                var images = await _miviaClient.GetImagesAsync();
                var fileName = Path.GetFileName(filePath);

                var existingImage = images.FirstOrDefault(img => img.OriginalFilename == fileName);
                var imageId = existingImage?.Id ?? await _miviaClient.PostImageAsync(filePath, false);

                historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {filePath} uploaded!";
                record = new HistoryRecord(EventType.FileUploaded, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);

                var monitoredDirectory = _directoryService.MonitoredDirectories.FirstOrDefault(d => d.Id == watcherId);
                if (monitoredDirectory?.ModelIds != null && monitoredDirectory.ModelNames != null)
                {
                    await ProcessModelsAsync(monitoredDirectory, imageId, filePath, watcherId);
                }
            }
            catch (Exception ex)
            {
                historyMessage = $"[{DateTime.Now}] Error processing file {filePath}: {ex.Message}";
                record = new HistoryRecord(EventType.FileError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
            }
        }

        private async Task ProcessModelsAsync(MonitoredDirectory directory, string imageId, string filePath, int watcherId)
        {
            var jobsIds = new List<string>();
            var modelsDictionary = new Dictionary<string, string>();

            for (int i = 0; i < directory.ModelIds.Count; i++)
            {
                var modelId = directory.ModelIds[i];
                var modelName = directory.ModelNames[i];
                modelsDictionary[modelId] = modelName;

                var jobId = await _miviaClient.ScheduleJobAsync(imageId, modelId);
                if (jobId != null)
                {
                    jobsIds.Add(jobId);
                    await LogJobScheduled(jobId, watcherId);
                }
            }

            foreach (var jobId in jobsIds)
            {
                await ProcessJobResultAsync(jobId, filePath, modelsDictionary, watcherId);
            }
        }

        private async Task LogJobScheduled(string jobId, int watcherId)
        {
            var historyMessage = $"[{DateTime.Now}] Job scheduled successfully! Job ID: {jobId}";
            var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
            await _historyService.SaveHistoryRecordAsync(record);
        }

        private async Task ProcessJobResultAsync(string jobId, string filePath, Dictionary<string, string> modelsDictionary, int watcherId)
        {
            if (await _miviaClient.IsJobFinishedAsync(jobId))
            {
                var directoryPath = Path.GetDirectoryName(filePath);
                var modelName = modelsDictionary[jobId].Replace(" ", "_");
                var timeStamp = DateTime.Now.ToString("HH_mm");
                var pdfFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{modelName}_{timeStamp}.pdf";
                var pdfFilePath = Path.Combine(directoryPath, pdfFileName);

                await _miviaClient.GetSaveReportsPDF(new List<string> { jobId }, pdfFilePath);

                var historyMessage = $"[{DateTime.Now}] Report generated for Job ID: {jobId}, saved at: {pdfFilePath}";
                var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
            }
        }

        public void StartWatching()
        {
            _isWatching = true;
        }

        public void StopWatching()
        {
            _isWatching = false;
            foreach (var cts in _watcherTokens.Values)
            {
                cts.Cancel();
            }
        }

        public void Dispose()
        {
            StopWatching();
            foreach (var cts in _watcherTokens.Values)
            {
                cts.Dispose();
            }
            _watcherTokens.Clear();
        }
    }
}