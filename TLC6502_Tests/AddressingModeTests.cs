using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLC6502;

namespace TLC6502.Tests
{
    [TestClass]
    public class ProcessorAddressingModeTests
    {
        private TLC6502.Processor m_Processor;

        [TestInitialize]
        public void SetUpTest()
        {
            m_Processor = new();
        }

        [TestMethod]
        public void TestAbsoluteAddressingMode()
        {

        }

        [TestMethod]
        public void TestZeroPageAddressingMode()
        {

        }

        [TestMethod]
        public void TestIndirectAddressingMode()
        {

        }

        [TestMethod]
        public void TestAbsoluteIndexedAddressingMode()
        {

        }

        [TestMethod]
        public void TestZeroPageIndexedAddressingMode()
        {

        }

        [TestMethod]
        public void TestIndexedIndirectAddressingMode()
        {

        }

        [TestMethod]
        public void TestIndirectIndexedAddressingMode()
        {

        }
    }
}
