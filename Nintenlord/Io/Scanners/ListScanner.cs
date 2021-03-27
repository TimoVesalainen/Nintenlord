using System.Collections.Generic;

namespace Nintenlord.IO.Scanners
{
    public sealed class ListScanner<T> : IStoringScanner<T>
    {
        private readonly IList<T> list;
        private int currentOffset;

        public ListScanner(IList<T> list)
        {
            this.list = list;
        }

        public T this[long offset] => list[(int)offset];

        public bool IsStored(long offset)
        {
            return offset >= 0 && offset < list.Count;
        }

        public bool IsStored(long offset, long length)
        {
            return offset >= 0 && offset + length <= list.Count;
        }

        public bool IsAtEnd => currentOffset == list.Count;

        public long Offset
        {
            get => currentOffset;
            set => currentOffset = (int)value;
        }

        public bool CanSeek => true;

        public T Current => list[currentOffset];

        public bool MoveNext()
        {
            currentOffset++;
            return currentOffset < list.Count;
        }
    }
}
