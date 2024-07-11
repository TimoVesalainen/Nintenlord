using Nintenlord.Collections;
using Nintenlord.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class DiscreteDistributions<T> : IDiscreteDistribution<IEnumerable<T>>
    {
        readonly IDiscreteDistribution<T>[] distributions;

        public DiscreteDistributions(IEnumerable<IDiscreteDistribution<T>> distributions)
        {
            this.distributions = distributions?.ToArray() ?? throw new ArgumentNullException(nameof(distributions));
        }

        public IEnumerable<T> Sample()
        {
            return distributions.Select(distribution => distribution.Sample());
        }

        public IEnumerable<IEnumerable<T>> Support()
        {
            return distributions.Select(d => d.Support()).CartesianProduct();
        }

        public int Weight(IEnumerable<T> item)
        {
            return item.Zip(distributions, (item, distribution) => distribution.Weight(item)).Product();
        }
    }
}
