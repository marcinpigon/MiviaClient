using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class CreateReportDto
    {
        public List<string> JobsIds { get; set; }
        public int TzOffset { get; set; }
    }
}
