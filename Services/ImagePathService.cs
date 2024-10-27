using MiviaMaui.Interfaces;
using MiviaMaui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Services
{
    public class ImagePathService : IImagePathService
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly string _dbPath;

        public ImagePathService(string? dbPath = null)
        {
            _dbPath = dbPath ?? DatabasePath.GetDatabasePath("MiviaMaui.db3");
            _database = new SQLiteAsyncConnection(_dbPath);
            _database.CreateTableAsync<ImagePathRecord>().Wait();
        }

        public async Task<string> GetImagePath(string imageId)
        {
            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            var record = await _database.Table<ImagePathRecord>()
                .FirstOrDefaultAsync(x => x.ImageId == imageId);

            return record?.Path ?? throw new KeyNotFoundException($"Image path not found for ID: {imageId}");
        }

        public Task<List<ImagePathRecord>> GetAllImagePaths()
        {
            return _database.Table<ImagePathRecord>().ToListAsync();
        }

        public async Task<int> StoreImagePath(string imageId, string path)
        {
            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var existingRecord = await _database.Table<ImagePathRecord>()
                .FirstOrDefaultAsync(x => x.ImageId == imageId);

            if (existingRecord != null)
            {
                existingRecord.Path = path;
                return await _database.UpdateAsync(existingRecord);
            }

            var newRecord = new ImagePathRecord
            {
                ImageId = imageId,
                Path = path,
                CreatedAt = DateTime.UtcNow
            };

            return await _database.InsertAsync(newRecord);
        }

        public async Task<int> DeleteImagePath(string imageId)
        {
            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            return await _database.Table<ImagePathRecord>()
                .DeleteAsync(x => x.ImageId == imageId);
        }

        public async Task<bool> ImagePathExists(string imageId)
        {
            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            var count = await _database.Table<ImagePathRecord>()
                .Where(x => x.ImageId == imageId)
                .CountAsync();

            return count > 0;
        }

        public async Task DropImagePathTableAsync()
        {
            await _database.ExecuteAsync("DROP TABLE IF EXISTS ImagePathRecord");
        }

        public async Task RecreateImagePathTableAsync()
        {
            await DropImagePathTableAsync();
            await _database.CreateTableAsync<ImagePathRecord>();
        }
    }

    public static class ImagePathServiceExtensions
    {
        public static IServiceCollection AddImagePathService(this IServiceCollection services, string? dbPath = null)
        {
            services.AddSingleton<IImagePathService>(provider => new ImagePathService(dbPath));
            return services;
        }
    }
}
