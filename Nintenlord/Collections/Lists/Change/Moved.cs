using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public sealed class Moved<T> : IListChange<T>, IEquatable<Moved<T>>
    {
        private readonly int length;

        public IReadOnlyList<T> Original { get; }
        public IReadOnlyList<T> Next { get; }
        public int OriginalIndex { get; }
        public int OriginalLength => length;
        public int NextIndex { get; }
        public int NextLength => length;

        public Moved(int length, IReadOnlyList<T> original, IReadOnlyList<T> next, int originalIndex, int nextIndex)
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

        public override string ToString()
        {
            return $"{{{nameof(Moved<T>)} {nameof(Original)}={Original}, {nameof(Next)}={Next}, {nameof(OriginalIndex)}={OriginalIndex}, {nameof(OriginalLength)}={OriginalLength}, {nameof(NextIndex)}={NextIndex}, {nameof(NextLength)}={NextLength}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is Moved<T> moved && Equals(moved);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(length, Original, Next, OriginalIndex, NextIndex);
        }

        public bool Equals(Moved<T> other)
        {
            if (other is null)
            {
                return false;
            }

            return
                length == other.length &&
                Original == other.Original &&
                Next == other.Next &&
                OriginalIndex == other.OriginalIndex &&
                NextIndex == other.NextIndex;
        }
    }
}
