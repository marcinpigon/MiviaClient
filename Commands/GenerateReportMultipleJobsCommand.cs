using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Commands
{
    public class GenerateReportMultipleJobsCommand : ICommand
    {
        public List<string> JobIds { get; set; }
        public string OutputPath { get; set; }
    }
}
