using MiviaMaui.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Interfaces
{
    public interface IImageUploadContextService
    {
        Task<IReadOnlyList<ImageDto>> GetCurrentImagesAsync();
        void SetCurrentImages(IEnumerable<ImageDto> images);
        Task<bool> IsImageAlreadyExistsAsync(string fileName);
    }
}
