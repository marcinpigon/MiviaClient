using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Commands
{
    public class GenerateReportCommand : ICommand
    {
        public string JobId { get; set; }
        public string OutputPath { get; set; }
    }
}
