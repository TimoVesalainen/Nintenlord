using System;

namespace Nintenlord.Distributions.Continous
{
    public sealed class GammaDistribution : IDistribution<double>
    {
        public double Shape { get; }
        public double Scale { get; }

        private double Limit => Math.E / (Math.E + Shape);

        public double Sample()
        {
            // Ahrens-Dieter
            while (true)
            {
                var u = StandardUniformDoubleDistribution.Distribution.Sample();
                var v = StandardUniformDoubleDistribution.Distribution.Sample();
                var w = StandardUniformDoubleDistribution.Distribution.Sample();

                var ksi = u <= Limit ? Math.Pow(v, 1 / Shape) : 1 - Math.Log(v);
                var nu = u <= Limit ? w * Math.Pow(ksi, Shape - 1) : w * Math.Exp(-ksi);

                if (nu <= Math.Pow(ksi, Shape - 1) * Math.Exp(-ksi))
                {
                    return Scale * ksi;
                }
            }
        }
    }
}
