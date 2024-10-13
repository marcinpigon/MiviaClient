using Android.OS;
using Android.Runtime;
using Android.Util;
using MiviaMaui.Interfaces;
using MiviaMaui.Models;
using MiviaMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Platforms.Android
{
    public class AndroidFileObserver : FileObserver
    {
        private readonly string _absolutePath;
        private readonly HistoryService _historyService;
        private readonly IMiviaClient _miviaClient;
        private readonly DirectoryService _directoryService;
        public AndroidFileObserver(string path, HistoryService historyService, IMiviaClient miviaClient, DirectoryService directoryService)
            : base(path, FileObserverEvents.Create)
        {
            _absolutePath = path;
            _historyService = historyService;
            _miviaClient = miviaClient;
            _directoryService = directoryService;
        }

        public override async void OnEvent([GeneratedEnum] FileObserverEvents e, string path)
        {
            if (path != null && e == FileObserverEvents.Create)
            {            
                var historyMessage = $"[{DateTime.Now}] File: {path} created";
                var record = new HistoryRecord(EventType.FileCreated, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
                await ProcessNewFileAsync(path, 0);
            }
        }

        private async Task ProcessNewFileAsync(string filePath, int watcherId)
        {
            try
            {
                var images = await _miviaClient.GetImagesAsync();
                var fileName = Path.GetFileName(filePath);

                var existingImage = images.FirstOrDefault(img => img.OriginalFilename == fileName);
                var imageId = existingImage?.Id ?? await _miviaClient.PostImageAsync(filePath, false);

                var historyMessage = $"[{DateTime.Now}] Watcher ID: {watcherId}, File: {filePath} uploaded!";
                var record = new HistoryRecord(EventType.FileUploaded, historyMessage);
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
                        var pdfFilePath = Path.Combine(_absolutePath, pdfFileName);

                        // Save to the Downloads folder
                        //var downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                        //var pdfFilePath = Path.Combine(downloadsPath, pdfFileName);

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
                var historyMessage = $"[{DateTime.Now}] Error processing file {filePath}: {ex.Message}";
                var record = new HistoryRecord(EventType.FileError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
            }
        }
    }

}
