using System.Collections.Generic;
using NUnit.Framework;

namespace Jnk.BitArray.Tests
{
    public static class BitArrayExtensionsTests
    {
        public static object[] AllSetIndicesEnumeratesCorrectlyCases =
        {
            new object[] { new BitArray128(0ul, 0ul), new int[] { } },
            new object[] { new BitArray128(1ul, 0ul), new[] { 0 } },
            new object[] { new BitArray128(2ul, 0ul), new[] { 1 } },
            new object[] { new BitArray128(3ul, 0ul), new[] { 0, 1 } },
            new object[] { new BitArray128(5ul, 5ul), new[] { 0, 2, 64, 66  } },
        };

        [TestCaseSource(nameof(AllSetIndicesEnumeratesCorrectlyCases))]
        public static void AllSetIndices_EnumeratesCorrectly(BitArray128 array, int[] expected)
        {
            var actual = new List<int>();

            foreach (int index in array.AllSetIndices())
                actual.Add(index);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}