using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Dtos
{
    public class LicenseDto
    {
        public LicenseType Type { get; set; }
        public LicenseLimits? Limits { get; set; }
    }

    public enum LicenseType
    {
        DEMO,
        TIME_LIMITED,
        IMAGES_LIMITED,
        FLOATING,
        UNLIMITED,
        ENTERPRISE,
        NONE
    }

    public class LicenseLimits
    {
        public string? Description { get; set; }
        public int Finish { get; set; }
        public int Images { get; set; }
        public int Used { get; set; }
    }
}
