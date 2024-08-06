using Nintenlord.Distributions.Continous;
using System;

namespace Nintenlord.Distributions
{
    public sealed class PoissonDistribution : IDistribution<int>
    {
        public double Lambda { get; }

        public int Sample()
        {
            var u = StandardUniformDoubleDistribution.Distribution.Sample();

            var x = 0;
            var p = Math.Exp(-Lambda);
            var s = p;

            while (u > s)
            {
                x++;
                p *= Lambda / x;
                s += p;
            }

            return x;
        }
    }
}
