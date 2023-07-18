using System;
using System.Collections.Generic;

namespace Nintenlord.Distributions.Continous
{
    public sealed class CoinFlip<T> : IContinousDistribution<T>
    {
        readonly T heads;
        readonly T tails;
        readonly IEqualityComparer<T> comparer;
        readonly double headsPropability;

        public CoinFlip(T heads, T tails, double headsPropability, IEqualityComparer<T> comparer = null)
        {
            if (headsPropability < 0 || headsPropability > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(headsPropability), headsPropability, "Value not valid propability"); 
            }
            this.heads = heads;
            this.tails = tails;
            this.headsPropability = headsPropability;
            this.comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public double Density(T item)
        {
            if (comparer.Equals(item, heads))
            {
                return headsPropability;
            }
            else if (comparer.Equals(item, tails))
            {
                return 1 - headsPropability;
            }
            return 0;
        }

        public T Sample()
        {
            return StandardUniformDoubleDistribution.Distribution.Sample() < headsPropability ? heads : tails;
        }
    }
}
