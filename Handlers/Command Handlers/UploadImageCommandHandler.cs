using MiviaMaui.Commands;
using MiviaMaui.Interfaces;
using MiviaMaui.Models;
using MiviaMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Command_Handlers
{
    public class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, string>
    {
        private readonly IMiviaClient _miviaClient;
        private readonly HistoryService _historyService;
        private readonly IImageUploadContextService _imageUploadContextService;

        public UploadImageCommandHandler(IMiviaClient miviaClient, HistoryService historyService, 
            IImageUploadContextService imageUploadContextService)
        {
            _miviaClient = miviaClient;
            _historyService = historyService;
            _imageUploadContextService = imageUploadContextService;
        }

        public async Task<string> HandleAsync(UploadImageCommand command)
        {
            var fileName = Path.GetFileName(command.FilePath);
            var existingImage = await _imageUploadContextService.IsImageAlreadyExistsAsync(fileName)
                ? (await _imageUploadContextService.GetCurrentImagesAsync())
                    .FirstOrDefault(img => img.OriginalFilename == fileName)
                : null;

            string imageId;
            if (existingImage != null)
            {
                imageId = existingImage.Id;
            }
            else
            {
                imageId = await _miviaClient.PostImageAsync(command.FilePath, false);
                var historyMessage = $"[{DateTime.Now}] Watcher ID: {command.WatcherId}, File: {command.FilePath} uploaded!";
                var record = new HistoryRecord(EventType.FileUploaded, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);
            }
            return imageId;
        }
    }

}
