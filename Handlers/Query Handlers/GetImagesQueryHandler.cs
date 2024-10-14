using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using MiviaMaui.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Query_Handlers
{
    public class GetImagesQueryHandler : IQueryHandler<GetImagesQuery, List<ImageDto>>
    {
        private readonly IMiviaClient _miviaClient;

        public GetImagesQueryHandler(IMiviaClient miviaClient)
        {
            _miviaClient = miviaClient;
        }

        public async Task<List<ImageDto>> HandleAsync(GetImagesQuery query)
        {
            return await _miviaClient.GetImagesAsync(); 
        }
    }

}
