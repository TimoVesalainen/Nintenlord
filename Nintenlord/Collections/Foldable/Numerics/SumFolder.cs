using System.Numerics;

namespace Nintenlord.Collections.Foldable.Numerics
{
    public sealed class SumFolder<TNumber, TNumberResult> : IFolder<TNumber, TNumberResult, TNumberResult>
        where TNumberResult : IAdditiveIdentity<TNumberResult, TNumberResult>, IAdditionOperators<TNumberResult, TNumber, TNumberResult>
    {
        public static readonly SumFolder<TNumber, TNumberResult> Instance = new();

        private SumFolder()
        {

        }

        public TNumberResult Start => TNumberResult.AdditiveIdentity;

        public TNumberResult Fold(TNumberResult state, TNumber input)
        {
            return state + input;
        }

        public TNumberResult Transform(TNumberResult state)
        {
            return state;
        }
    }
}
