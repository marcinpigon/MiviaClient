using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Commands
{
    public class ScheduleJobCommand : ICommand
    {
        public string ImageId { get; set; }
        public string ModelId { get; set; }
    }
}
