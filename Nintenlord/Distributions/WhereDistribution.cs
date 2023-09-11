using System;

namespace Nintenlord.Distributions
{
    public sealed class WhereDistribution<T> : IDistribution<T>
    {
        private readonly IDistribution<T> distribution;
        private readonly Predicate<T> predicate;

        public WhereDistribution(IDistribution<T> distribution, Predicate<T> predicate)
        {
            this.distribution = distribution ?? throw new ArgumentNullException(nameof(distribution));
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public T Sample()
        {
            while (true) 
            {
                var sample = distribution.Sample();
                if (predicate(sample))
                {
                    return sample;
                }
            }
        }
    }
}
