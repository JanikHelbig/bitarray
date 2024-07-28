using NUnit.Framework;

namespace Jnk.BitArray.Tests
{
    public static class BitArray128Tests
    {
        public static object[] IndexerGetCases =
        {
            new object[] { new BitArray128(0ul, 0ul), 0, false },
            new object[] { new BitArray128(1ul, 0ul), 0, true },
            new object[] { new BitArray128(0ul, 1ul), 64, true },
        };

        [TestCaseSource(nameof(IndexerGetCases))]
        public static void IndexerGet_ReturnsCorrectValue(BitArray128 array, int index, bool expected)
        {
            bool actual = array[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static object[] IndexerSetCases =
        {
            new object[] { new BitArray128(0ul, 0ul), 0, true, new BitArray128(1ul, 0ul) },
            new object[] { new BitArray128(0ul, 0ul), 64, true, new BitArray128(0ul, 1ul) },
            new object[] { new BitArray128(1ul, 0ul), 0, false, new BitArray128(0ul, 0ul) },
            new object[] { new BitArray128(0ul, 1ul), 64, false, new BitArray128(0ul, 0ul) },
        };

        [TestCaseSource(nameof(IndexerSetCases))]
        public static void IndexerSet_ResultsInCorrectValue(BitArray128 array, int index, bool value, BitArray128 expected)
        {
            array[index] = value;

            Assert.That(array, Is.EqualTo(expected));
        }

        public static object[] ShiftLeftCases =
        {
            new object[] { new BitArray128(1ul, 2ul), 0, new BitArray128(1ul, 2ul) },
            new object[] { new BitArray128(1ul, 2ul), -1, new BitArray128(0, 1ul << 63) },
            new object[] { new BitArray128(1ul, 2ul), 128, new BitArray128(1ul, 2ul) },
            new object[] { new BitArray128(1ul, 2ul), 129, new BitArray128(2ul, 4ul) },
            new object[] { new BitArray128(1ul, 0ul), 1, new BitArray128(2ul, 0ul) },
            new object[] { new BitArray128(1ul, 0ul), 65, new BitArray128(0ul, 2ul) },
        };

        [TestCaseSource(nameof(ShiftLeftCases))]
        public static void ShiftLeftOperator_ResultsInCorrectValue(BitArray128 array, int shiftAmount, BitArray128 expected)
        {
            BitArray128 actual = array << shiftAmount;

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static object[] ShiftRightCases =
        {
            new object[] { new BitArray128(1ul, 2ul), 0, new BitArray128(1ul, 2ul) },
            new object[] { new BitArray128(0ul, 1ul << 63), -1, new BitArray128(1ul, 0ul) },
            new object[] { new BitArray128(1ul, 2ul), 128, new BitArray128(1ul, 2ul) },
            new object[] { new BitArray128(2ul, 4ul), 129, new BitArray128(1ul, 2ul) },
            new object[] { new BitArray128(2ul, 0ul), 1, new BitArray128(1ul, 0ul) },
            new object[] { new BitArray128(0ul, 2ul), 65, new BitArray128(1ul, 0ul) },
        };

        [TestCaseSource(nameof(ShiftRightCases))]
        public static void ShiftRightOperator_ResultsInCorrectValue(BitArray128 array, int shiftAmount, BitArray128 expected)
        {
            BitArray128 actual = array >> shiftAmount;

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static object[] GetTrailingZerosCases =
        {
            new object[] { new BitArray128(0ul, 0ul), 0 },
            new object[] { new BitArray128(1ul, 0ul), 0 },
            new object[] { new BitArray128(2ul, 0ul), 1 },
            new object[] { new BitArray128(4ul, 0ul), 2 },
            new object[] { new BitArray128(0ul, 1ul), 64 },
            new object[] { new BitArray128(0ul, ulong.MaxValue), 64 },
            new object[] { new BitArray128(0ul, 2ul), 65 },
            new object[] { new BitArray128(0ul, 1ul << 63), 127 },
        };

        [TestCaseSource(nameof(GetTrailingZerosCases))]
        public static void GetTrailingZeros_ReturnsCorrectValue(BitArray128 array, int expected)
        {
            int actual = array.CountTrailingZeros();

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static object[] ToStringCases =
        {
            new object[] { new BitArray128(0ul, 0ul), "00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000" },
            new object[] { new BitArray128(1ul, 0ul), "00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000001" },
            new object[] { new BitArray128(2ul, 0ul), "00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000000.00000010" },
            new object[] { new BitArray128(1ul + 4ul + 16ul + 64, 1ul + 4ul + 16ul + 64ul), "00000000.00000000.00000000.00000000.00000000.00000000.00000000.01010101.00000000.00000000.00000000.00000000.00000000.00000000.00000000.01010101" },
            new object[] { new BitArray128(ulong.MaxValue, ulong.MaxValue), "11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111.11111111" },
        };

        [TestCaseSource(nameof(ToStringCases))]
        public static void ToString_ReturnsCorrectValue(BitArray128 input, string expected)
        {
            var actual = input.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}