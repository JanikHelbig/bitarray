using System;

namespace Jnk.BitArray
{
    public struct BitArray128 : IBitArray
    {
        public int Length => 128;

        private ulong _data0;
        private ulong _data1;

        public readonly bool AllFalse => _data0 == 0ul && _data1 == 0ul;

        public readonly bool AllTrue => _data0 == ulong.MaxValue && _data1 == ulong.MaxValue;

        public BitArray128(ulong data0, ulong data1)
        {
            _data0 = data0;
            _data1 = data1;
        }

        public bool this[int index]
        {
            get
            {
                if (index is < 0 or > 127)
                    throw new ArgumentOutOfRangeException();

                return index < 64
                    ? (_data0 & (1ul << index)) != 0ul
                    : (_data1 & (1ul << index - 64)) != 0ul;
            }

            set
            {
                if (index is < 0 or > 127)
                    throw new ArgumentOutOfRangeException();

                if (index < 64)
                    _data0 = value ? _data0 | (1ul << index) : _data0 & ~(1ul << index);
                else
                    _data1 = value ? _data1 | (1ul << index - 64) : _data1 & ~(1ul << index - 64);
            }
        }

        public bool this[uint index]
        {
            get
            {
                if (index > 127)
                    throw new ArgumentOutOfRangeException();

                return index < 64
                    ? (_data0 & (1ul << (int)index)) != 0ul
                    : (_data1 & (1ul << (int)index - 64)) != 0ul;
            }

            set
            {
                if (index > 127)
                    throw new ArgumentOutOfRangeException();

                if (index < 64)
                    _data0 = value ? _data0 | (1ul << (int)index) : _data0 & ~(1ul << (int)index);
                else
                    _data1 = value ? _data1 | (1ul << (int)index - 64) : _data1 & ~(1ul << (int)index - 64);
            }
        }

        public bool Equals(BitArray128 other) => this == other;

        public override bool Equals(object obj) => obj is BitArray128 other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (_data0.GetHashCode() * 397) ^ _data1.GetHashCode();
            }
        }

        public static bool operator ==(BitArray128 a, BitArray128 b) => a._data0 == b._data0 && a._data1 == b._data1;

        public static bool operator !=(BitArray128 a, BitArray128 b) => a._data0 != b._data0 || a._data1 != b._data1;

        public static BitArray128 operator ~(BitArray128 a) => new(~a._data0, ~a._data1);

        public static BitArray128 operator &(BitArray128 a, BitArray128 b) => new(a._data0 & b._data0, a._data1 & b._data1);

        public static BitArray128 operator |(BitArray128 a, BitArray128 b) => new(a._data0 | b._data0, a._data1 | b._data1);

        public static BitArray128 operator ^(BitArray128 a, BitArray128 b) => new(a._data0 ^ b._data0, a._data1 ^ b._data1);

        public static BitArray128 operator <<(BitArray128 value, int shiftAmount)
        {
            // Only keep 7 lowest order bits to match behaviour of built-in shift operator.
            shiftAmount &= 0b_00000000_00000000_00000000_01111111;

            if (shiftAmount > 63)
            {
                value._data1 = value._data0;
                value._data0 = 0ul;
                shiftAmount -= 64;
            }

            ulong data0 = value._data0 << shiftAmount;
            ulong data1 = (value._data1 << shiftAmount) | ((value._data0 >> (64 - shiftAmount)) & ~(ulong.MaxValue << shiftAmount));

            return new BitArray128(data0, data1);
        }

        public static BitArray128 operator >>(BitArray128 value, int shiftAmount)
        {
            // Only keep 7 lowest order bits to match behaviour of built-in shift operator.
            shiftAmount &= 0b_00000000_00000000_00000000_01111111;

            if (shiftAmount > 63)
            {
                value._data0 = value._data1;
                value._data1 = 0ul;
                shiftAmount -= 64;
            }

            ulong data0 = (value._data0 >> shiftAmount) | ((value._data1 << (64 - shiftAmount)) & ~(ulong.MaxValue >> shiftAmount));
            ulong data1 = value._data1 >> shiftAmount;
            return new BitArray128(data0, data1);
        }

        public readonly int CountSetIndices() => BitUtils.CountSetBits(_data0) + BitUtils.CountSetBits(_data1);

        public readonly int CountTrailingZeros()
        {
            if (_data0 != 0)
                return BitUtils.CountTrailingZeros(_data0);
            if (_data1 != 0)
                return BitUtils.CountTrailingZeros(_data1) + 64;
            return 0;
        }

        public override string ToString()
        {
            Span<char> buffer = stackalloc char[128 + 15];

            var digitIndex = 0;
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = (i + 1) % 9 == 0 ? '.' : this[digitIndex++] ? '1' : '0';

            buffer.Reverse();
            return new string(buffer);
        }
    }
}