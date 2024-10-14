using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Commands
{
    public class UploadImageCommand : ICommand
    {
        public string FilePath { get; set; }
        public int WatcherId { get; set; }
    }
}
