using Nintenlord.Collections;
using Nintenlord.Distributions;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Numerics
{
    /// <summary>
    /// Extensions and helper methods to integers
    /// </summary>
    public static class IntegerExtensions
    {
        public static int Lerp(int min, int max, double val, MidpointRounding roundingMode)
        {
            return min + (int)Math.Round((max - min) * val, roundingMode);
        }

        public static IEnumerable<TNumber> GetIntegers<TNumber>(TNumber start, TNumber end, TNumber step = default)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IIncrementOperators<TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>
        {
            var stepToUse = step == TNumber.AdditiveIdentity ? TNumber.MultiplicativeIdentity : step;
            for (TNumber i = start; i <= end; i += stepToUse)
            {
                yield return i;
            }
        }

        public static IEnumerable<TNumber> GetIntegersNear<TNumber>(this TNumber x, TNumber range)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IIncrementOperators<TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>
        {
            for (TNumber i = x - range; i <= x + range; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<TNumber> Range<TNumber>(this TNumber start, TNumber length)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IIncrementOperators<TNumber>
        {
            for (TNumber i = TNumber.AdditiveIdentity; i <= length; i++)
            {
                yield return start + i;
            }
        }

        public static IEnumerable<IEnumerable<TNumber>> Compositions<TNumber>(this TNumber number)
            where TNumber : ISubtractionOperators<TNumber, TNumber, TNumber>,
            IAdditionOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IEqualityOperators<TNumber, TNumber, bool>,
            IIncrementOperators<TNumber>,
            IAdditiveIdentity<TNumber, TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>
        {
            if (number == TNumber.AdditiveIdentity)
            {
                yield return Enumerable.Empty<TNumber>();
            }
            if (number == TNumber.MultiplicativeIdentity)
            {
                yield return TNumber.MultiplicativeIdentity.Return();
            }
            else
            {
                for (TNumber i = TNumber.AdditiveIdentity; i < number; i++)
                {
                    foreach (var item in i.Compositions())
                    {
                        yield return item.Prepend(number - i);
                    }
                }
            }
        }

        public static TNumber BinomialCoefficient<TNumber>(TNumber n, TNumber k)
            where TNumber :
            IEqualityOperators<TNumber, TNumber, bool>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IAdditionOperators<TNumber, TNumber, TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>
        {
            if (n < TNumber.AdditiveIdentity)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n needs to be non-negative");
            }

            if (k < TNumber.AdditiveIdentity || k > n)
            {
                return TNumber.AdditiveIdentity;
            }

            static TNumber Inner(TNumber n, TNumber k)
            {
                if (k == TNumber.AdditiveIdentity || k == n)
                {
                    return TNumber.MultiplicativeIdentity;
                }
                return Inner(n - TNumber.MultiplicativeIdentity, k - TNumber.MultiplicativeIdentity) + Inner(n - TNumber.MultiplicativeIdentity, k);
            }

            return Inner(n, k);
        }

        public static IEnumerable<TNumber> BinomialCoefficients<TNumber>(TNumber n)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IIncrementOperators<TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>,
            IDivisionOperators<TNumber, TNumber, TNumber>
        {
            if (n < TNumber.AdditiveIdentity)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n needs to be non-negative");
            }

            return TNumber.MultiplicativeIdentity.Range(n).SkipLast(1)
                .Select(k => (n + TNumber.MultiplicativeIdentity - k, k))
                .Scan(TNumber.MultiplicativeIdentity, (acc, tuple) => acc * tuple.Item1 / tuple.k);
        }

        public static IEnumerable<IEnumerable<TNumber>> BinomialCoefficientRows<TNumber>()
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>
        {
            return BinomialCoefficientRowEnumerable<TNumber>.Instance;
        }

        public static TNumber StirlingNumberFirstKind<TNumber>(TNumber n, TNumber k)
            where TNumber : IEqualityOperators<TNumber, TNumber, bool>,
            IMultiplicativeIdentity<TNumber, TNumber>,
            IAdditiveIdentity<TNumber, TNumber>,
            IDecrementOperators<TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IAdditionOperators<TNumber, TNumber, TNumber>
        {
            if (n == k)
            {
                return TNumber.MultiplicativeIdentity;
            }
            if (n == TNumber.AdditiveIdentity || k == TNumber.AdditiveIdentity)
            {
                return TNumber.AdditiveIdentity;
            }

            n--;
            return n * StirlingNumberFirstKind(n, k) + StirlingNumberFirstKind(n, --k);
        }

        public static TNumber StirlingNumberSecondKind<TNumber>(TNumber n, TNumber k)
            where TNumber : IEqualityOperators<TNumber, TNumber, bool>,
            IMultiplicativeIdentity<TNumber, TNumber>,
            IAdditiveIdentity<TNumber, TNumber>,
            IDecrementOperators<TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IAdditionOperators<TNumber, TNumber, TNumber>
        {
            if (n == k)
            {
                return TNumber.MultiplicativeIdentity;
            }
            if (n == TNumber.AdditiveIdentity || k == TNumber.AdditiveIdentity)
            {
                return TNumber.AdditiveIdentity;
            }

            n--;
            return k * StirlingNumberSecondKind(n, k) + StirlingNumberSecondKind(n, --k);
        }

        public static TNumber LahNumber<TNumber>(TNumber n, TNumber k)
            where TNumber : IEqualityOperators<TNumber, TNumber, bool>,
            IMultiplicativeIdentity<TNumber, TNumber>,
            IAdditiveIdentity<TNumber, TNumber>,
            IDecrementOperators<TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IAdditionOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>
        {
            if (n == k)
            {
                return TNumber.MultiplicativeIdentity;
            }
            if (k == TNumber.AdditiveIdentity || n < k)
            {
                return TNumber.AdditiveIdentity;
            }

            n--;
            return (n + k) * LahNumber(n, k) + LahNumber(n, --k);
        }
    }
}
