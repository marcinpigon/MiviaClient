using MiviaMaui.Dtos;
using MiviaMaui.Models;
using MiviaMaui.Services;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MiviaMaui
{
    public class MiviaClient : IMiviaClient
    {
        private readonly HttpClient _httpClient;    // HttpClient passed through Dependency Injection container
        private readonly HistoryService _historyService;
        private readonly string _accessToken;

        private readonly string _baseUrl = "https://app.mivia.ai";
        private const string UploadUri = "/api/image";
        private const string ModelsUri = "/api/settings/models";
        private const string ModelUri = "/api/jobs";
        private const string ReportUri = "/api/reports/pdf2";

        private bool _initialized = false;

        public MiviaClient(HttpClient httpClient, HistoryService historyService)
        {
            _httpClient = httpClient;
            _historyService = historyService;
        }

        private async Task InitializeClient()
        {
            if (!_initialized)
            {
                // Retrieve the access token asynchronously
                var accessToken = await SecureStorage.GetAsync("AccessToken");

                // Clear existing headers to ensure clean configuration
                _httpClient.DefaultRequestHeaders.Clear();

                // Add the authorization header if the access token is available
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
                var response = await _httpClient.GetAsync("/api/image");

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
    }
}