using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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

        private AlphabeticComparer(IComparer<T> ordering)
        {
            lexicographicComparer = LexicographicComparer<T>.Create(ordering);
        }

        public int Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
            var xLength = x.Count();
            var yLength = y.Count();

            if (xLength < yLength)
            {
                return -1;
            }
            else if (xLength > yLength)
            {
                return 1;
            }
            else
            {
                return lexicographicComparer.Compare(x, y);
            }
        }
    }
}
