using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class UserJobDto
    {
        public string id { get; set; }
        public string imageId { get; set; }
        public DateTime finishedAt { get; set; }
        public DateTime startedAt { get; set; }
        public DateTime createdAt { get; set; }
        public JobStatus status { get; set; }
        public object result { get; set; }
        public string modelId { get; set; }
        public ArchivedStatus archived { get; set; }
        public OutdatedStatus outdated { get; set; }
    }

    public enum JobStatus
    {
        CACHED,
        NEW,
        FAILED,
        PENDING
    }

    public class ArchivedStatus
    {
        public string description { get; set; }
        public bool value { get; set; }
    }

    public class OutdatedStatus
    {
        public string description { get; set; }
        public bool value { get; set; }
    }
}
