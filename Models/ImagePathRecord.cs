using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Models
{
    public class ImagePathRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string ImageId { get; set; }

        public string Path { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
