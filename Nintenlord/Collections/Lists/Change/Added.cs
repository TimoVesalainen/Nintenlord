using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public sealed class Added<T> : IListChange<T>, IEquatable<Added<T>>
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

        public override string ToString()
        {
            return $"{{{nameof(Added<T>)} {nameof(Original)}={Original}, {nameof(Next)}={Next}, {nameof(OriginalIndex)}={OriginalIndex}, {nameof(OriginalLength)}={OriginalLength}, {nameof(NextIndex)}={NextIndex}, {nameof(NextLength)}={NextLength}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is Added<T> added && Equals(added);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(index, Original, Next, NextLength);
        }

        public bool Equals(Added<T> other)
        {
            if (other is null)
            {
                return false;
            }

            return
                index == other.index &&
                NextLength == other.NextLength &&
                Original == other.Original &&
                Next == other.Next;
        }
    }
}
