using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class ImageDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OriginalFilename { get; set; }
        public bool Validated { get; set; }
    }
}
