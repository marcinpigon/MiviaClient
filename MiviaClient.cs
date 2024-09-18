using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using MiviaMaui.Models;
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
        private readonly string? _accessToken;

        private readonly string _baseUrl = "https://app.mivia.ai";
        private const string ImageUri = "/api/image";
        private const string ModelsUri = "/api/settings/models";
        private const string JobsUri = "/api/jobs";
        private const string ReportUri = "/api/reports/pdf2";
        private bool _initialized = false;

        public MiviaClient(HttpClient httpClient, HistoryService historyService, INotificationService notificationService)
        {
            _httpClient = httpClient;
            _historyService = historyService;
            _notificationService = notificationService;
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
                }
            }
        }

        public async Task<List<ModelDto>> GetModelsAsync()
        {
            await InitializeClient();
            var response = await _httpClient.GetAsync("/api/settings/models");

            if (response.IsSuccessStatusCode)
            {
                var historyMessage = "Models fetched";
                var record = new HistoryRecord(EventType.HttpModels, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);

                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Models JSON: {jsonString}");
                return JsonSerializer.Deserialize<List<ModelDto>>(jsonString);

            }
            else
            {
                var historyMessage = $"Failed fetching models: {response.StatusCode}";
                var record = new HistoryRecord(EventType.HttpError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);

                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<List<ImageDto>> GetImagesAsync()
        {
            try
            {
                await InitializeClient();
                var response = await _httpClient.GetAsync(ImageUri);

                response.EnsureSuccessStatusCode();

                var historyMessage = "Images fetched";
                var record = new HistoryRecord(EventType.HttpImages, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);

                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Images: {jsonString}");
                return JsonSerializer.Deserialize<List<ImageDto>>(jsonString);
            }
            catch (Exception ex)
            {
                var historyMessage = $"Failed fetching images: {ex.Message}";
                var record = new HistoryRecord(EventType.HttpError, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
                throw;
            }
        }

        public async Task<string> PostImageAsync(string filePath, bool forced)
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

                    _notificationService.ShowNotification($"Image: {uploadedImage[0].Id}", $"Image: {filePath} sent successfully!");

                    var historyMessage = $"Image sent successfully: {filePath}";
                    var record = new HistoryRecord(EventType.HttpImages, historyMessage);
                    await _historyService.SaveHistoryRecordAsync(record);

                    return uploadedImage?[0]?.Id ?? "";
                }
            }
            catch (Exception e)
            {
                _notificationService.ShowNotification("Image", $"Error sending image: {e.Message}");
                return "";
            }
        }

        public async Task DeleteImageAsync(string id)
        {
            try
            {
                await InitializeClient();
                var response = await _httpClient.DeleteAsync(ImageUri + $"/{id}");
                response.EnsureSuccessStatusCode();
                _notificationService.ShowNotification("Image", $"Deleted image: {id}");

            }
            catch ( Exception e ) 
            {
                _notificationService.ShowNotification("Image", $"Error deleting image: {e.Message}");
            }
        }

        public async Task<string?> ScheduleJobAsync(string imageId, string modelId)
        {
            try
            {
                var requestContent = new
                {
                    imageIds = new[] { imageId },
                    modelId
                };
                var jsonContent = JsonSerializer.Serialize(requestContent);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _notificationService.ShowNotification(
                    "Scheduling job",
                    $"Image ID: {imageId}");

                var response = await _httpClient.PostAsync(JobsUri, content);

                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var jsonJobIds = JsonSerializer.Deserialize<List<PostJobDto>>(jsonResponse);

                _notificationService.ShowNotification(
                    "Job Scheduled!", $"Succesfully scheduled job for image: {jsonJobIds?[0].JobId}");

                return jsonJobIds?[0].JobId;
            }
            catch (Exception ex)
            {
                _notificationService.ShowNotification("Job scheduling failed",$"Failed to schedule job: {ex.Message}");
                return null;
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