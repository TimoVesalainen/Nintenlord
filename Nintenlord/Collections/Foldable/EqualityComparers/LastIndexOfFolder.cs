using System;

namespace Nintenlord.Collections.Foldable.EqualityComparers
{
    public sealed class LastIndexOfFolder<T> : IFolder<T, (int index, int found), int>
    {
        readonly Predicate<T> predicate;

        public LastIndexOfFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public (int index, int found) Start => (0, -1);

        public (int index, int found) Fold((int index, int found) state, T input)
        {
            var (index, found) = state;
            if (predicate(input))
            {
                found = index;
            }

            return (index + 1, found);
        }

        public int Transform((int index, int found) state)
        {
            return state.found;
        }
    }
}
