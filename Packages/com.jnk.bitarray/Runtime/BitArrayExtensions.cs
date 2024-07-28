namespace Jnk.BitArray
{
    public static class BitArrayExtensions
    {
        public static SetIndicesEnumerable<T> AllSetIndices<T>(this T array) where T : struct, IBitArray => new(array);
    }
}