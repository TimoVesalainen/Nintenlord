using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Nintenlord.Collections;
using Nintenlord.Distributions.Discrete;

namespace Nintenlord.Distributions
{
    public sealed class SingletonDistribution<T> : IDiscreteDistribution<T>
    {
        public T Value { get; }

        private readonly IEqualityComparer<T> comparer;

        private static readonly ConcurrentDictionary<T, SingletonDistribution<T>> cache = new();

        public static SingletonDistribution<T> Create(T value)
        {
            return cache.GetOrAdd(value, x => new SingletonDistribution<T>(x, EqualityComparer<T>.Default));
        }

        public SingletonDistribution(T value, IEqualityComparer<T> comparer)
        {
            Value = value;
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public T Sample()
        {
            return Value;
        }

        public IEnumerable<T> Support()
        {
            return EnumerableExtensions.Return(Value);
        }

        public int Weight(T t)
        {
            return comparer.Equals(Value, t) ? 1 : 0;
        }
    }
}
