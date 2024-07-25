using Nintenlord.Utility;
using System;

namespace Nintenlord.Collections.Foldable.EqualityComparers
{
    public sealed class LastFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        public readonly static LastFolder<T> Instance = new(x => true);

        private readonly Predicate<T> predicate;

        public LastFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            if (predicate(input))
            {
                return input;
            }
            else
            {
                return state;
            }
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state;
        }
    }
}
