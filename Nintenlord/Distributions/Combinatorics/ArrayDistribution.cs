using System;

namespace Nintenlord.Distributions.Combinatorics
{
    sealed class ArrayDistribution<T> : IDistribution<T[]>
    {
        readonly IDistribution<T> distribution;
        readonly int length;

        public ArrayDistribution(IDistribution<T> distribution, int length)
        {
            this.distribution = distribution ?? throw new ArgumentNullException(nameof(distribution));
            this.length = length;

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
        }

        public T[] Sample()
        {
            var list = new T[length];

            for (int i = 0; i < list.Length; i++)
            {
                list[i] = distribution.Sample();
            }

            return list;
        }
    }
}
