﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUMO
{
    class Track
    {
        public string Name { get; set; }
        public IEnumerable<PatternPlacement> Patterns { get; }
    }
}
