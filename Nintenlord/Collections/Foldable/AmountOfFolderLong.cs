using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class AmountOfFolderLong<T> : IFolder<T, long, long>
    {
        readonly Predicate<T> predicate;

        public AmountOfFolderLong(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public long Start => 0;

        public long Fold(long state, T input)
        {
            if (predicate(input))
            {
                return state + 1;
            }
            else
            {
                return state;
            }
        }

        public long Transform(long state)
        {
            return state;
        }
    }
}
