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

        public int SupportCount => 1;

        private readonly IEqualityComparer<T> comparer;

        private static readonly ConcurrentDictionary<T, SingletonDistribution<T>> cache = new();

        public static SingletonDistribution<T> Create(T value, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null || comparer == EqualityComparer<T>.Default)
            {
                return cache.GetOrAdd(value, x => new SingletonDistribution<T>(x, comparer ?? EqualityComparer<T>.Default));
            }
            return new SingletonDistribution<T>(value, comparer);
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
