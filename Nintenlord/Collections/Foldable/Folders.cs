using Nintenlord.Utility;

namespace Nintenlord.Collections.Foldable
{
    public static class Folders
    {
        public static readonly IFolder<int, int, int> SumI = new FunctionFolder<int, int, int>(0, (x, y) => x + y, x => x);
        public static readonly IFolder<long, long, long> SumL = new FunctionFolder<long, long, long>(0, (x, y) => x + y, x => x);
        public static readonly IFolder<float, float, float> SumF = new FunctionFolder<float, float, float>(0, (x, y) => x + y, x => x);
        public static readonly IFolder<double, double, double> SumD = new FunctionFolder<double, double, double>(0, (x, y) => x + y, x => x);

        public static CountIntFolder<T> CountI<T>() => CountIntFolder<T>.Value;
        public static CountLongFolder<T> CountL<T>() => CountLongFolder<T>.Value;

        public static readonly IFolder<int, (int, int), double> AverageI = SumI.Combine(CountI<int>(), (sum, count) => sum / (double)count);
        public static readonly IFolder<long, (long, int), double> AverageL = SumL.Combine(CountI<long>(), (sum, count) => sum / (double)count);
        public static readonly IFolder<float, (float, int), double> AverageF = SumF.Combine(CountI<float>(), (sum, count) => sum / (double)count);
        public static readonly IFolder<double, (double, int), double> AverageD = SumD.Combine(CountI<double>(), (sum, count) => sum / (double)count);

        public static MinFolder<T> Min<T>() => MinFolder<T>.Default;
        public static MaxFolder<T> Max<T>() => MaxFolder<T>.Default;

        public static IFolder<T, (Maybe<T>, Maybe<T>), Maybe<(T min, T max)>> MinMax<T>()
        {
            return Min<T>().Combine(Max<T>(), (x, y) => MaybeHelpers.Zip(x, y, (a, b) => (a, b)));
        }

    }
}
