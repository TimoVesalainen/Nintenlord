using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public sealed class LexicographicComparer<T> : IComparer<IEnumerable<T>>
    {
        private static readonly ConcurrentDictionary<IComparer<T>, LexicographicComparer<T>> cache = new();

        public static LexicographicComparer<T> Default => Create(Comparer<T>.Default);

        readonly IComparer<T> ordering;

        public static LexicographicComparer<T> Create(IComparer<T> ordering)
        {
            if (ordering is null)
            {
                throw new ArgumentNullException(nameof(ordering));
            }

            return cache.GetOrAdd(ordering, def => new LexicographicComparer<T>(def));
        }

        private LexicographicComparer(IComparer<T> ordering)
        {
            this.ordering = ordering;
        }

        public int Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y is null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            using var xEnumerator = x.GetEnumerator();
            using var yEnumerator = y.GetEnumerator();
            bool xHasValue = xEnumerator.MoveNext();
            bool yHasValue = yEnumerator.MoveNext();
            while (xHasValue && yHasValue)
            {
                var charComparison = ordering.Compare(xEnumerator.Current, yEnumerator.Current);
                if (charComparison != 0)
                {
                    return charComparison;
                }

                xHasValue = xEnumerator.MoveNext();
                yHasValue = yEnumerator.MoveNext();
            }

            if (xHasValue == yHasValue)
            {
                return 0;
            }
            else if (xHasValue)
            {
                return 1;
            }
            else if (yHasValue)
            {
                return -1;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
