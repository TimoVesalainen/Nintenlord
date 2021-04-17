using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Comparers
{
    public sealed class SelectComparer<T, TInner> : IComparer<T>
    {
        private readonly Func<T, TInner> valueFunction;
        private readonly IComparer<TInner> innerComparer;

        public SelectComparer(Func<T, TInner> valueFunction, IComparer<TInner> innerComparer = null)
        {
            this.valueFunction = valueFunction ?? throw new ArgumentNullException(nameof(valueFunction));
            this.innerComparer = innerComparer ?? Comparer<TInner>.Default;
        }

        public int Compare(T x, T y)
        {
            return innerComparer.Compare(valueFunction(x), valueFunction(y));
        }
    }
}
