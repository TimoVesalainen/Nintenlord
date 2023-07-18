using System;

namespace Nintenlord.Distributions
{
    public sealed class SelectManyDistribution<TIn, TMiddle, TOut> : IDistribution<TOut>
    {
        private readonly IDistribution<TIn> distribution;
        private readonly Func<TIn, IDistribution<TMiddle>> selector;
        private readonly Func<TIn, TMiddle, TOut> projection;

        public SelectManyDistribution(IDistribution<TIn> distribution, Func<TIn, IDistribution<TMiddle>> selector, Func<TIn, TMiddle, TOut> projection)
        {
            this.distribution = distribution ?? throw new ArgumentNullException(nameof(distribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
            this.projection = projection ?? throw new ArgumentNullException(nameof(projection));
        }

        public TOut Sample()
        {
            var sample = distribution.Sample();
            return projection(sample, selector(sample).Sample());
        }
    }
}
