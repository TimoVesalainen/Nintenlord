using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public sealed class Added<T> : IListChange<T>
    {
        readonly int index;

        public IList<T> Original { get; }

        public IList<T> Next { get; }

        public int OriginalIndex => index;

        public int OriginalLength => 0;

        public int NextIndex => index;

        public int NextLength { get; }

        public Added(int index, IList<T> original, IList<T> next, int nextLength)
        {
            this.index = index;
            Original = original;
            Next = next;
            NextLength = nextLength;
        }

        public IEnumerable<T> AddedItems()
        {
            for (int i = 0; i < NextLength; i++)
            {
                yield return Next[i + index];
            }
        }
    }
}
