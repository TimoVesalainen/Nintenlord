using Nintenlord.Collections;
using Nintenlord.Matricis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Numerics
{
    public static class NumberSequences
    {
        /// <summary>
        /// For convergent sequences, produces sequence that converges faster.
        /// For divergent sequences, prodduces convergent series.
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

            return series.GetSequential3s().Select((tuple) =>
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

        public static IEnumerable<TNumber> BinomialTransformation<TNumber>(this IEnumerable<TNumber> series)
            where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
            IAdditiveIdentity<TNumber, TNumber>,
            IMultiplyOperators<TNumber, TNumber, TNumber>,
            IMultiplicativeIdentity<TNumber, TNumber>,
            IUnaryNegationOperators<TNumber, TNumber>
        {
            if (series is null)
            {
                throw new ArgumentNullException(nameof(series));
            }

            var parityMultipliers = TNumber.MultiplicativeIdentity.UnAggregate(item => -item);

            return IntegerExtensions.BinomialCoefficientRows<TNumber>().Select(
                multipliers => multipliers.Zip(parityMultipliers).Zip(series, (t, y) => t.First * t.Second * y).Sum());
        }
    }
}
