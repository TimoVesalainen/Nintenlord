using System;
using System.Collections.Generic;
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
    }
}
