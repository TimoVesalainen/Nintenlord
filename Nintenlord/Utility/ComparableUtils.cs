using System;

namespace Nintenlord.Utility
{
    static public class ComparableUtils
    {
        public static T Min<T>(T a, T b) where T : IComparable<T>
        {
            var comparison = a.CompareTo(b);

            if (comparison <= 0)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public static T? MinNullIsLargest<T>(T? a, T? b) where T : struct, IComparable<T>
        {
            if (a is T aVal && b is T bVal)
            {
                return Min(aVal, bVal);
            }
            else
            {
                return a ?? b;
            }
        }

        public static T? MinNullIsSmallest<T>(T? a, T? b) where T : struct, IComparable<T>
        {
            if (a is T aVal && b is T bVal)
            {
                return Min(aVal, bVal);
            }
            else
            {
                return null;
            }
        }

        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            var comparison = a.CompareTo(b);

            if (comparison >= 0)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public static T? MaxNullIsLargest<T>(T? a, T? b) where T : struct, IComparable<T>
        {
            if (a is T aVal && b is T bVal)
            {
                return Max(aVal, bVal);
            }
            else
            {
                return null;
            }
        }

        public static T? MaxNullIsSmallest<T>(T? a, T? b) where T : struct, IComparable<T>
        {
            if (a is T aVal && b is T bVal)
            {
                return Max(aVal, bVal);
            }
            else
            {
                return a ?? b;
            }
        }
    }
}
