﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Queries
{
    public class IsJobFinishedQuery : IQuery<bool>
    {
        public string JobId { get; set; }
    }
}
