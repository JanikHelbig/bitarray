namespace Jnk.BitArray
{
    public interface IBitArray
    {
        public int Length { get; }
        public bool AllFalse { get; }
        public bool AllTrue { get; }
        public bool this[int index] { get; set; }
        public int CountSetIndices();
        public int CountTrailingZeros();
    }
}