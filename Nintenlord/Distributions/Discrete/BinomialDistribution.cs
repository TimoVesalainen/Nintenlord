using Nintenlord.Distributions.Discrete;
using Nintenlord.Utility.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Distributions.Discrete
{
    public sealed class BinomialDistribution : IDiscreteDistribution<int>
    {
        public int SuccessWeight { get; }
        public int FailureWeight { get; }

        public int Count { get; }

        private readonly IDiscreteDistribution<int> dist;

        public BinomialDistribution(int successWeight, int failureWeight, int count)
        {
            SuccessWeight = successWeight;
            FailureWeight = failureWeight;
            Count = count;
            dist = BernuelliDistribution.Create(failureWeight, successWeight);
        }

        public int Sample()
        {
            return Enumerable.Range(0, Count).Select(_ => dist.Sample()).Sum();
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(0, Count + 1);
        }

        public int Weight(int t)
        {
            return IntegerExtensions.BinomialCoefficient(Count, t)
                * (int)Math.Pow(SuccessWeight, t)
                * (int)Math.Pow(FailureWeight, Count - t);
        }
    }
}
