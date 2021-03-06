using System;

namespace Nintenlord.RandomDistributions
{
    public sealed class UniformDistribution : IDistribution<double>
    {
        private readonly double start;
        private readonly double length;
        private readonly Random random;

        private UniformDistribution(Random random, double start, double length)
        {
            this.start = start;
            this.length = length;
            this.random = random;
        }

        public double NextValue()
        {
            return start + random.NextDouble() * length;
        }

        public static UniformDistribution FromStartEnd(Random random, double start, double end)
        {
            return new UniformDistribution(random, start, end - start);
        }

        public static UniformDistribution FromStartLength(Random random, double start, double length)
        {
            return new UniformDistribution(random, start, length);
        }
    }
}
