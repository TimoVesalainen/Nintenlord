using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public sealed class ReverseComparer<T> : IComparer<T>
    {
        private static readonly ConcurrentDictionary<IComparer<T>, ReverseComparer<T>> cache = new();

        public static ReverseComparer<T> Default => Create(Comparer<T>.Default) as ReverseComparer<T>;

        private readonly IComparer<T> baseComparer;

        public static IComparer<T> Create(IComparer<T> comparer)
        {
            if (comparer is ReverseComparer<T> reverseComparer)
            {
                return reverseComparer.baseComparer;
            }

            return cache.GetOrAdd(comparer, c => new ReverseComparer<T>(c));
        }

        public ReverseComparer(IComparer<T> baseComparer)
        {
            this.baseComparer = baseComparer ?? throw new ArgumentNullException(nameof(baseComparer));
        }

        public int Compare(T x, T y)
        {
            return baseComparer.Compare(y, x);
        }
    }
}
