using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class AmountOfFolderInt<T> : IFolder<T, int, int>
    {
        readonly Predicate<T> predicate;

        public AmountOfFolderInt(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public int Start => 0;

        public int Fold(int state, T input)
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

        public int Transform(int state)
        {
            return state;
        }
    }
}
