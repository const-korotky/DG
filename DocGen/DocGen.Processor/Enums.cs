using System;

namespace DocGen.Processor
{
    [Flags]
    public enum PrintOption : byte
    {
        None = 0,
        Order = 1,
        Location = 2,
        Zone = 3,
    }

    [Flags]
    public enum PrintZone : byte
    {
        _0 = 0,
        _30 = 1,
        _100 = 2,
    }
}
