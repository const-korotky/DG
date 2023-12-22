using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Processor
{
    [Flags]
    public enum PrintOption : byte
    {
        Order = 0,
        Location = 1,
        Zone = 2,
    }
}
