using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using MiviaMaui.Models;
using MiviaMaui.Resources.Languages;
using MiviaMaui.Services;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace MiviaMaui
{
    public class MiviaClient : IMiviaClient
    {
        private readonly HttpClient _httpClient;    // HttpClient passed through Dependency Injection container
        private readonly HistoryService _historyService;
        private readonly INotificationService _notificationService;
        private readonly ISnackbarService _snackbarService;
        private readonly string? _accessToken;

        private readonly string _baseUrl = "https://app.mivia.ai";
        private const string ImageUri = "/api/image";
        private const string ModelsUri = "/api/settings/models";
        private const string JobsUri = "/api/jobs";
        private const string ReportUri = "/api/reports/pdf2";
        private bool _initialized = false;

        public bool GetInitializedStatus()
        {
            return _initialized;
        }

        public MiviaClient(HttpClient httpClient, HistoryService historyService,
            INotificationService notificationService, ISnackbarService snackbarService)
        {
            _httpClient = httpClient;
            _historyService = historyService;
            _notificationService = notificationService;
            _snackbarService = snackbarService;
        }

        private async Task InitializeClient()
        {
            if (!_initialized)
            {
                var accessToken = await SecureStorage.GetAsync("AccessToken");

                _httpClient.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    _httpClient.DefaultRequestHeaders.Add("authorization", accessToken);
                    _initialized = true;
                }
                else
                {
                    await _snackbarService.ShowErrorSnackbarAsync("Access token not set! Please set it in the Configuration Page", 10000);
                }
            }
        }

        public async Task<List<ModelDto>> GetModelsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/settings/models");

                if (response.IsSuccessStatusCode)
                {
                    var historyMessage = "Models fetched";
                    var record = new HistoryRecord(EventType.HttpModels, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    await _snackbarService.ShowSuccessSnackbarAsync("Models fetched successfully");
                    return JsonSerializer.Deserialize<List<ModelDto>>(jsonString);
                }
                else
                {
                    var historyMessage = $"Failed fetching models: {response.StatusCode}";
                    var record = new HistoryRecord(EventType.HttpError, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);

                    await _snackbarService.ShowErrorSnackbarAsync($"Failed to fetch models: {response.StatusCode}");
                    throw new HttpRequestException($"Request failed with Status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                await _snackbarService.ShowErrorSnackbarAsync($"Failed to fetch models. Please check your internet connection");
                return new List<ModelDto>();
            }
        }

        public async Task<List<ImageDto>> GetImagesAsync()
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                try
                {
                    await InitializeClient();
                    var response = await _httpClient.GetAsync(ImageUri);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    response.EnsureSuccessStatusCode();

                    var historyMessage = "Images fetched";
                    var record = new HistoryRecord(EventType.HttpImages, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Images: {jsonString}");
                    return JsonSerializer.Deserialize<List<ImageDto>>(jsonString);
                }
                catch (UnauthorizedAccessException)
                {
                    await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
                    throw;
                }
                catch (Exception ex)
                {
                    var historyMessage = $"Failed fetching images: {ex.Message}";
                    var record = new HistoryRecord(EventType.HttpError, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);
                    throw;
                }
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
                return new List<ImageDto>();
            }
        }

        public async Task<string> PostImageAsync(string filePath, bool forced)
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                try
                {
                    await InitializeClient();

                    _notificationService.ShowNotification("Image", "Sending image: " + filePath);

                    var mimeType = GetMimeType(filePath);

                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "files",
                            FileName = Path.GetFileName(filePath)
                        };

                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

                        content.Add(fileContent);
                        content.Add(new StringContent(forced.ToString().ToLower()), "forced");

                        var response = await _httpClient.PostAsync(ImageUri, content);

                        response.EnsureSuccessStatusCode();

                        var jsonString = await response.Content.ReadAsStringAsync();

                        List<ImageDto>? uploadedImage = JsonSerializer.Deserialize<List<ImageDto>>(jsonString) ?? null;

                        await _snackbarService.ShowSuccessSnackbarAsync($"Image: {Path.GetFileName(filePath)} sent successfully!");

                        var historyMessage = $"Image sent successfully: {filePath}";
                        var record = new HistoryRecord(EventType.HttpImages, historyMessage, null, filePath);
                        await _historyService.SaveHistoryRecordAsync(record);

                        return uploadedImage?[0]?.Id ?? "";
                    }
                }
                catch (Exception e)
                {
                    _notificationService.ShowNotification("Image", $"Error sending image: {e.Message}");
                    await _snackbarService.ShowErrorSnackbarAsync($"Error sending image: {e.Message}");
                    return "";
                }
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
                return string.Empty;
            }
        }

        public async Task DeleteImageAsync(string id)
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                try
                {
                    await InitializeClient();
                    var response = await _httpClient.DeleteAsync(ImageUri + $"/{id}");
                    response.EnsureSuccessStatusCode();
                    await _snackbarService.ShowSuccessSnackbarAsync($"Deleted image: {id}");

                }
                catch (Exception e)
                {
                    await _snackbarService.ShowErrorSnackbarAsync($"Error deleting image: {e.Message}");
                }
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
            }
        }

        public async Task<string?> ScheduleJobAsync(string imageId, string modelId)
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                try
                {
                    await InitializeClient();
                    var requestContent = new
                    {
                        imageIds = new[] { imageId },
                        modelId
                    };
                    var jsonContent = JsonSerializer.Serialize(requestContent);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(JobsUri, content);

                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var jsonJobIds = JsonSerializer.Deserialize<List<PostJobDto>>(jsonResponse);

                    await _snackbarService.ShowSuccessSnackbarAsync($"Succesfully scheduled job for image: {jsonJobIds?[0].JobId}");

                    return jsonJobIds?[0].JobId;
                }
                catch (Exception ex)
                {
                    _notificationService.ShowNotification("Job scheduling failed", $"Failed to schedule job: {ex.Message}");
                    await _snackbarService.ShowErrorSnackbarAsync($"Failed to schedule job: {ex.Message}");
                    return null;
                }
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
                return null;
            }
        }

        public async Task<UserJobDto?> GetJobDetailsAsync(string jobId)
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                try
                {
                    await InitializeClient();

                    var response = await _httpClient.GetAsync($"{JobsUri}/{jobId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var jobDetails = JsonSerializer.Deserialize<UserJobDto>(jsonString);
                        return jobDetails;
                    }
                    else
                    {
                        var historyMessage = $"Failed to fetch job details for Job ID: {jobId}, Status: {response.StatusCode}";
                        var record = new HistoryRecord(EventType.HttpError, historyMessage);
                        await _historyService.SaveHistoryRecordAsync(record);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    var historyMessage = $"Error fetching job details for Job ID: {jobId}: {ex.Message}";
                    var record = new HistoryRecord(EventType.HttpError, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);
                    return null;
                }
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
                return null;
            }
        }


        public async Task<bool> IsJobFinishedAsync(string jobId, int maxRetries = 10, int delayMs = 10000)
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                int retries = 0;
                while (retries < maxRetries)
                {
                    try
                    {
                        var jobDetails = await GetJobDetailsAsync(jobId);

                        if (jobDetails == null) break;

                        if (jobDetails.Status == "FAILED")
                        {
                            var historyMessage = $"[{DateTime.Now}] Job ID: {jobId} has failed. No report will be generated.";
                            var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                            await _historyService.SaveHistoryRecordAsync(record);

                            await _snackbarService.ShowErrorSnackbarAsync(historyMessage);

                            return false;
                        }

                        if (jobDetails.Status == "CACHED" || jobDetails.Status == "NEW")
                        {
                            var historyMessage = $"[{DateTime.Now}] Job ID: {jobId}, Status: {jobDetails.Status}";
                            var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                            await _historyService.SaveHistoryRecordAsync(record);

                            await _snackbarService.ShowSuccessSnackbarAsync(historyMessage);
                            return true;
                        }

                        retries++;
                        await Task.Delay(delayMs);
                    }
                    catch (Exception ex)
                    {
                        var historyMessage = $"[{DateTime.Now}] Error checking job Status for Job ID: {jobId}: {ex.Message}";
                        var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                        await _historyService.SaveHistoryRecordAsync(record);
                        return false;
                    }
                }

                var finalMessage = $"[{DateTime.Now}] Job ID: {jobId} did not complete within the allowed retries.";
                var finalRecord = new HistoryRecord(EventType.HttpJobs, finalMessage);
                await _historyService.SaveHistoryRecordAsync(finalRecord);

                await _snackbarService.ShowErrorSnackbarAsync(finalMessage);

                return false;
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
                return false;
            }
        }



        public async Task GetSaveReportsPDF(List<string> jobsIds, string filePath)
        {
            if (!_initialized)
            {
                await InitializeClient();
            }
            if (_initialized)
            {
                try
                {
                    await InitializeClient();
                    var offset = -DateTimeOffset.Now.Offset.TotalMinutes;
                    var content = new
                    {
                        jobsIds,
                        tzOffset = offset
                    };
                    var jsonContent = JsonSerializer.Serialize(content);
                    var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("/api/reports/pdf", stringContent);

                    response.EnsureSuccessStatusCode();

                    var pdfBytes = await response.Content.ReadAsStreamAsync();

                    using (var pdfStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await pdfStream.CopyToAsync(fileStream);
                        }
                    }

                    _notificationService.ShowClickableNotification("Report Saved", $"PDF report successfully saved to {filePath}", filePath);
                    await _snackbarService.ShowSuccessSnackbarAsync($"PDF report successfully saved to {filePath}", filePath);

                }
                catch (Exception ex)
                {
                    _notificationService.ShowNotification("Error", $"An error occurred while saving the PDF report: {ex.Message}");
                }
            }
            else
            {
                await _snackbarService.ShowErrorSnackbarAsync("Couldn't authorize user. Please check your settings.", 10000);
            }
        }

        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".tif" or ".tiff" => "image/tiff",
                ".ico" => "image/x-icon",
                _ => "application/octet-stream"
            };
        }

    }
}