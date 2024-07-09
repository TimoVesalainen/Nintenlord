using Nintenlord.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class IntegerDistribution : IDiscreteDistribution<int>
    {
        private readonly IDiscreteDistribution<int> distributionDistribution;
        private readonly IDistribution<int>[] distributions;
        private readonly int[] weights;

        private IntegerDistribution(IDistribution<int>[] distributions, int[] weights)
        {
            if (distributions.Length != weights.Length)
            {
                throw new ArgumentException("distributions and weights length need to be same", nameof(weights));
            }
            this.distributionDistribution = DiscreteUniformDistribution.Create(0, weights.Length - 1);
            this.distributions = distributions ?? throw new ArgumentNullException(nameof(distributions));
            this.weights = weights ?? throw new ArgumentNullException(nameof(weights));
        }

        public static IDiscreteDistribution<int> Create(IEnumerable<int> weights)
        {
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }

            return CreateInner(weights.ToArray());
        }

        public static IDiscreteDistribution<int> Create(params int[] weights)
        {
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }

            return CreateInner(weights);
        }

        private static IDiscreteDistribution<int> CreateInner(int[] weights)
        {
            if (weights.Length == 0)
            {
                return EmptyDistribution<int>.Instance;
            }
            else if (weights.Length == 1)
            {
                if (weights[0] > 0)
                {
                    return SingletonDistribution<int>.Create(0);
                }
                else
                {
                    return EmptyDistribution<int>.Instance;
                }
            }
            else if (weights.Length == 2)
            {
                return BernuelliDistribution.Create(weights[0], weights[1]);
            }

            int gcd = weights.GreatestCommonDivisor();
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] /= gcd;
            }

            var sum = weights.Sum();
            var distributions = new IDistribution<int>[weights.Length];

            var lows = new Dictionary<int, int>();
            var highs = new Dictionary<int, int>();

            for (int i = 0; i < weights.Length; i += 1)
            {
                int weight = weights[i] * weights.Length;
                if (weight == sum)
                    distributions[i] = SingletonDistribution<int>.Create(i);
                else if (weight < sum)
                    lows.Add(i, weight);
                else
                    highs.Add(i, weight);
            }

            while (lows.Any())
            {
                var low = lows.First();
                lows.Remove(low.Key);
                var high = highs.First();
                highs.Remove(high.Key);

                int lowNeeds = sum - low.Value;
                distributions[low.Key] = BernuelliDistribution.Create(low.Value, lowNeeds)
                    .Select(x => x == 0 ? low.Key : high.Key);

                int newHigh = high.Value - lowNeeds;
                if (newHigh == sum)
                    distributions[high.Key] = SingletonDistribution<int>.Create(high.Key);
                else if (newHigh < sum)
                    lows[high.Key] = newHigh;
                else
                    highs[high.Key] = newHigh;
            }

            return new IntegerDistribution(distributions, weights);
        }

        public int Sample()
        {
            return distributions[distributionDistribution.Sample()].Sample();
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(0, distributions.Length).Where(i => weights[i] > 0);
        }

        public int Weight(int t)
        {
            if (t >= 0 && t < weights.Length)
            {
                return weights[t];
            }
            return 0;
        }

        public override string ToString()
        {
            return $"{string.Join(", ", weights.Select((weight, value) => $"({value}, weight {weight})"))}";
        }
    }
}
