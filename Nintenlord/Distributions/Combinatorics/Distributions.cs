using Nintenlord.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Combinatorics
{
    sealed class Distributions<T> : IDistribution<IEnumerable<T>>
    {
        readonly IDistribution<T>[] distributions;

        public Distributions(IEnumerable<IDistribution<T>> distributions)
        {
            this.distributions = distributions?.ToArray() ?? throw new ArgumentNullException(nameof(distributions));
        }

        public IEnumerable<T> Sample()
        {
            return distributions.Select(distribution => distribution.Sample());
        }
    }
}
