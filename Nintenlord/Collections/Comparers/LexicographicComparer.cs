using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public sealed class LexicographicComparer<T> : IComparer<IEnumerable<T>>
    {
        readonly IComparer<T> ordering;

        public LexicographicComparer(IComparer<T> ordering)
        {
            this.ordering = ordering;
        }

        public int Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
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
