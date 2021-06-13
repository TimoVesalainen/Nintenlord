using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Collections.EqualityComparer
{
    public static class EqualityCompareUtils
    {
        public static IEqualityComparer<TOuter> Select<T, TOuter>(this IEqualityComparer<T> comparer, Func<TOuter, T> selector)
        {
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
        public static IEqualityComparer<T[]> ToListComparer<T>(this IEqualityComparer<T> comparer)
        {
            return new ArrayEqualityComparer<T>(comparer);
        }
    }
}
