namespace Jnk.BitArray
{
    public static class BitUtils
    {
        private const ulong DeBruijnSequence = 0b_0000001011000100101000011001000111111010011101101010111100110111;
        private static readonly int[] DeBruijnLookup = { 0, 1, 2, 19, 10, 3, 20, 28, 25, 11, 14, 4, 21, 56, 39, 29, 17, 26, 12, 37, 15, 47, 5, 49, 7, 22, 44, 57, 60, 40, 51, 30, 63, 18, 9, 27, 24, 13, 55, 38, 16, 36, 46, 48, 6, 43, 59, 50, 62, 8, 23, 54, 35, 45, 42, 58, 61, 53, 34, 41, 52, 33, 32, 31 };

        /// <summary>
        /// Determines the number of consecutive zero bits on right (tailing) in an unsigned 64-bit value.
        /// </summary>
        /// <param name="value">The value to count trailing zeros of.</param>
        /// <returns>The number of trailing zeros. When no bits are set it also returns 0.</returns>
        public static int CountTrailingZeros(ulong value) => DeBruijnLookup[((value & (~value + 1ul)) * DeBruijnSequence) >> 58];

        /// <summary>
        /// Count the number of set bits in a 64-bit integer.
        /// </summary>
        /// <param name="v">The value to count the set bits of.</param>
        /// <returns>The number of set bits.</returns>
        public static int CountSetBits(ulong v)
        {
            const ulong magic0 = 0x55555555_55555555;
            const ulong magic1 = 0x33333333_33333333;
            const ulong magic2 = 0x0F0F0F0F_0F0F0F0F;
            const ulong magic3 = 0x01010101_01010101;

            v -= (v >> 1) & magic0;
            v = (v & magic1) + ((v >> 2) & magic1);
            v = (v + (v >> 4)) & magic2;
            return (int)((v * magic3) >> (sizeof(ulong) - 1) * 8);
        }
    }
}