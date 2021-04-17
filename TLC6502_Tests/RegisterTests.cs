using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLC6502;

namespace TLC6502.Tests
{
    [TestClass]
    public class ProcessorRegisterTests
    {
        private TLC6502.Processor m_Processor;

        [TestInitialize]
        public void SetUpTest()
        {
            m_Processor = new();
        }
        [TestMethod]
        public void CanInstantiate()
        {
            Assert.IsNotNull(m_Processor);
            Assert.IsInstanceOfType(m_Processor, typeof(TLC6502.Processor));
        }
        [TestMethod]
        public void TestCanSetGeneralRegisters()
        {
            m_Processor.X = (byte)255;
            m_Processor.Y = (byte)128;
            m_Processor.A = (byte)64;

            Assert.AreEqual((byte)255, m_Processor.X);
            Assert.AreEqual((byte)128, m_Processor.Y);
            Assert.AreEqual((byte)64, m_Processor.A);
        }

        [TestMethod]
        public void TestCanSetAndReadProgramCounter()
        {
            m_Processor.PC = 0x1234;
            Assert.AreEqual(0x1234, m_Processor.PC);
        }

        [TestMethod]
        public void TestCanSetAndReadStackPointer()
        {
            m_Processor.SP = (byte)0xf0;
            Assert.AreEqual(0xf0, m_Processor.SP);
        }

        [TestMethod]
        public void TestCanAccessProcessorFlagRegister()
        {
            m_Processor.PSR = (byte)0xff;
            Assert.AreEqual((byte)0xff, m_Processor.PSR);
        }
        [TestMethod]
        public void TestCanAccessProcessorFlagAccessors()
        {
            m_Processor.PSR = (byte)0xff;
            Assert.AreEqual((byte)0xff, m_Processor.PSR);
            Assert.IsTrue(m_Processor.PSR_B1);
            Assert.IsTrue(m_Processor.PSR_B2);
            Assert.IsTrue(m_Processor.PSR_CARRY);
            Assert.IsTrue(m_Processor.PSR_DECIMAL);
            Assert.IsTrue(m_Processor.PSR_INTERRUPT_DISABLE);
            Assert.IsTrue(m_Processor.PSR_NEGATIVE);
            Assert.IsTrue(m_Processor.PSR_OVERFLOW);
            Assert.IsTrue(m_Processor.PSR_ZERO);

            m_Processor.PSR = (byte)0x00;
            Assert.AreEqual((byte)0x00, m_Processor.PSR);
            Assert.IsFalse(m_Processor.PSR_B1);
            Assert.IsFalse(m_Processor.PSR_B2);
            Assert.IsFalse(m_Processor.PSR_CARRY);
            Assert.IsFalse(m_Processor.PSR_DECIMAL);
            Assert.IsFalse(m_Processor.PSR_INTERRUPT_DISABLE);
            Assert.IsFalse(m_Processor.PSR_NEGATIVE);
            Assert.IsFalse(m_Processor.PSR_OVERFLOW);
            Assert.IsFalse(m_Processor.PSR_ZERO);
        }

        [TestMethod]
        public void TestCanSetAndClearProcessorFlags()
        {
            m_Processor.PSR = (byte)0x00;
            m_Processor.PSR_ZERO = true;
            m_Processor.PSR_CARRY = true;
            m_Processor.PSR_B1 = true;
            m_Processor.PSR_B2 = true;
            m_Processor.PSR_DECIMAL = true;
            m_Processor.PSR_INTERRUPT_DISABLE = true;
            m_Processor.PSR_NEGATIVE = true;
            m_Processor.PSR_OVERFLOW = true;
            Assert.AreEqual((byte)0xff, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_NEGATIVE = false;
            Assert.AreEqual(0x7f, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_OVERFLOW = false;
            Assert.AreEqual(0xbf, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_B2 = false;
            Assert.AreEqual(0xdf, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_B1 = false;
            Assert.AreEqual(0xef, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_DECIMAL = false;
            Assert.AreEqual(0xf7, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_INTERRUPT_DISABLE = false;
            Assert.AreEqual(0xfb, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_ZERO = false;
            Assert.AreEqual(0xfd, m_Processor.PSR);

            m_Processor.PSR = 0xff;
            m_Processor.PSR_CARRY = false;
            Assert.AreEqual(0xfe, m_Processor.PSR);
        }
    }
}
