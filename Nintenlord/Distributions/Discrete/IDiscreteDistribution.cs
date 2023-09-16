using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Collections;

namespace Nintenlord.Distributions.Discrete
{
    public interface IDiscreteDistribution<T> : IDistribution<T>
    {
        IEnumerable<T> Support();

        int Weight(T item);
    }

    public static class DiscreteDistributionHelpers
    {
        public static int TotalWeight<T>(this IDiscreteDistribution<T> distribution)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            return distribution.Support().Select(distribution.Weight).Sum();
        }

        public static double ExpectedValue(this IDiscreteDistribution<int> d) =>
            d.Support().Select(s => (double)s * d.Weight(s)).Sum() / d.TotalWeight();

        public static double ExpectedValue(this IDiscreteDistribution<double> d) =>
            d.Support().Select(s => s * d.Weight(s)).Sum() / d.TotalWeight();

        public static IDiscreteDistribution<T> ToDistribution<T>(this IEnumerable<T> enumerable, IDiscreteDistribution<int> indexDistribution = null, IEqualityComparer<T> equalityComparer = null)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            var count = enumerable.Count();

            if (indexDistribution?.Support().Any(index => index < 0 || index >= count) ?? false)
            {
                throw new ArgumentException("Invalid index distribution", nameof(indexDistribution));
            }

            var dist = indexDistribution ?? DiscreteUniformDistribution.Create(0, count - 1);

            return DiscreteDistribution<T>.Create(dist, enumerable, equalityComparer);
        }

        public static IDiscreteDistribution<T> ToWeighedDistribution<T>(this IEnumerable<KeyValuePair<T, int>> weights, IEqualityComparer<T> equalityComparer = null)
        {
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }

            var indexDistribution = IntegerDistribution.Create(weights.Select(x => x.Value));

            return DiscreteDistribution<T>.Create(
                indexDistribution, 
                weights.Select(x => x.Key), 
                equalityComparer);
        }

        public static IDiscreteDistribution<T> ToWeighedDistribution<T>(this IEnumerable<(T key, int weight)> weights, IEqualityComparer<T> equalityComparer = null)
        {
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }

            var indexDistribution = IntegerDistribution.Create(weights.Select(x => x.weight));

            return DiscreteDistribution<T>.Create(
                indexDistribution, 
                weights.Select(x => x.key), 
                equalityComparer);
        }
    }
}
