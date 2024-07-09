using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Distributions;

namespace Nintenlord.Distributions.Combinatorics
{
    sealed class Distributions<T> : IDistribution<T[]>
    {
        readonly IDistribution<T>[] distributions;

        public Distributions(IEnumerable<IDistribution<T>> distributions)
        {
            this.distributions = distributions?.ToArray() ?? throw new ArgumentNullException(nameof(distributions));
        }

        public T[] Sample()
        {
            return distributions.Select(distribution => distribution.Sample()).ToArray();
        }
    }
}
