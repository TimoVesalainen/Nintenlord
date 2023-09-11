using Nintenlord.Distributions;
using Nintenlord.Distributions.Discrete;
using NUnit.Framework;
using System.Linq;

namespace Nintenlord.Tests.Distributions
{
    public class DistributionTests
    {
        [Test]
        public void SelectManyOnDiscreteDistribution()
        {
            var distribution = new[] { 1, 1, 2 }.ToDistribution();
            var result = distribution.SelectMany(x => SingletonDistribution<int>.Create(x));

            Assert.IsInstanceOf<IDiscreteDistribution<int>>(result);
        }
    }
}
