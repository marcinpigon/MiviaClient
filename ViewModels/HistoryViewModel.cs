using MiviaMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.ViewModels
{
    public class HistoryRecordViewModel
    {
        public HistoryRecord Record { get; }

        public bool HasPath => !string.IsNullOrEmpty(Record.FolderPath) ||
                             !string.IsNullOrEmpty(Record.FilePath);

        public DateTime Timestamp => Record.Timestamp;
        public string? Description => Record.Description;
        public EventType EventType => Record.EventType;
        public string? FilePath => Record.FilePath;
        public string? FolderPath => Record.FolderPath;

        public HistoryRecordViewModel(HistoryRecord record)
        {
            Record = record;
        }
    }
}
