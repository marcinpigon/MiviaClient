using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using MiviaMaui.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaTest
{
    public class ModelServiceTests
    {
        private readonly Mock<IMiviaClient> _mockClient;
        private readonly ModelService _modelService;

        public ModelServiceTests()
        {
            _mockClient = new Mock<IMiviaClient>();
            _modelService = new ModelService(_mockClient.Object);
        }

        [Fact]
        public async Task GetModelsAsync_ShouldFetchModels_WhenCacheIsEmpty()
        {
            // Arrange
            var expectedModels = new List<ModelDto>
            {
                new ModelDto { Id = "ID1", Name = "Model1" },
                new ModelDto { Id = "ID2", Name = "Model2" }
            };

            _mockClient.Setup(client => client.GetModelsAsync())
                .ReturnsAsync(expectedModels);

            // Act
            var result = await _modelService.GetModelsAsync();

            // Assert
            Assert.Equal(expectedModels, result);
            _mockClient.Verify(client => client.GetModelsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetModelsAsync_ShouldNotFetchModels_WhenCacheIsValid()
        {
            // Arrange
            var cachedModels = new List<ModelDto>
            {
                new ModelDto { Id = "ID1", Name = "Model1" },
                new ModelDto { Id = "ID2", Name = "Model2" }
            };

            _mockClient.Setup(client => client.GetModelsAsync())
                .ReturnsAsync(cachedModels);

            // Act
            var result = await _modelService.GetModelsAsync();
            var secondResult = await _modelService.GetModelsAsync(); // Should use cache

            // Assert
            Assert.Equal(cachedModels, result);
            Assert.Equal(cachedModels, secondResult);
            _mockClient.Verify(client => client.GetModelsAsync(), Times.Once); // Ensure it only fetched once
        }
    }
}
