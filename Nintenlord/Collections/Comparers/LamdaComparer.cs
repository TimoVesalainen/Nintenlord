using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public class LamdaComparer<T> : IComparer<T>, IEqualityComparer<T>
    {
        private readonly Func<T, T, int> f;

        public LamdaComparer(Func<T, T, int> f)
        {
            this.f = f;
        }
        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            return f(x, y);
        }

        #endregion

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            return f(x, y) == 0;
        }

        public int GetHashCode(T obj)
        {
            return f(default, obj);
        }

        #endregion
    }
}
