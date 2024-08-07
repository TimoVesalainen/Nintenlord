﻿using System;

namespace Nintenlord.Distributions.Continous
{
    public sealed class RejectionSampling<T> : IContinousDistribution<T>
    {
        readonly Func<T, double> density;
        readonly IContinousDistribution<T> source;
        readonly double epsilon;

        public RejectionSampling(Func<T, double> density, IContinousDistribution<T> source, double epsilon)
        {
            this.density = density ?? throw new ArgumentNullException(nameof(density));
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.epsilon = epsilon;
        }

        public double Density(T item)
        {
            return density(item);
        }

        public T Sample()
        {
            while (true)
            {
                var sourceItem = source.Sample();
                var originalDensity = source.Density(sourceItem) * epsilon;
                if (GetChance(density(sourceItem) / originalDensity).Sample())
                {
                    return sourceItem;
                }
            }
        }

        static CoinFlip<bool> GetChance(double headsPropability)
        {
            return new CoinFlip<bool>(true, false, headsPropability);
        }
    }
}
