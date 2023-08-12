using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public sealed class FirstIndexOfFolder<T> : IFolder<T, (int index, int found), int>
    {
        readonly Predicate<T> predicate;

        public FirstIndexOfFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public (int index, int found) Start => (0, -1);

        public (int index, int found) Fold((int index, int found) state, T input)
        {
            var (index, found) = state;
            if (found != -1)
            {
                if (predicate(input))
                {
                    found = index;
                }

                return (index + 1, found);
            }
            else
            {
                return (index + 1, found);
            }
        }

        public int Transform((int index, int found) state)
        {
            return state.found;
        }
    }
}
