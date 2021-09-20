using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public sealed class Moved<T> : IListChange<T>
    {
        private readonly int length;

        public IList<T> Original { get; }
        public IList<T> Next { get; }
        public int OriginalIndex { get; }
        public int OriginalLength => length;
        public int NextIndex { get; }
        public int NextLength => length;

        public Moved(int length, IList<T> original, IList<T> next, int originalIndex, int nextIndex)
        {
            this.length = length;
            Original = original;
            Next = next;
            OriginalIndex = originalIndex;
            NextIndex = nextIndex;
        }

        public IEnumerable<T> MovedItems()
        {
            for (int i = 0; i < length; i++)
            {
                yield return Original[i + OriginalIndex];
            }
        }
    }
}
