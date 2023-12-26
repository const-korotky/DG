using System;

namespace DocGen.Processor
{
    [Flags]
    public enum PrintOption : byte
    {
        None = 0,
        Order = 1,
        Location = 10,
        Zone = 20,
    }

    [Flags]
    public enum PrintZone : byte
    {
        _0,
        _30,
        _100,
    }
}
