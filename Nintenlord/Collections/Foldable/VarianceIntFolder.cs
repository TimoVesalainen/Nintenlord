using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class VarianceIntFolder : IFolder<long, (long, double, double), double>
    {
        public static readonly VarianceIntFolder Instance = new VarianceIntFolder();

        private VarianceIntFolder()
        {
        }

        public (long, double, double) Start => (0,0,0);

        public (long, double, double) Fold((long, double, double) state, long input)
        {
            var (count, mean, m2) = state;

            var newMean = (mean * count + input) / (count + 1);
            var delta = input - mean;
            var newM2 = m2 + delta * delta * count / (count + 1);

            return (count + 1, newMean, newM2);
        }

        public double Transform((long, double, double) state)
        {
            var (count, _, m2) = state;
            return m2 / count;
        }
    }
}
