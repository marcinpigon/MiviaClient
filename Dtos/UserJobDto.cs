using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    using System;
    using System.Text.Json.Serialization;

    public class UserJobDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("imageId")]
        public string ImageId { get; set; } 

        [JsonPropertyName("finishedAt")]
        public DateTime? FinishedAt { get; set; } 

        [JsonPropertyName("startedAt")]
        public DateTime? StartedAt { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } 

        [JsonPropertyName("status")]
        public string Status { get; set; } 

        [JsonPropertyName("result")]
        public object? Result { get; set; } 

        [JsonPropertyName("modelId")]
        public string ModelId { get; set; } 

        [JsonPropertyName("archived")]
        public bool Archived { get; set; } 

        [JsonPropertyName("outdated")]
        public bool Outdated { get; set; }
    }
}
