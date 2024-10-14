using MiviaMaui.Interfaces;
using MiviaMaui.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Query_Handlers
{
    public class GetUploadedImageQuery : IQuery<string>
    {
        public string FilePath { get; set; }
    }

    public class GetUploadedImageQueryHandler : IQueryHandler<GetUploadedImageQuery, string>
    {
        private readonly IMiviaClient _miviaClient;

        public GetUploadedImageQueryHandler(IMiviaClient miviaClient)
        {
            _miviaClient = miviaClient;
        }

        public async Task<string> HandleAsync(GetUploadedImageQuery query)
        {
            var images = await _miviaClient.GetImagesAsync();
            var fileName = Path.GetFileName(query.FilePath);
            var existingImage = images.FirstOrDefault(img => img.OriginalFilename == fileName);
            return existingImage?.Id ?? string.Empty;
        }
    }

}
