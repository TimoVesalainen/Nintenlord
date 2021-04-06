using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public class ReverseComparer<T> : IComparer<T>
    {
        private static ReverseComparer<T> defaultComparer;
        public static ReverseComparer<T> Default => defaultComparer ?? (defaultComparer = new ReverseComparer<T>(Comparer<T>.Default));

        private readonly IComparer<T> baseComparer;

        public ReverseComparer(IComparer<T> baseComparer)
        {
            if (baseComparer == null)
            {
                throw new ArgumentNullException(nameof(baseComparer));
            }

            this.baseComparer = baseComparer;
        }

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            return baseComparer.Compare(y, x);
        }

        #endregion
    }
}
