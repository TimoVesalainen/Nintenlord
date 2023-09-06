using System.Numerics;

namespace Nintenlord.Collections.Foldable
{
    public sealed class SumFolder<TNumber> : IFolder<TNumber, TNumber, TNumber>
        where TNumber : IAdditiveIdentity<TNumber, TNumber>, IAdditionOperators<TNumber, TNumber, TNumber>
    {
        public static readonly SumFolder<TNumber> Instance = new();

        private SumFolder()
        {

        }

        public TNumber Start => TNumber.AdditiveIdentity;

        public TNumber Fold(TNumber state, TNumber input)
        {
            return state + input;
        }

        public TNumber Transform(TNumber state)
        {
            return state;
        }
    }
}
