using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nintenlord.Collections.Lists
{
    public readonly struct ReadOnlyListAccess<T> : IReadOnlyList<T>, IEquatable<ReadOnlyListAccess<T>>
    {
        private readonly IList<T> list;

        public ReadOnlyListAccess(IList<T> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public T this[int index] => list[index];

        public int Count => list.Count;

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is ReadOnlyListAccess<T> list)
            {
                return Equals(list);
            }
            return false;
        }

        public bool Equals(ReadOnlyListAccess<T> other)
        {
            return this.list == other.list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return list.GetHashCode();
        }

        public override string ToString()
        {
            return list.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public static bool operator ==(ReadOnlyListAccess<T> left, ReadOnlyListAccess<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ReadOnlyListAccess<T> left, ReadOnlyListAccess<T> right)
        {
            return !(left == right);
        }
    }
}
