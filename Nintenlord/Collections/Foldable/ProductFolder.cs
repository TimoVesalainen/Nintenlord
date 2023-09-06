using System.Numerics;

namespace Nintenlord.Collections.Foldable
{
    public sealed class ProductFolder<TNumber> : IFolder<TNumber, TNumber, TNumber>
        where TNumber : IMultiplicativeIdentity<TNumber, TNumber>, IMultiplyOperators<TNumber, TNumber, TNumber>
    {
        public static readonly ProductFolder<TNumber> Instance = new();

        private ProductFolder()
        {

        }

        public TNumber Start => TNumber.MultiplicativeIdentity;

        public TNumber Fold(TNumber state, TNumber input)
        {
            return state * input;
        }

        public TNumber Transform(TNumber state)
        {
            return state;
        }
    }
}
