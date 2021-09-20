using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public sealed class Removed<T> : IListChange<T>
    {
        readonly int index;

        public IList<T> Original { get; }

        public IList<T> Next { get; }

        public int OriginalIndex => index;

        public int OriginalLength { get; }

        public int NextIndex => index;

        public int NextLength => 0;

        public Removed(int index, IList<T> original, IList<T> next, int originalLength)
        {
            this.index = index;
            Original = original;
            Next = next;
            OriginalLength = originalLength;
        }

        public IEnumerable<T> RemovedItems()
        {
            for (int i = 0; i < OriginalLength; i++)
            {
                yield return Original[i + index];
            }
        }
    }
}
