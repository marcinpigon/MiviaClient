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
        // jaki obrazek, model, kiedy, wynik (udane, czy nie) lepiej wiecej niz mniej
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public EventType EventType { get; set; }
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; }

        public HistoryRecord() { }

        public HistoryRecord(EventType eventType, string description)
        {
            EventType = eventType;
            Description = description;
            Timestamp = DateTime.Now;
        }
    }
}
