using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Numerics
{
    public static class DivisionHelpers
    {
        public static IEnumerable<TBase> BaseNRepresentation<TNumber, TBase>(this TNumber value, TBase baseSize)
            where TNumber : IDivisionOperators<TNumber, TBase, TNumber>,
            IModulusOperators<TNumber, TBase, TBase>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>
            where TBase : IAdditiveIdentity<TBase, TBase>, IEqualityOperators<TBase, TBase, bool>
        {
            if (baseSize == TBase.AdditiveIdentity)
            {
                throw new ArgumentOutOfRangeException(nameof(baseSize), "Base can't be zero");
            }

            while (value > TNumber.AdditiveIdentity)
            {
                yield return value % baseSize;
                value /= baseSize;
            }
        }

        public static TNumber ToMod<TNumber, TBase>(this TNumber number, TBase mod)
            where TNumber : IModulusOperators<TNumber, TBase, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IAdditionOperators<TNumber, TBase, TNumber>
            where TBase : ISubtractionOperators<TBase, TNumber, TNumber>
        {
            var modded = number % mod;

            if (modded < TNumber.AdditiveIdentity)
            {
                return modded + mod;
            }
            return modded;
        }

        public static void ToMod<TNumber, TBase>(ref TNumber number, TBase mod)
            where TNumber : IModulusOperators<TNumber, TBase, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IAdditionOperators<TNumber, TBase, TNumber>
            where TBase : ISubtractionOperators<TBase, TNumber, TNumber>
        {
            var modded = number % mod;

            if (modded < TNumber.AdditiveIdentity)
            {
                modded += mod;
            }
            number = modded;
        }

        public static TNumber GreatestCommonDivisor<TNumber>(this TNumber a, TNumber b)
            where TNumber : IModulusOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>
        {
            static TNumber GDC(TNumber n, TNumber m) => m == TNumber.AdditiveIdentity ? n : GDC(m, n % m);

            return a > b ? GDC(a, b) : GDC(b, a);
        }

        public static TNumber GreatestCommonDivisor<TNumber>(this IEnumerable<TNumber> numbers)
            where TNumber : IModulusOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>
        {
            if (numbers is null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            return numbers.Aggregate(TNumber.AdditiveIdentity, GreatestCommonDivisor);
        }

        public static TNumber LeastCommonDivider<TNumber>(this TNumber a, TNumber b)
            where TNumber : IModulusOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IDivisionOperators<TNumber, TNumber, TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>
        {
            return a * b / a.GreatestCommonDivisor(b);
        }

        public static TNumber LeastCommonDivider<TNumber>(this IEnumerable<TNumber> numbers)
            where TNumber : IModulusOperators<TNumber, TNumber, TNumber>,
            IComparisonOperators<TNumber, TNumber, bool>,
            IAdditiveIdentity<TNumber, TNumber>,
            IDivisionOperators<TNumber, TNumber, TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>
        {
            if (numbers is null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            return numbers.Aggregate(TNumber.MultiplicativeIdentity, LeastCommonDivider);
        }
    }
}
