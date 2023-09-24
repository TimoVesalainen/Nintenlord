using Nintenlord.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Numerics
{
    public static class NumberExtensions
    {
        public static bool IsInRange<TNumber>(this TNumber val, TNumber min, TNumber max)
             where TNumber : IComparisonOperators<TNumber, TNumber, bool>
        {
            return val >= min && val <= max;
        }

        public static bool IsInRangeHO<TNumber>(this TNumber val, TNumber min, TNumber max)
             where TNumber : IComparisonOperators<TNumber, TNumber, bool>
        {
            return val >= min && val < max;
        }

        public static bool IsInRangeLO<TNumber>(this TNumber val, TNumber min, TNumber max)
             where TNumber : IComparisonOperators<TNumber, TNumber, bool>
        {
            return val > min && val <= max;
        }

        public static TNumber Clamp<TNumber>(this TNumber i, TNumber min, TNumber max)
            where TNumber : INumber<TNumber>
        {
            return TNumber.Clamp(i, min, max);
        }

        public static TNumber Lerp<TFloat, TNumber>(this TNumber a, TNumber b, TFloat t)
             where TFloat : IMultiplyOperators<TFloat, TNumber, TNumber>
             where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>
        {
            return a + t * (b - a);
        }

        public static bool Intersects<TIndex, TLength>(TIndex index1, TLength length1, TIndex index2, TLength length2)
            where TIndex : IAdditionOperators<TIndex, TLength, TIndex>,
            IComparisonOperators<TIndex, TIndex, bool>
            where TLength : IEqualityOperators<TLength, TLength, bool>,
            IAdditiveIdentity<TLength, TLength>
        {
            if (length1 == TLength.AdditiveIdentity || length2 == TLength.AdditiveIdentity)
            {
                return false;
            }

            return index1 < index2 + length2 && index1 >= index2 ||
            index2 < index1 + length1 && index2 >= index1;
        }

        /// <summary>
        /// For convergent sequences, produces sequence that converges faster.
        /// For divergent sequences, prodduces convergent series,
        /// </summary>
        public static IEnumerable<TNumber> ShanksTransformation<TNumber>(this IEnumerable<TNumber> series)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IDivisionOperators<TNumber, TNumber, TNumber>
        {
            if (series is null)
            {
                throw new ArgumentNullException(nameof(series));
            }

            return series.GetParts3s().Select((tuple) =>
            {
                var (t0, t1, t2) = tuple;

                var above = t2 * t0 - t1 * t1;
                var below = t2 - t1 - t1 + t0;
                return above / below;
            });
        }

        public static IEnumerable<TNumber> ShanksTransformation<TNumber>(this IEnumerable<TNumber> series, int n)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            ISubtractionOperators<TNumber, TNumber, TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IDivisionOperators<TNumber, TNumber, TNumber>
        {
            if (series is null)
            {
                throw new ArgumentNullException(nameof(series));
            }
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value was negative");
            }

            for (var i = 0; i < n; i++)
            {
                series = ShanksTransformation(series);
            }
            return series;
        }
    }
}
