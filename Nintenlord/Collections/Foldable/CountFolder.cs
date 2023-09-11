using System.Numerics;

namespace Nintenlord.Collections.Foldable
{
    public sealed class CountFolder<T, TNumber> : IFolder<T, TNumber, TNumber>
        where TNumber : IAdditiveIdentity<TNumber, TNumber>, IIncrementOperators<TNumber>
    {
        public readonly static CountFolder<T, TNumber> Instance = new();

        private CountFolder()
        {

        }

        public TNumber Start => TNumber.AdditiveIdentity;

        public TNumber Fold(TNumber state, T input)
        {
            return state++;
        }

        public TNumber Transform(TNumber state)
        {
            return state;
        }
    }
}
