using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Reflection;
using TLC6502;

namespace TLC6502
{
    public class Processor
    {
        #region Instance Variables
        private TLC6502.Memory m_Memory;

        private byte m_REGISTER_A;
        private byte m_REGISTER_X;
        private byte m_REGISTER_Y;
        private byte m_STACK_POINTER;
        private ushort m_PROGRAM_COUNTER;
        private byte m_STATUS_REGISTER;
        #endregion

        #region Constructors
        public Processor() : this(new Memory()) { }
        public Processor(ushort MemorySize) : this(new Memory(MemorySize)) { }
        public Processor(Memory memory)
        {
            this.m_Memory = memory;
        }
        #endregion

        #region Property Accessors - Instance Variable Objects
        public TLC6502.Memory Memory { get => m_Memory; }
        #endregion

        #region Property Accessors - Registers
        public byte A { get => m_REGISTER_A; set => m_REGISTER_A = value; }
        public byte X { get => m_REGISTER_X; set => m_REGISTER_X = value; }
        public byte Y { get => m_REGISTER_Y; set => m_REGISTER_Y = value; }
        public byte SP { get => m_STACK_POINTER; set => m_STACK_POINTER = value; }
        public ushort PC { get => m_PROGRAM_COUNTER; set => m_PROGRAM_COUNTER = value; }
        public byte PSR { get => m_STATUS_REGISTER; set => m_STATUS_REGISTER = value; }

        #endregion

        #region Property Accessors - Processor Flags
        public bool PSR_CARRY
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.CARRY);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.CARRY, value);
        }
        public bool PSR_DECIMAL
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.DECIMAL);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.DECIMAL, value);
        }
        public bool PSR_B1
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.FLAG_B1);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.FLAG_B1, value);
        }
        public bool PSR_B2
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.FLAG_B2);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.FLAG_B2, value);
        }
        public bool PSR_INTERRUPT_DISABLE
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.INTERRUPT_DISABLE);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.INTERRUPT_DISABLE, value);
        }
        public bool PSR_NEGATIVE
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.NEGATIVE);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.NEGATIVE, value);
        }
        public bool PSR_OVERFLOW
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.OVERFLOW);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.OVERFLOW, value);
        }
        public bool PSR_ZERO
        {
            get => this.GetPSRFlag(PROCESSOR_FLAGS.ZERO);
            set => this.SetPSRFlag(PROCESSOR_FLAGS.ZERO, value);
        }
        #endregion

        #region Processor Flag Helpers
        private void SetPSRFlag(PROCESSOR_FLAGS whichFlag, bool flagValue)
        {
            if (flagValue)
            {
                this.PSR |= (byte)whichFlag;
            }
            else
            {
                this.PSR &= (byte)~((byte)whichFlag);
            }
        }
        private void SetPSRFlag(PROCESSOR_FLAGS whichFlag)
        {
            SetPSRFlag(whichFlag, true);
        }

        private void ClearPSRFlag(PROCESSOR_FLAGS whichFlag)
        {
            SetPSRFlag(whichFlag, false);
        }

        public bool GetPSRFlag(PROCESSOR_FLAGS whichFlag)
        {
            byte value = (byte)(this.PSR & (byte)whichFlag);
            return (value == 0 ? false : true);
        }
        #endregion

        #region 8/16 bit Conversion Helpers

        public ushort BytesToWord(byte MSB, byte LSB)
        {
            return (ushort)((MSB << 8) + LSB);
        }

        public byte MSB(ushort word)
        {
            return (byte)(word | 0xff00);
        }

        public byte LSB(ushort word)
        {
            return (byte)(word | 0x00ff);
        }

        #endregion

        #region Memory Addressing Mode Helpers

        public ushort GetAddressRelative(byte relativeOffset)
        {
            return this.GetAddressRelative(this.PC, relativeOffset);
        }
        public ushort GetAddressRelative(ushort startingAddress, byte relativeOffset)
        {
            byte offset = relativeOffset;
            ushort newAddress;

            if (relativeOffset > 127)
            {
                offset = (byte)~offset;
                this.PSR_CARRY = true;
                newAddress = (ushort)(startingAddress - offset);
                if (newAddress < 0)
                {
                    newAddress = (ushort)(this.Memory.MemSize + newAddress);
                }
            }
            else
            {
                newAddress = (ushort)((startingAddress + offset) % this.Memory.MemSize);
            }
            return newAddress;
        }

        public ushort GetAddressAbsolute(ushort absoluteAddress)
        {
            return absoluteAddress;
        }

        public ushort GetAddressZeroPage(byte offsetRegister)
        {
            throw new NotImplementedException();
        }

        public ushort GetAddressIndirect(ushort indirectValue)
        {
            throw new NotImplementedException();
        }

        public ushort GetAddressAbsoluteIndexed(ushort absoluteAddress, byte offsetRegister)
        {
            throw new NotImplementedException();
        }

        public ushort GetAddressZeroPageIndexed(ushort absoluteAddress, byte zeroPageIndex)
        {
            throw new NotImplementedException();
        }

        public ushort GetAddressIndexedIndirect(byte startingIndex, byte registerValue)
        {
            throw new NotImplementedException();
        }

        public ushort GetAddressIndirectIndexed(byte startingIndex, byte registerValue)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
