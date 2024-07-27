using Nintenlord.Collections;
using Nintenlord.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class EnumerableDiscreteDistributions<T> : IDiscreteDistribution<IEnumerable<T>>
    {
        readonly IDiscreteDistribution<T> distribution;
        readonly int count;

        public int SupportCount { get; }

        public EnumerableDiscreteDistributions(IDiscreteDistribution<T> distribution, int count)
        {
            this.distribution = distribution ?? throw new ArgumentNullException(nameof(distribution));
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            this.count = count;
            SupportCount = (int)Math.Pow(distribution.SupportCount, count);
        }

        public IEnumerable<T> Sample()
        {
            for (int i = 0; i < count; i++)
            {
                yield return distribution.Sample();
            }
        }

        public IEnumerable<IEnumerable<T>> Support()
        {
            return Enumerable.Repeat(distribution.Support(), count).CartesianProduct();
        }

        public int Weight(IEnumerable<T> item)
        {
            return item.Select(distribution.Weight).Product();
        }
    }
}
