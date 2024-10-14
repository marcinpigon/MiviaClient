using MiviaMaui.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Queries
{
    public class GetImagesQuery : IQuery<List<ImageDto>>
    {
        public List<ImageDto> Images { get; set; }
    }
}
