using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Models
{
    public class HistoryRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string? FolderPath { get; set; }
        public string? FilePath { get; set; }

        public HistoryRecord() { }

        public HistoryRecord(EventType eventType, string description, string? folderPath = null, string? filePath = null)
        {
            EventType = eventType;
            Description = description;
            Timestamp = DateTime.Now;
            FolderPath = folderPath;
            FilePath = filePath;
        }
    }
}
