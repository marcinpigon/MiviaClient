using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class UserJobDto
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public JobStatus Status { get; set; }
        public object Result { get; set; }
        public string ModelId { get; set; }
        public ArchivedStatus Archived { get; set; }
        public OutdatedStatus Outdated { get; set; }
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
        public string Description { get; set; }
        public bool Value { get; set; }
    }

    public class OutdatedStatus
    {
        public string Description { get; set; }
        public bool Value { get; set; }
    }
}
