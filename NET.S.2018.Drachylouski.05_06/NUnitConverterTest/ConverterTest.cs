using System;
using NUnit.Framework;

namespace NUnitConverterTest
{
    using ConverterLogic;

    [TestFixture]
    public class ConverterTest
    {
        [TestCase("0110111101100001100001010111111", 2, ExpectedResult = 934331071)]
        [TestCase("01101111011001100001010111111", 2, ExpectedResult = 233620159)]
        [TestCase("11101101111011001100001010", 2, ExpectedResult = 62370570)]
        [TestCase("1AeF101", 16, ExpectedResult = 28242177)]
        [TestCase("1ACB67", 16, ExpectedResult = 1756007)]
        [TestCase("764241", 8, ExpectedResult = 256161)]
        [TestCase("10", 5, ExpectedResult = 5)]
        public long NUitTestCase(string source, int n) => source.ToDecimalConverter(n);


        [TestCase("1ACB67", 2)]
        [TestCase("SA123", 16)]
        public void NUnitExceptionTestCase1(string source, int n)
        {
            Assert.Throws<ArgumentException>(() => source.ToDecimalConverter(n));
        }

        [TestCase("73321", 20)]
        [TestCase("123", -4)]
        public void NUnitExceptionTestCase2(string source, int n)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => source.ToDecimalConverter(n));
        }
    }
}
