using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Comparers
{
    public sealed class AlphabeticComparer<T> : IComparer<IEnumerable<T>>
    {
        private static readonly ConcurrentDictionary<IComparer<T>, AlphabeticComparer<T>> cache = new();

        public static AlphabeticComparer<T> Default => Create(Comparer<T>.Default);

        public static AlphabeticComparer<T> Create(IComparer<T> ordering)
        {
            if (ordering is null)
            {
                throw new ArgumentNullException(nameof(ordering));
            }

            return cache.GetOrAdd(ordering, def => new AlphabeticComparer<T>(def));
        }

        readonly LexicographicComparer<T> lexicographicComparer;
        private readonly IComparer<T> ordering;

        private AlphabeticComparer(IComparer<T> ordering)
        {
            lexicographicComparer = LexicographicComparer<T>.Create(ordering);
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

            if (x.TryGetNonEnumeratedCount(out var xCount) && y.TryGetNonEnumeratedCount(out var yCount))
            {
                if (xCount < yCount)
                {
                    return -1;
                }
                else if (xCount > yCount)
                {
                    return 1;
                }
                else
                {
                    return lexicographicComparer.Compare(x, y);
                }
            }
            else
            {
                using var xEnumerator = x.GetEnumerator();
                using var yEnumerator = y.GetEnumerator();
                bool xHasValue = xEnumerator.MoveNext();
                bool yHasValue = yEnumerator.MoveNext();
                int? comparison = null;
                while (xHasValue && yHasValue)
                {
                    var charComparison = ordering.Compare(xEnumerator.Current, yEnumerator.Current);
                    if (charComparison != 0)
                    {
                        comparison ??= charComparison;
                    }

                    xHasValue = xEnumerator.MoveNext();
                    yHasValue = yEnumerator.MoveNext();
                }

                if (xHasValue == yHasValue)
                {
                    return comparison ?? 0;
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
}
