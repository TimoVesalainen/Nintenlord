﻿using System;

namespace Nintenlord.Collections.Foldable.EqualityComparers
{
    public sealed class FirstIndexOfFolder<T> : IFolder<T, (int index, int found), int>
    {
        readonly Predicate<T> predicate;

        public FirstIndexOfFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public (int index, int found) Start => (0, -1);

        public ((int index, int found) state, bool skipRest) FoldMayEnd((int index, int found) state, T input)
        {
            var (index, found) = state;
            if (found == -1)
            {
                if (predicate(input))
                {
                    return ((index + 1, index), true);
                }

                return ((index + 1, found), false);
            }
            else
            {
                return ((index + 1, found), true);
            }
        }

        public int Transform((int index, int found) state)
        {
            return state.found;
        }
    }
}
