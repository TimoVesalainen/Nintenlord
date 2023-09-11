using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.EqualityComparer
{
    public static class EqualityCompareUtils
    {
        public static IEqualityComparer<TOuter> Select<T, TOuter>(this IEqualityComparer<T> comparer, Func<TOuter, T> selector)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return new SelectEqualityComparer<TOuter, T>(selector, comparer);
        }

        public static IEqualityComparer<T> ToEqualityComparer<T>(this IComparer<T> comparer)
        {
            if (comparer is IEqualityComparer<T> equalityComparer)
            {
                return equalityComparer;
            }
            else
            {
                return new ComparerEqualityComparer<T>(comparer);
            }
        }

        public static IEqualityComparer<T[]> ToArrayComparer<T>(this IEqualityComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return EnumerableEqualityComparer<T>.Create(comparer);
        }

        public static IEqualityComparer<List<T>> ToListComparer<T>(this IEqualityComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return EnumerableEqualityComparer<T>.Create(comparer);
        }
    }
}
