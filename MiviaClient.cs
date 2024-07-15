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

        MiviaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _accessToken = SecureStorage.GetAsync("AccessToken").Result ?? "";
            
            if (string.IsNullOrEmpty(_accessToken))
            { 
            }
        }
    }
}