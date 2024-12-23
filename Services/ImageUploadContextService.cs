using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Services
{
    public class ImageUploadContextService : IImageUploadContextService
    {
        private List<ImageDto> _currentImages = new List<ImageDto>();
        private readonly IMiviaClient _miviaClient;

        public ImageUploadContextService(IMiviaClient miviaClient)
        {
            _miviaClient = miviaClient;
        }

        public async Task<IReadOnlyList<ImageDto>> GetCurrentImagesAsync()
        {
            if (!_currentImages.Any())
            {
                _currentImages = await _miviaClient.GetImagesAsync();
            }
            return _currentImages.AsReadOnly();
        }

        public void SetCurrentImages(IEnumerable<ImageDto> images)
        {
            _currentImages = new List<ImageDto>(images);
        }

        public async Task<bool> IsImageAlreadyExistsAsync(string fileName)
        {
            var images = await GetCurrentImagesAsync();
            return images.Any(img => img.OriginalFilename == fileName);
        }
    }
}
