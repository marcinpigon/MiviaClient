using Android.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MiviaMaui.Models;
using MiviaMaui.Interfaces;
using Android.Provider;
using AndroidX.DocumentFile.Provider;
using Android.Database;
using Path = System.IO.Path;
using Uri = Android.Net.Uri;
using static Android.Provider.MediaStore;
using MiviaMaui.Platforms.Android;

namespace MiviaMaui.Services
{
    public class AndroidDirectoryWatcherService : IDirectoryWatcherService, IDisposable
    {
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;
        private readonly IMiviaClient _miviaClient;
        private readonly Dictionary<string, HashSet<string>> _processedFiles;
        private bool _isWatching;
        private Timer _pollingTimer;

        public AndroidDirectoryWatcherService(DirectoryService directoryService,
            HistoryService historyService,
            IMiviaClient miviaClient)
        {
            _directoryService = directoryService;
            _historyService = historyService;
            _miviaClient = miviaClient;
            _processedFiles = new Dictionary<string, HashSet<string>>();

            InitializeWatchers();
        }

        private void InitializeWatchers()
        {
            foreach (var directory in _directoryService.MonitoredDirectories)
            {
                _processedFiles[directory.Path] = new HashSet<string>();
            }

            _directoryService.MonitoredDirectories.CollectionChanged += MonitoredDirectories_CollectionChanged;
        }

        private void MonitoredDirectories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MonitoredDirectory newDir in e.NewItems)
                {
                    _processedFiles[newDir.Path] = new HashSet<string>();
                }
            }

            if (e.OldItems != null)
            {
                foreach (MonitoredDirectory oldDir in e.OldItems)
                {
                    _processedFiles.Remove(oldDir.Path);
                }
            }
        }

        public void StartWatching()
        {
            _isWatching = true;
            _pollingTimer = new Timer(PollDirectories, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        public void StopWatching()
        {
            _isWatching = false;
            _pollingTimer?.Dispose();
        }

        private async void PollDirectories(object state)
        {
            await LogError("CHECKING FOR NEW FILES");
            if (!_isWatching) return;

            foreach (var directory in _directoryService.MonitoredDirectories)
            {

                MainThread.BeginInvokeOnMainThread(async () => await CheckForNewFiles(directory));
            }
        }


        private async Task CheckForNewFiles(MonitoredDirectory directory)
        {
            try
            {
                var uri = Uri.Parse(directory.Path);
                var documentFile = DocumentFile.FromTreeUri(Platform.CurrentActivity, uri);

                if (documentFile == null || !documentFile.Exists())
                {
                    await LogError($"Directory not found or inaccessible: {directory.Path}");
                    return;
                }

                foreach (var file in documentFile.ListFiles())
                {
                    if (!IsAllowedFileType(file.Name))
                        continue;

                    var fileId = $"{file.Uri}_{file.LastModified()}";

                    if (_processedFiles[directory.Path].Contains(fileId))
                        continue;

                    _processedFiles[directory.Path].Add(fileId);

                    var actualPath = await CopyFileToCache(file);
                    if (actualPath != null)
                    {
                        await ProcessNewFileAsync(actualPath, directory.Id);
                    }
                    else
                    {
                        await LogError($"Failed to copy file to cache: {file.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                await LogError($"Error checking directory {directory.Path}: {ex.Message}");
            }
        }

        private async Task<string> CopyFileToCache(DocumentFile file)
        {
            try
            {
                var cacheDir = Platform.CurrentActivity?.CacheDir;
                if (cacheDir == null) return null;

                var tempFile = new Java.IO.File(cacheDir, file.Name);
                using var inputStream = Platform.CurrentActivity?.ContentResolver?.OpenInputStream(file.Uri);
                using var outputStream = new Java.IO.FileOutputStream(tempFile);

                if (inputStream == null) return null;

                await using var dotNetInputStream = new StreamReader(inputStream).BaseStream;
                await using var dotNetOutputStream = System.IO.File.OpenWrite(tempFile.AbsolutePath);
                await dotNetInputStream.CopyToAsync(dotNetOutputStream);

                return tempFile.AbsolutePath;
            }
            catch (Exception ex)
            {
                await LogError($"Error copying file to cache: {file.Name}, Error: {ex.Message}");
                return null;
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

                var jobsIds = new List<string>();
                var modelIds = monitoredDirectory?.ModelIds;
                var modelNames = monitoredDirectory?.ModelNames;
                var modelsDictionary = new Dictionary<string, string>();

                if (monitoredDirectory != null && modelIds != null && modelNames != null && modelIds.Any() && modelIds.Count == modelNames.Count)
                {
                    for (int i = 0; i < modelIds.Count; i++)
                    {
                        var modelId = modelIds[i];
                        var modelName = modelNames[i];

                        modelsDictionary.Add(modelId, modelName);

                        // Schedule job
                        var jobId = await _miviaClient.ScheduleJobAsync(imageId, modelId);
                        jobsIds.Add(jobId);

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
                    historyMessage = "Model IDs and Names count mismatch or no models found.";
                    record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);
                }

                // Getting reports
                foreach (var jobId in jobsIds)
                {
                    var isJobFinished = await _miviaClient.IsJobFinishedAsync(jobId);

                    if (isJobFinished)
                    {
                        var directoryPath = Path.GetDirectoryName(filePath);

                        var modelId = monitoredDirectory.ModelIds[jobsIds.IndexOf(jobId)];
                        var modelName = modelsDictionary[modelId];
                        modelName = modelName.Replace(" ", "_");
                        var timeStamp = DateTime.Now.ToString("HH_mm");

                        var pdfFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{modelName}_{timeStamp}.pdf";
                        //var pdfFilePath = Path.Combine(directoryPath, pdfFileName);

                        // Save to the Downloads folder
                        var downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                        var pdfFilePath = Path.Combine(downloadsPath, pdfFileName);

                        await _miviaClient.GetSaveReportsPDF(jobsIds, pdfFilePath);

                        historyMessage = $"[{DateTime.Now}] Report generated for Job ID: {jobId}, saved at: {pdfFilePath}";
                        record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                        await _historyService.SaveHistoryRecordAsync(record);
                    }
                    else
                    {
                        historyMessage = $"[{DateTime.Now}] Job ID: {jobId} has failed or is incomplete. No report generated.";
                        record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                        await _historyService.SaveHistoryRecordAsync(record);
                    }
                }

            }
            catch (Exception ex)
            {
                historyMessage = $"[{DateTime.Now}] Error processing file {filePath}: {ex.Message}";
                record = new HistoryRecord(EventType.FileError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
            }
        }

        private async Task LogError(string message)
        {
            var historyMessage = $"[{DateTime.Now}] {message}";
            var record = new HistoryRecord(EventType.FileError, historyMessage);
            await _historyService.SaveHistoryRecordAsync(record);
        }

        private async Task LogJobScheduled(string jobId, int watcherId)
        {
            var historyMessage = $"[{DateTime.Now}] Job scheduled successfully! Job ID: {jobId}";
            var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
            await _historyService.SaveHistoryRecordAsync(record);
        }

        public void Dispose()
        {
            StopWatching();
        }
    }
}