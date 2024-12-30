using MiviaMaui.Interfaces;
using MiviaMaui.Services;
using MiviaMaui;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System.Net.Http.Headers;

namespace MiviaTest
{
    public class MiviaClientIntegrationTests : IAsyncLifetime
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<INotificationService> _mockNotificationService;
        private readonly Mock<ISnackbarService> _mockSnackbarService;
        private readonly HistoryService _historyService;
        private readonly MiviaClient _sut; // System Under Test
        private readonly string _accessToken;
        private readonly string _testImagePath;

        public MiviaClientIntegrationTests()
        {
            // Setup configuration to read from user secrets
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<MiviaClientIntegrationTests>()
                .Build();

            _accessToken = configuration["MiviaApi:AccessToken"]
                ?? throw new InvalidOperationException("Access token not found in user secrets");

            // Setup test image path
            _testImagePath = Path.Combine(Path.GetTempPath(), $"test_image_{Guid.NewGuid()}.jpg");

            // Setup HttpClient with authorization header
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://app.mivia.ai")
            };

            _mockNotificationService = new Mock<INotificationService>();
            _mockSnackbarService = new Mock<ISnackbarService>();
            _historyService = new HistoryService();

            _sut = new MiviaClient(
                _httpClient,
                _historyService,
                _mockNotificationService.Object,
                _mockSnackbarService.Object,
                _accessToken
            );
        }

        public async Task InitializeAsync()
        {
            using (var image = new Image<Rgba32>(100, 100))
            {
                await image.SaveAsJpegAsync(_testImagePath);
            }
        }

        public Task DisposeAsync()
        {
            if (File.Exists(_testImagePath))
            {
                File.Delete(_testImagePath);
            }
            return Task.CompletedTask;
        }

        [Fact]
        public async Task UploadImage_ThenVerifyExists_ShouldSucceed()
        {
            // Act - Upload image
            var imageId = await _sut.PostImageAsync(_testImagePath, false);

            // Assert - Check if upload was successful
            Assert.False(string.IsNullOrEmpty(imageId));
            _mockNotificationService.Verify(x =>
                x.ShowNotification("Image", It.Is<string>(s => s.Contains("Sending image"))),
                Times.Once);

            // Act - Get all images
            var images = await _sut.GetImagesAsync();

            // Assert - image exists
            var uploadedImage = images.FirstOrDefault(img => img.Id == imageId);
            Assert.NotNull(uploadedImage);

            // Act - Delete image
            await _sut.DeleteImageAsync(imageId);

            // Assert - image removed
            images = await _sut.GetImagesAsync();
            Assert.DoesNotContain(images, img => img.Id == imageId);
        }
    }
}
