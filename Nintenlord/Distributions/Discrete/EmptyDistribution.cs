using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class EmptyDistribution<T> : IDiscreteDistribution<T>
    {
        public static readonly EmptyDistribution<T> Instance = new();

        private EmptyDistribution() { }

        public int SupportCount => 0;

        public T Sample()
        {
            throw new InvalidOperationException("Cannot sample empty distribution");
        }

        public IEnumerable<T> Support()
        {
            return Enumerable.Empty<T>();
        }

        public int Weight(T t)
        {
            return 0;
        }
    }
}
