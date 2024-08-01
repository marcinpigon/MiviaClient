using MiviaMaui.Dtos;
using MiviaMaui.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MiviaMaui
{
    public class MiviaClient : IMiviaClient
    {
        private readonly HttpClient _httpClient;    // HttpClient passed through Dependency Injection container
        private readonly string _accessToken;

        private readonly string _baseUrl = "https://app.mivia.ai";
        private const string UploadUri = "/api/image";
        private const string ModelsUri = "/api/settings/models";
        private const string ModelUri = "/api/jobs";
        private const string ReportUri = "/api/reports/pdf2";

        public MiviaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task InitializeClient()
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

        public async Task<List<ModelDto>> GetModelsAsync()
        {
            await InitializeClient();
            var response = await _httpClient.GetAsync("/api/settings/models");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Models JSON: {jsonString}");
                return JsonSerializer.Deserialize<List<ModelDto>>(jsonString);
            }
            else
            {
                // Handle the error response accordingly
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}