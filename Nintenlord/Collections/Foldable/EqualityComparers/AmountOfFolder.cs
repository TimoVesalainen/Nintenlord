using System;
using System.Numerics;

namespace Nintenlord.Collections.Foldable.EqualityComparers
{
    public sealed class AmountOfFolder<T, TNumber> : IFolder<T, TNumber, TNumber>
        where TNumber : IAdditiveIdentity<TNumber, TNumber>, IIncrementOperators<TNumber>
    {
        readonly Predicate<T> predicate;

        public AmountOfFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public TNumber Start => TNumber.AdditiveIdentity;

        public TNumber Fold(TNumber state, T input)
        {
            if (predicate(input))
            {
                return ++state;
            }
            else
            {
                return state;
            }
        }

        public TNumber Transform(TNumber state)
        {
            return state;
        }
    }
}
