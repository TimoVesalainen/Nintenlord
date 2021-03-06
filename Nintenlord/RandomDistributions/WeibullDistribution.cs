using System;

namespace Nintenlord.RandomDistributions
{
    public sealed class WeibullDistribution : IDistribution<double>
    {
        private readonly UniformDistribution dist;
        private readonly double alpha;
        private readonly double lambda;

        public WeibullDistribution(Random random, double alpha, double lambda)
        {
            dist = UniformDistribution.FromStartLength(random, 0, 1);
            this.lambda = lambda;
            this.alpha = alpha;
        }

        #region IDistribution<double> Members

        public double NextValue()
        {
            return Math.Pow(-Math.Log(1 - dist.NextValue()) / lambda, 1 / alpha);
        }

        #endregion
    }
}
