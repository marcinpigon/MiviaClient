using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class ExecuteRequestDto
    {
        public required string ModelId {  get; set; }
        public required List<string> ImageIds { get; set; }

    }
}
