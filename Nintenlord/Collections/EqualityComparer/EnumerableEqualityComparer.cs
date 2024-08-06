using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class EnumerableEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        private static readonly ConcurrentDictionary<IEqualityComparer<T>, EnumerableEqualityComparer<T>> cache = new();

        public static EnumerableEqualityComparer<T> Default => Create(EqualityComparer<T>.Default);

        public static EnumerableEqualityComparer<T> Create(IEqualityComparer<T> itemEqualityComparer)
        {
            if (itemEqualityComparer is null)
            {
                throw new ArgumentNullException(nameof(itemEqualityComparer));
            }

            return cache.GetOrAdd(itemEqualityComparer, eq => new EnumerableEqualityComparer<T>(eq));
        }

        readonly IEqualityComparer<T> itemEqualityComparer;

        private EnumerableEqualityComparer(IEqualityComparer<T> itemEqualityComparer)
        {
            this.itemEqualityComparer = itemEqualityComparer;
        }

        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            var hasXLength = x.TryGetNonEnumeratedCount(out var xLength);
            var hasYLength = x.TryGetNonEnumeratedCount(out var yLength);

            if (hasXLength && hasYLength)
            {
                if (hasXLength != hasYLength)
                {
                    return false;
                }
            }

            using var xEnumerator = x.GetEnumerator();
            using var yEnumerator = y.GetEnumerator();

            var hasNextX = xEnumerator.MoveNext();
            var hasNextY = yEnumerator.MoveNext();

            while (hasNextX && hasNextY)
            {
                if (!itemEqualityComparer.Equals(xEnumerator.Current, yEnumerator.Current))
                {
                    return false;
                }
                hasNextX = xEnumerator.MoveNext();
                hasNextY = yEnumerator.MoveNext();
            }

            return hasNextX == hasNextY;
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            unchecked
            {
                int code = 0;

                foreach (var ob in obj)
                {
                    code *= 11;
                    code += itemEqualityComparer.GetHashCode(ob);
                }

                return code;
            }
        }
    }
}
