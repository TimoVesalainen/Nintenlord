﻿namespace Nintenlord.Collections.Foldable.Numerics
{
    public sealed class MeanIntFolder : IFolder<long, (double mean, long count), double>
    {
        public static readonly MeanIntFolder Instance = new();

        private MeanIntFolder()
        {
        }

        public (double mean, long count) Start => (0, 0);

        public (double mean, long count) Fold((double mean, long count) state, long input)
        {
            var (mean, count) = state;

            var newMean = mean * count / (count + 1) + input / (count + 1);

            return (newMean, count + 1);
        }

        public double Transform((double mean, long count) state)
        {
            return state.mean;
        }
    }
}
