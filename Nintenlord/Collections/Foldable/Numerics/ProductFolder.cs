using System.Numerics;

namespace Nintenlord.Collections.Foldable.Numerics
{
    public sealed class ProductFolder<TNumber, TNumberResult> : IFolder<TNumber, TNumberResult, TNumberResult>
        where TNumberResult : IMultiplicativeIdentity<TNumberResult, TNumberResult>, IMultiplyOperators<TNumberResult, TNumber, TNumberResult>
    {
        public static readonly ProductFolder<TNumber, TNumberResult> Instance = new();

        private ProductFolder()
        {

        }

        public TNumberResult Start => TNumberResult.MultiplicativeIdentity;

        public TNumberResult Fold(TNumberResult state, TNumber input)
        {
            return state * input;
        }

        public TNumberResult Transform(TNumberResult state)
        {
            return state;
        }
    }
}
