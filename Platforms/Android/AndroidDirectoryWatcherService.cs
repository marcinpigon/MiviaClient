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

namespace MiviaMaui.Services
{
    public class AndroidDirectoryWatcherService : IDirectoryWatcherService, IDisposable
    {
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;
        private readonly IMiviaClient _miviaClient;
        private readonly Dictionary<int, ContentObserver> _observers;
        private readonly Dictionary<string, HashSet<string>> _processedFiles;
        private bool _isWatching;

        public AndroidDirectoryWatcherService(DirectoryService directoryService,
            HistoryService historyService,
            IMiviaClient miviaClient)
        {
            _directoryService = directoryService;
            _historyService = historyService;
            _miviaClient = miviaClient;
            _observers = new Dictionary<int, ContentObserver>();
            _processedFiles = new Dictionary<string, HashSet<string>>();

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
            if (string.IsNullOrEmpty(directory.Path))
                return;

            var activity = Platforms.Android.ActivityStateManager.CurrentActivity;
            if (activity == null)
            {

                return;
            }

            var uri = Uri.Parse(directory.Path);
            var documentFile = DocumentFile.FromTreeUri(Platform.CurrentActivity, uri);

            if (documentFile == null || !documentFile.Exists())
                return;

            if (!_processedFiles.ContainsKey(directory.Path))
            {
                _processedFiles[directory.Path] = new HashSet<string>();
            }

            var handler = new Handler(Looper.MainLooper);
            var observer = new FolderContentObserver(handler,
                async () => await CheckForNewFiles(directory));

            _observers[directory.Id] = observer;

            Platform.CurrentActivity?.ContentResolver?.RegisterContentObserver(
                MediaStore.Files.GetContentUri("external"),
                true,
                observer);

            MainThread.BeginInvokeOnMainThread(async () => await CheckForNewFiles(directory));
        }

        private async Task CheckForNewFiles(MonitoredDirectory directory)
        {
            try
            {
                var uri = Uri.Parse(directory.Path);
                var documentFile = DocumentFile.FromTreeUri(Platform.CurrentActivity, uri);

                if (documentFile == null || !documentFile.Exists())
                    return;

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
                }
            }
            catch (Exception ex)
            {
                var historyMessage = $"[{DateTime.Now}] Error checking directory {directory.Path}: {ex.Message}";
                var record = new HistoryRecord(EventType.FileError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
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

                using var dotNetInputStream = new StreamReader(inputStream).BaseStream;
                using var dotNetOutputStream = System.IO.File.OpenWrite(tempFile.AbsolutePath);
                await dotNetInputStream.CopyToAsync(dotNetOutputStream);

                return tempFile.AbsolutePath;
            }
            catch
            {
                return null;
            }
        }

        private void RemoveWatcherForDirectory(MonitoredDirectory directory)
        {
            if (_observers.TryGetValue(directory.Id, out var observer))
            {
                Platform.CurrentActivity?.ContentResolver?.UnregisterContentObserver(observer);
                _observers.Remove(directory.Id);
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
                var fp = directory.Path;
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
            foreach (var directory in _directoryService.MonitoredDirectories)
            {
                MainThread.BeginInvokeOnMainThread(async () => await CheckForNewFiles(directory));
            }
        }

        public void StopWatching()
        {
            _isWatching = false;
            foreach (var observer in _observers.Values)
            {
                Platform.CurrentActivity?.ContentResolver?.UnregisterContentObserver(observer);
            }
            _observers.Clear();
        }

        public void Dispose()
        {
            StopWatching();
        }
    }

    public class FolderContentObserver : ContentObserver
    {
        private readonly Func<Task> _onChangeCallback;

        public FolderContentObserver(Handler handler, Func<Task> onChangeCallback) : base(handler)
        {
            _onChangeCallback = onChangeCallback;
        }

        public override async void OnChange(bool selfChange)
        {
            await _onChangeCallback();
        }

        public override async void OnChange(bool selfChange, Uri uri)
        {
            await _onChangeCallback();
        }
    }
}