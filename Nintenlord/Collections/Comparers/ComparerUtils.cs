using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Collections.Comparers
{
    public static class ComparerUtils
    {
        public static T Max<T>(this IComparer<T> comparer, T item1, T item2)
        {
            var comparison = comparer.Compare(item1, item2);

            if (comparison >= 0)
            {
                return item1;
            }
            else
            {
                return item2;
            }
        }

        public static T Min<T>(this IComparer<T> comparer, T item1, T item2)
        {
            var comparison = comparer.Compare(item1, item2);

            if (comparison <= 0)
            {
                return item1;
            }
            else
            {
                return item2;
            }
        }

        public static IComparer<TOuter> Select<T, TOuter>(this IComparer<T> comparer, Func<TOuter, T> selector)
        {
            return new SelectComparer<TOuter, T>(selector, comparer);
        }

        public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            return new ReverseComparer<T>(comparer);
        }

        public static IComparer<T> Then<T>(this IComparer<T> comparer, params IComparer<T>[] parameters)
        {
            return new SequentialComparer<T>(parameters.Prepend(comparer));
        }
    }
}
