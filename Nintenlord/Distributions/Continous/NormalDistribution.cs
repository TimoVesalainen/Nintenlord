using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Nintenlord.Distributions.Continous
{
    public sealed class NormalDistribution : IDistribution<double>
    {
        public double Mean { get; }
        public double Sigma { get; }

        public readonly static NormalDistribution Standard = Distribution(0, 1);
        public static NormalDistribution Distribution(double mean, double sigma) => new(mean, sigma);

        private NormalDistribution(double mean, double sigma)
        {
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "Sigma should be positive");
            }

            Mean = mean;
            Sigma = sigma;
        }

        private static double StandardSample() =>
          Sqrt(-2.0 * Log(StandardUniformDoubleDistribution.Distribution.Sample())) *
            Cos(2.0 * PI * StandardUniformDoubleDistribution.Distribution.Sample());

        public double Sample() => Mean + Sigma * StandardSample();
    }
}
