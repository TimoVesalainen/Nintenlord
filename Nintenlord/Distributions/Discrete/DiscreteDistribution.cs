using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Distributions;
using Nintenlord.Numerics;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class DiscreteDistribution<T> : IDiscreteDistribution<T>
    {
        private readonly IDiscreteDistribution<int> underlying;
        private readonly T[] items;
        private readonly Dictionary<T, int> weights;

        public static IDiscreteDistribution<T> Create(
            IDiscreteDistribution<int> underlying,
            IEnumerable<T> items,
            IEqualityComparer<T> equalityComparer = null)
        {
            if (underlying is null)
            {
                throw new ArgumentNullException(nameof(underlying));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var itemsArray = items.ToArray();
            equalityComparer ??= EqualityComparer<T>.Default;

            var weights = underlying.Support()
                .GroupBy(i => itemsArray[i], underlying.Weight, equalityComparer)
                .Select(group => (group.Key, sum: group.Sum()))
                .Where(group => group.sum > 0);

            var gcd = weights.Select(weight => weight.sum).GreatestCommonDivisor();

            var weightDictionary = weights.ToDictionary(group => group.Key, group => group.sum / gcd, equalityComparer);

            if (weightDictionary.Count == 0)
            {
                return EmptyDistribution<T>.Instance;
            }
            else if (weightDictionary.Count == 1)
            {
                return new SingletonDistribution<T>(weightDictionary.Keys.Single(), equalityComparer);
            }

            return new DiscreteDistribution<T>(underlying, itemsArray, weightDictionary);
        }

        public DiscreteDistribution(IDiscreteDistribution<int> underlying, T[] items, Dictionary<T, int> weights)
        {
            this.underlying = underlying ?? throw new ArgumentNullException(nameof(underlying));
            this.items = items ?? throw new ArgumentNullException(nameof(items));
            this.weights = weights ?? throw new ArgumentNullException(nameof(weights));
        }

        public T Sample()
        {
            return items[underlying.Sample()];
        }

        public IEnumerable<T> Support()
        {
            return weights.Keys;
        }

        public int Weight(T t)
        {
            if (weights.TryGetValue(t, out var weight))
            {
                return weight;
            }
            else
            {
                return 0;
            }
        }
    }
}
