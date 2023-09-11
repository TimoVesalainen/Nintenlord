using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class SelectEqualityComparer<T, TInner> : IEqualityComparer<T>
    {
        private readonly Func<T, TInner> valueFunction;
        private readonly IEqualityComparer<TInner> innerComparer;

        public SelectEqualityComparer(Func<T, TInner> valueFunction, IEqualityComparer<TInner> innerComparer)
        {
            this.valueFunction = valueFunction;
            this.innerComparer = innerComparer;
        }

        public bool Equals(T x, T y)
        {
            return innerComparer.Equals(valueFunction(x), valueFunction(y));
        }

        public int GetHashCode(T obj)
        {
            return innerComparer.GetHashCode(valueFunction(obj));
        }
    }
}
