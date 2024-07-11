using Nintenlord.Collections;
using Nintenlord.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class ArrayDiscreteDistributions<T> : IDiscreteDistribution<T[]>
    {
        readonly IDiscreteDistribution<T> distribution;
        readonly int count;

        public ArrayDiscreteDistributions(IDiscreteDistribution<T> distribution, int count)
        {
            this.distribution = distribution ?? throw new ArgumentNullException(nameof(distribution));
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            this.count = count;
        }

        public T[] Sample()
        {
            var sample = new T[count];

            for (int i = 0; i < sample.Length; i++)
            {
                sample[i] = distribution.Sample();
            }

            return sample;
        }

        public IEnumerable<T[]> Support()
        {
            return Enumerable.Repeat(distribution.Support(), count).CartesianProduct().Select(item => item.ToArray());
        }

        public int Weight(T[] item)
        {
            return item.Select(distribution.Weight).Product();
        }
    }
}
