using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Models
{
    public enum EventType
    {
        DirectoryCreated,
        DirectoryDeleted,
        DirectoryUpdated,

        ConfigurationUpdated,

        FileCreated,
        FileDeleted,
        FileUpdated,
        FileUploaded,
        FileError,

        HttpModels,
        HttpImages,
        HttpJobs,
        HttpError,
    }
}
