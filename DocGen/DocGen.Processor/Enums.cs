using System;

namespace DocGen.Processor
{
    [Flags]
    public enum PrintOption : byte
    {
        Order = 0,
        Location = 1,
        Zone = 2,
    }

    [Flags]
    public enum PrintZone : byte
    {
        _0 = 0,
        _30 = 1,
        _100 = 2,
    }
}
