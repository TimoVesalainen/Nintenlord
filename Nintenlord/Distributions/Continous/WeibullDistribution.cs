using System;

namespace Nintenlord.Distributions.Continous
{
    public sealed class WeibullDistribution : IDistribution<double>
    {
        public double Alpha { get; }

        public double Lambda { get; }

        public WeibullDistribution(double alpha, double lambda)
        {
            Lambda = lambda;
            Alpha = alpha;
        }

        public double Sample()
        {
            var u = StandardUniformDoubleDistribution.Distribution.Sample();
            return Math.Pow(-Math.Log(1 - u) / Lambda, 1 / Alpha);
        }
    }
}
