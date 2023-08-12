using Nintenlord.Utility;

namespace Nintenlord.Collections.Foldable
{
    public sealed class LastFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        public readonly static LastFolder<T> Instance = new();

        private LastFolder()
        {
        }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            return input;
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state.Value;
        }
    }
}
