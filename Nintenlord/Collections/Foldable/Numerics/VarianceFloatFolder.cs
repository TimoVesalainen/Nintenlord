namespace Nintenlord.Collections.Foldable.Numerics
{
    public sealed class VarianceFloatFolder : IFolder<double, (long, double, double), double>
    {
        public static readonly VarianceFloatFolder Instance = new();

        private VarianceFloatFolder()
        {
        }

        public (long, double, double) Start => (0, 0, 0);

        public (long, double, double) Fold((long, double, double) state, double input)
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
