using System;

namespace TLC6502
{
    public enum PROCESSOR_FLAGS
    {
        CARRY = 0x01,
        ZERO = 0x02,
        INTERRUPT_DISABLE = 0x04,
        DECIMAL = 0x08,
        FLAG_B1 = 0x10,
        FLAG_B2 = 0x20,
        OVERFLOW = 0x40,
        NEGATIVE = 0x80
    };
}
