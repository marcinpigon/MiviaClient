using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class ImageDto
    {
        public string Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("orginalFilename")]
        public string OriginalFilename { get; set; }

        public bool Validated { get; set; }
    }
}
