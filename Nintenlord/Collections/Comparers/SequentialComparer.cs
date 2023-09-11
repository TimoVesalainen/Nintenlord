using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Comparers
{
    public sealed class SequentialComparer<T> : IComparer<T>
    {
        readonly IComparer<T>[] comparers;

        public SequentialComparer(IEnumerable<IComparer<T>> comparers)
        {
            if (comparers is null)
            {
                throw new ArgumentNullException(nameof(comparers));
            }

            this.comparers = comparers.ToArray();
        }

        public int Compare(T x, T y)
        {
            for (int i = 0; i < comparers.Length; i++)
            {
                var result = comparers[i].Compare(x, y);
                if (result != 0)
                {
                    return result;
                }
            }
            return 0;
        }
    }
}
