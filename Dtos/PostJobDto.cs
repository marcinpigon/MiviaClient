using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class PostJobDto
    {
        [JsonPropertyName("id")]
        public string JobId { get; set; } = "";
    }
}
