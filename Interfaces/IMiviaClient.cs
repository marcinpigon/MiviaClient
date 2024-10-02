using MiviaMaui.Dtos;
using MiviaMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiviaMaui.Interfaces
{
    public interface IMiviaClient
    {
        Task<List<ModelDto>> GetModelsAsync();
        Task<List<ImageDto>> GetImagesAsync();
        Task<string> PostImageAsync(string filePath, bool forced);
        Task DeleteImageAsync(string id);
        Task<string> ScheduleJobAsync(string imageId, string modelId);
        Task<bool> IsJobFinishedAsync(string jobId, int maxRetries = 10, int delayMs = 10000);
        Task GetSaveReportsPDF(List<string> jobsIds, string filePath);
    }
}
