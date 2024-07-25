using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class AllFolder<T> : IFolder<T, bool, bool>
    {
        readonly Predicate<T> predicate;

        public AllFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public bool Start => true;

        public (bool state, bool skipRest) FoldMaybe(bool state, T input)
        {
            var newState = state && predicate(input);
            return (newState, !newState);
        }

        public bool Transform(bool state)
        {
            return state;
        }
    }
}
