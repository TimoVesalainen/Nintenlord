using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class DiscreteUniformDistribution : IDiscreteDistribution<int>
    {
        static readonly Random randomInstance = new();

        readonly Random random;
        public int Min { get; }
        public int Max { get; }

        public int SupportCount => Max - Min + 1;

        readonly static ConcurrentDictionary<(int min, int max), DiscreteUniformDistribution> cache = new();

        public static IDiscreteDistribution<int> Create(int min, int max)
        {
            if (min > max)
            {
                return EmptyDistribution<int>.Instance;
            }
            if (min == max)
            {
                return SingletonDistribution<int>.Create(min);
            }

            return cache.GetOrAdd((min, max), (pair) => new DiscreteUniformDistribution(randomInstance, pair.min, pair.max));
        }

        private DiscreteUniformDistribution(Random random, int min, int max)
        {
            this.random = random ?? throw new ArgumentNullException(nameof(random));

            if (min > max)
            {
                throw new ArgumentException("Max < Min", nameof(max));
            }

            Min = min;
            Max = max;
        }

        public int Sample()
        {
            return random.Next(Min, Max + 1);
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(Min, Max - Min + 1);
        }

        public int Weight(int t)
        {
            return Min <= t && t <= Max ? 1 : 0;
        }
    }
}
