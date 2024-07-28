using NUnit.Framework;

namespace Jnk.BitArray.Tests
{
    public static class BitUtilsTests
    {
        [TestCase(0b_00001001_00100100ul, 4)]
        [TestCase(~0ul, 64)]
        public static void CountSetBits_ReturnsCorrectValue(ulong value, int expected)
        {
            int actual = BitUtils.CountSetBits(value);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}