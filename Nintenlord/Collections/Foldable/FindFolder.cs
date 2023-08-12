using Nintenlord.Utility;
using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class FindFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        readonly Predicate<T> predicate;

        public FindFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            if (state.HasValue)
            {
                return state;
            }

            if (predicate(input))
            {
                return input;
            }
            return state;
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state;
        }
    }
}
