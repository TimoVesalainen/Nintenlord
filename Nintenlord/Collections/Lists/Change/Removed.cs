using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public sealed class Removed<T> : IListChange<T>, IEquatable<Removed<T>>
    {
        readonly int index;

        public IReadOnlyList<T> Original { get; }

        public IReadOnlyList<T> Next { get; }

        public int OriginalIndex => index;

        public int OriginalLength { get; }

        public int NextIndex => index;

        public int NextLength => 0;

        public Removed(int index, IReadOnlyList<T> original, IReadOnlyList<T> next, int originalLength)
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

        public override string ToString()
        {
            return $"{{{nameof(Removed<T>)} {nameof(Original)}={Original}, {nameof(Next)}={Next}, {nameof(OriginalIndex)}={OriginalIndex}, {nameof(OriginalLength)}={OriginalLength}, {nameof(NextIndex)}={NextIndex}, {nameof(NextLength)}={NextLength}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is Removed<T> removed && Equals(removed);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(index, Original, Next, OriginalLength);
        }

        public bool Equals(Removed<T> other)
        {
            if (other is null)
            {
                return false;
            }

            return
                index == other.index &&
                OriginalLength == other.OriginalLength &&
                Original == other.Original &&
                Next == other.Next;
        }
    }
}
