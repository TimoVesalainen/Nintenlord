using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Numerics
{
    public static class FloatingPointExtensions
    {
        public static IEnumerable<TInt> GetIntegersBetween<TInt, TFloat>(TFloat min, TFloat max)
            where TFloat : IFloatingPoint<TFloat> 
            where TInt : INumberBase<TInt>, IComparisonOperators<TInt, TInt, bool>
        {
            if (min > max)
            {
                throw new ArgumentException("min is larger than max");
            }

            TInt minI = TInt.CreateChecked(TFloat.Ceiling(min));
            TInt maxI = TInt.CreateChecked(TFloat.Floor(max));

            for (TInt i = minI; i <= maxI; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<TFloat> GetFloats<TFloat>(int n)
             where TFloat : IFloatingPoint<TFloat>
        {
            var paramAsFloat = TFloat.CreateChecked(n);
            for (int i = 0; i <= n; i++)
            {
                yield return TFloat.CreateChecked(i) / paramAsFloat;
            }
        }

        public static TNumber SigmoidRP<TNumber>(this TNumber value, TNumber min, TNumber max)
            where TNumber: IExponentialFunctions<TNumber>, IComparisonOperators<TNumber, TNumber, bool>
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Min > max");
            }

            var exponent = TNumber.Exp(value * TNumber.CreateChecked(10) - TNumber.CreateChecked(5));

            var sigmoid = exponent / (TNumber.One + exponent);

            return min + sigmoid * (max - min);
        }
    }
}
