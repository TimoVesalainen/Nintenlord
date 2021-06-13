using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public sealed class LambdaComparer<T> : IComparer<T>, IEqualityComparer<T>
    {
        private readonly Func<T, T, int> f;

        public LambdaComparer(Func<T, T, int> f)
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
