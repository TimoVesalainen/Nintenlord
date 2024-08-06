using Nintenlord.Distributions.Continous;
using Nintenlord.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class BernuelliDistribution : IDiscreteDistribution<int>
    {
        public int ZeroWeight { get; }
        public int OneWeight { get; }

        private double Probability => ZeroWeight / (ZeroWeight + OneWeight);

        public static IDiscreteDistribution<int> Create(int zeroWeight, int oneWeight)
        {
            if (zeroWeight < 0)
            {
                throw new ArgumentException("Weight should be non-negative", nameof(zeroWeight));
            }
            if (oneWeight < 0)
            {
                throw new ArgumentException("Weight should be non-negative", nameof(oneWeight));
            }

            if (zeroWeight == 0 || oneWeight == 0)
            {
                if (zeroWeight > 0)
                {
                    return SingletonDistribution<int>.Create(0);
                }
                else if (oneWeight > 0)
                {
                    return SingletonDistribution<int>.Create(1);
                }
                else
                {
                    return EmptyDistribution<int>.Instance;
                }
            }

            return new BernuelliDistribution(zeroWeight, oneWeight);
        }

        private BernuelliDistribution(int zeroWeight, int oneWeight)
        {
            if (zeroWeight <= 0)
            {
                throw new ArgumentException("Weight should be positive", nameof(zeroWeight));
            }
            if (oneWeight <= 0)
            {
                throw new ArgumentException("Weight should be positive", nameof(oneWeight));
            }
            var divisor = zeroWeight.GreatestCommonDivisor(oneWeight);
            ZeroWeight = zeroWeight / divisor;
            OneWeight = oneWeight / divisor;
        }

        public int Sample()
        {
            return StandardUniformDoubleDistribution.Distribution.Sample() < Probability ? 0 : 1;
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(0, 2);
        }

        public int SupportCount => 2;

        public int Weight(int t)
        {
            return t switch
            {
                0 => ZeroWeight,
                1 => OneWeight,
                _ => 0
            };
        }
    }
}
