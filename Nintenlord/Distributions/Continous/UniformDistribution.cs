using System;

namespace Nintenlord.Distributions.Continous
{
    public sealed class StandardUniformDoubleDistribution : IDistribution<double>
    {
        public static readonly StandardUniformDoubleDistribution Distribution = new(new Random());

        private readonly Random source;

        public StandardUniformDoubleDistribution(Random source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public double Sample()
        {
            return source.NextDouble();
        }
    }

    public sealed class StandardUniformFloatDistribution : IDistribution<float>
    {
        public static readonly StandardUniformFloatDistribution Distribution = new(new Random());

        private readonly Random source;

        public StandardUniformFloatDistribution(Random source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public float Sample()
        {
            return source.NextSingle();
        }
    }
}
