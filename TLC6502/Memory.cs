using System;
using System.Collections.Generic;
using System.IO;

namespace TLC6502
{
    public class Memory
    {
        #region Configuration Constants
        public const ushort DEFAULT_MEM_SIZE = (ushort)65535;
        #endregion

        #region Instance Variables
        private byte[] m_memory;
        private ushort m_memSize;
        #endregion

        #region Constructors
        public Memory(ushort MemSize)
        {
            this.MemoryBytes = new byte[MemSize + 1];
            for (ushort i = 0; i < MemSize; i++)
                this.SetByte(i, 0xff);
            this.MemSize = MemSize;
        }

        public Memory() : this(DEFAULT_MEM_SIZE) { }
        #endregion

        #region Property Accessors
        public byte[] MemoryBytes { get => m_memory; set => m_memory = value; }
        public ushort MemSize { get => m_memSize; set => m_memSize = value; }
        #endregion

        #region Memory Access Helpers
        public void SetByte(ushort address, byte value)
        {
            MemoryBytes[address] = value;
        }

        public void SetWord(ushort address, Int16 value)
        {
            SetByte(address, (byte)(value & 0xff));
            SetByte((ushort)(address + 1), (byte)(value >> 8));
        }

        public byte GetByte(ushort address)
        {
            return MemoryBytes[address];
        }

        public ushort GetWord(ushort address)
        {
            byte a = GetByte(address);
            byte b = GetByte((ushort)(address + 1));
            return (ushort)((b << 8) | a);
        }

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

        #region Memory Load/Save Helpers
        public void Load(ushort startingAddress, byte[] data, int bufSize)
        {
            if (bufSize == 0)
                bufSize = data.Length;
            bufSize = Math.Min(bufSize, MemSize - startingAddress);
            Buffer.BlockCopy(data, 0, this.MemoryBytes, startingAddress, bufSize);
        }

        public void Load(Stream stream, ushort startingAddress, int size)
        {
            if (size <= 0)
                size = (int)(stream.Length - stream.Position);
            size = Math.Min(size, this.MemSize - startingAddress);
            stream.Read(this.MemoryBytes, startingAddress, size);
        }

        public void Save(Stream stream, ushort startingAddress, int size)
        {
            if (size <= 0)
                size = (int)this.MemSize - startingAddress;
            byte[] memChunk = new byte[size];
            Buffer.BlockCopy(this.MemoryBytes, startingAddress, memChunk, 0, size);
            stream.Write(memChunk, 0, size);
        }
        #endregion
    }
}
