using MiviaMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Interfaces
{
    public interface IImagePathService
    {
        Task<string> GetImagePath(string imageId);
        Task<List<ImagePathRecord>> GetAllImagePaths();
        Task<int> StoreImagePath(string imageId, string path);
        Task<int> DeleteImagePath(string imageId);
        Task<bool> ImagePathExists(string imageId);
        Task DropImagePathTableAsync();
        Task RecreateImagePathTableAsync();
    }
}
