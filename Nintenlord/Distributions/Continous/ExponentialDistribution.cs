using System;

namespace Nintenlord.Distributions.Continous
{
    public sealed class ExponentialDistribution : IDistribution<double>
    {
        public double Lambda { get; }

        public ExponentialDistribution(double lambda)
        {
            if (lambda <= 0)
            {
                throw new ArgumentException(nameof(lambda), "Must be positive");
            }
            Lambda = lambda;
        }

        public double Sample()
        {
            var u = StandardUniformDoubleDistribution.Distribution.Sample();

            return -Math.Log(u) / Lambda;
        }
    }
}
