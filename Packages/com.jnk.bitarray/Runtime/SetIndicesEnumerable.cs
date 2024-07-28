using System;

namespace Jnk.BitArray
{
    public readonly struct SetIndicesEnumerable<T> where T : IBitArray
    {
        private readonly T _array;

        public SetIndicesEnumerable(T array)
        {
            _array = array;
        }

        public SetIndicesEnumerator<T> GetEnumerator() => new(_array);
    }

    public ref struct SetIndicesEnumerator<T> where T : IBitArray
    {
        private T _array;
        private int _current;

        public readonly int Current => _current;

        public SetIndicesEnumerator(T array)
        {
            _array = array;
            _current = -1;
        }

        public bool MoveNext()
        {
            // TODO: Is is possible to further reduce branching here?
            if (_current >= 0)
                _array[_current] = false;

            _current = _array.CountTrailingZeros();
            return !_array.AllFalse;
        }

        public void Reset() => throw new NotSupportedException();
    }
}