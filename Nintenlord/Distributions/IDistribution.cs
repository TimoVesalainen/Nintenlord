using Nintenlord.Collections;
using Nintenlord.Collections.Comparers;
using Nintenlord.Collections.EqualityComparer;
using Nintenlord.Distributions.Combinatorics;
using Nintenlord.Distributions.Discrete;
using Nintenlord.Numerics;
using Nintenlord.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions
{
    public interface IDistribution<out T>
    {
        T Sample();
    }

    public static class DistributionHelpers
    {
        public static IEnumerable<T> Samples<T>(this IDistribution<T> distribution)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (distribution is EmptyDistribution<T>)
            {
                throw new ArgumentException("Can't sample empty distribution", nameof(distribution));
            }

            IEnumerable<T> Inner()
            {
                while (true)
                    yield return distribution.Sample();
            }

            return Inner();
        }

        public static IEnumerable<double> ExpectedValues<T>(this IDistribution<int> distribution)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            return distribution.Samples().Select((x, i) => (x, i)).Scan(0d, (avg, t) => (avg * t.i + t.x) / (t.i + 1)).Skip(1);
        }

        public static IDistribution<TOut> Select<TIn, TOut>(this IDistribution<TIn> distribution, Func<TIn, TOut> selector)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (distribution is EmptyDistribution<TIn>)
            {
                return EmptyDistribution<TOut>.Instance;
            }
            else if (distribution is SingletonDistribution<TIn> singleton)
            {
                return SingletonDistribution<TOut>.Create(selector(singleton.Value));
            }
            else if (distribution is IDiscreteDistribution<TIn> discrete)
            {
                return discrete.Support()
                    .Select(item => (selector(item), discrete.Weight(item)))
                    .ToWeighedDistribution();
            }

            return new SelectDistribution<TIn, TOut>(distribution, selector);
        }

        public static IDistribution<T> Where<T>(this IDistribution<T> distribution, Predicate<T> predicate)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (distribution is EmptyDistribution<T>)
            {
                return EmptyDistribution<T>.Instance;
            }
            else if (distribution is SingletonDistribution<T> singleton)
            {
                return predicate(singleton.Value) ? singleton : EmptyDistribution<T>.Instance;
            }
            else if (distribution is IDiscreteDistribution<T> discrete)
            {
                return discrete.Support()
                    .Where(x => predicate(x))
                    .Select(item => (item, discrete.Weight(item)))
                    .ToWeighedDistribution();
            }

            return new WhereDistribution<T>(distribution, predicate);
        }

        public static IDistribution<TOut> SelectMany<TIn, TOut>(this IDistribution<TIn> distribution, Func<TIn, IDistribution<TOut>> selector)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            return SelectMany(distribution, selector, (a,b) => b);
        }

        public static IDistribution<TOut> SelectMany<TIn, TOut>(this IDistribution<TIn> distribution, Func<TIn, IDiscreteDistribution<TOut>> selector)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return SelectMany(distribution, selector, (a, b) => b);
        }

        public static IDistribution<TOut> SelectMany<TIn, TMiddle, TOut>(this IDistribution<TIn> distribution, Func<TIn, IDistribution<TMiddle>> selector, Func<TIn, TMiddle, TOut> projection)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (projection is null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            if (distribution is EmptyDistribution<TIn>)
            {
                return EmptyDistribution<TOut>.Instance;
            }
            else if (distribution is SingletonDistribution<TIn> singleton)
            {
                return selector(singleton.Value).Select(item => projection(singleton.Value, item));
            }
            else if (distribution is IDiscreteDistribution<TIn> discrete &&
                selector is Func<TIn, IDiscreteDistribution<TMiddle>> discreteSelector)
            {
                return SelectManyDiscrete(discrete, discreteSelector, projection);
            }

            return new SelectManyDistribution<TIn, TMiddle, TOut>(distribution, selector, projection);
        }

        public static IDistribution<TOut> SelectMany<TIn, TMiddle, TOut>(this IDistribution<TIn> distribution, Func<TIn, IDiscreteDistribution<TMiddle>> selector, Func<TIn, TMiddle, TOut> projection)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (projection is null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            if (distribution is EmptyDistribution<TIn>)
            {
                return EmptyDistribution<TOut>.Instance;
            }
            else if (distribution is SingletonDistribution<TIn> singleton)
            {
                return selector(singleton.Value).Select(item => projection(singleton.Value, item));
            }
            else if (distribution is IDiscreteDistribution<TIn> discrete)
            {
                return SelectManyDiscrete(discrete, selector, projection);
            }

            return new SelectManyDistribution<TIn, TMiddle, TOut>(distribution, selector, projection);
        }

        private static IDistribution<TOut> SelectManyDiscrete<TIn, TMiddle, TOut>(IDiscreteDistribution<TIn> discrete, Func<TIn, IDiscreteDistribution<TMiddle>> selector, Func<TIn, TMiddle, TOut> projection)
        {
            int product = discrete.Support()
                .Select(a => selector(a).TotalWeight())
                .Product();

            var weights = from a in discrete.Support()
                          let pb = selector(a)
                          from b in pb.Support()
                          group discrete.Weight(a) * pb.Weight(b) *
                          product / pb.TotalWeight()
                          by projection(a, b);

            var groups = weights.Select(group => (key: group.Key, sum: group.Sum()));

            var gcd = groups.Select(x => x.sum).GreatestCommonDivisor();

            return groups.Select(group => (group.key, group.sum / gcd)).ToWeighedDistribution();
        }

        public static IDistribution<(A, B)> Joint<A, B>(
            this IDistribution<A> prior,
            Func<A, IDistribution<B>> likelihood) =>
            SelectMany(prior, likelihood, (a, b) => (a, b));

        public static IDistribution<T> Where<T>(
          this IDistribution<T> d,
          Func<T, IDistribution<bool>> predicate)
        {
            return from sample in d
                   from accept in predicate(sample)
                   where accept
                   select sample;
        }

        public static IDistribution<T> Max<T>(
          this IDistribution<T> distribution1, IDistribution<T> distribution2, IComparer<T> comparer = null)
        {
            if (distribution1 is null)
            {
                throw new ArgumentNullException(nameof(distribution1));
            }

            if (distribution2 is null)
            {
                throw new ArgumentNullException(nameof(distribution2));
            }

            comparer ??= Comparer<T>.Default;

            return from sample1 in distribution1
                   from sample2 in distribution2
                   select comparer.Max(sample1, sample2);
        }

        public static IDistribution<T> Min<T>(
          this IDistribution<T> distribution1, IDistribution<T> distribution2, IComparer<T> comparer = null)
        {
            if (distribution1 is null)
            {
                throw new ArgumentNullException(nameof(distribution1));
            }

            if (distribution2 is null)
            {
                throw new ArgumentNullException(nameof(distribution2));
            }

            comparer ??= Comparer<T>.Default;

            return from sample1 in distribution1
                   from sample2 in distribution2
                   select comparer.Min(sample1, sample2);
        }


        public static IDistribution<T[]> ArrayDistribution<T>(
          this IDistribution<T> distribution, int amount, IEqualityComparer<T> comparer = null)
        {
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if (amount == 0)
            {
                return SingletonDistribution<T[]>.Create(Array.Empty<T>());
            }
            else if (distribution is EmptyDistribution<T>)
            {
                return EmptyDistribution<T[]>.Instance;
            }
            else if (distribution is SingletonDistribution<T> singleton)
            {
                return SingletonDistribution<T[]>.Create(Enumerable.Repeat(singleton.Value, amount).ToArray());
            }
            else if (distribution is IDiscreteDistribution<T> discrete)
            {
                return ArrayDiscreteDistribution(discrete, amount, comparer ?? EqualityComparer<T>.Default);
            }

            return new ArrayDistribution<T>(distribution, amount);
        }

        private static IDistribution<T[]> ArrayDiscreteDistribution<T>(IDiscreteDistribution<T> distribution, int amount, IEqualityComparer<T> comparer)
        {
            var arrayComparer = comparer.ToArrayComparer();

            var groups = Enumerable.Repeat(distribution.Support(), amount)
                .CartesianProduct()
                .Select(x => (key: x.ToArray(), sum: x.Select(distribution.Weight).Product()));

            var gcd = groups.Select(x => x.sum).GreatestCommonDivisor();

            return groups.Select(group => (group.key, group.sum / gcd)).ToWeighedDistribution(arrayComparer);
        }

        // TODO: Add selector IEnumerable<T> => TOut
        public static IDistribution<IEnumerable<T>> Distributions<T>(
          this IEnumerable<IDistribution<T>> distributions)
        {
            if (distributions is null)
            {
                throw new ArgumentNullException(nameof(distributions));
            }

            if (!distributions.Any())
            {
                return SingletonDistribution<IEnumerable<T>>.Create(Enumerable.Empty<T>());
            }
            else if (distributions.Any(x => x is EmptyDistribution<T>))
            {
                return EmptyDistribution<T[]>.Instance;
            }
            else if (distributions.All(x => x is SingletonDistribution<T>))
            {
                return SingletonDistribution<IEnumerable<T>>.Create(distributions.Select(x => (x as SingletonDistribution<T>).Value));
            }
            else if (distributions.All(x => x is IDiscreteDistribution<T>))
            {
                return DiscreteDistributions(distributions.Cast<IDiscreteDistribution<T>>());
            }

            return new Distributions<T>(distributions);
        }

        // TODO: Add selector IEnumerable<T> => TOut
        private static IDiscreteDistribution<IEnumerable<T>> DiscreteDistributions<T>(this IEnumerable<IDiscreteDistribution<T>> distributions)
        {
            var groups = distributions.Select(d => d.Support())
                .CartesianProduct()
                .Select(array => (key: array, sum: array.Zip(distributions, (item, dis) => dis.Weight(item)).Product()));

            var gcd = groups.Select(x => x.sum).GreatestCommonDivisor();

            return groups.Select(group => (group.key, group.sum / gcd)).ToWeighedDistribution();
        }
    }
}
