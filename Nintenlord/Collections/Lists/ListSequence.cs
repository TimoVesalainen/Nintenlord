using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Lists
{
    public readonly ref struct ListSequence<T>
    {
        public int Count { get; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Invalid index {index} with list length {Count}");
                }
                return list[ToIndex(index)];
            }
        }

        private readonly int ToIndex(int index)
        {
            return start + index * step;
        }

        readonly IReadOnlyList<T> list;
        readonly int start;
        readonly int step;

        public ListSequence(IReadOnlyList<T> list, int start, int count, int step = 1)
        {
            Count = count;
            this.list = list;
            this.start = start;
            this.step = step;
        }
    }
}
