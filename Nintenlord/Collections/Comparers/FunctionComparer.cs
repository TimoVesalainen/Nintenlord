using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public sealed class FunctionComparer<T> : IComparer<T>
    {
        private readonly Func<T, int> valueFunction;
        private static IComparer<int> comparer = Comparer<int>.Default;

        public FunctionComparer(Func<T, int> valueFunction)
        {
            this.valueFunction = valueFunction;
        }

        #region IComparer<Node> Members

        public int Compare(T x, T y)
        {
            return comparer.Compare(valueFunction(x), valueFunction(y));
        }

        #endregion

        public static explicit operator FunctionComparer<T>(Func<T, int> valueFunction)
        {
            return new FunctionComparer<T>(valueFunction);
        }
    }
}
