using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        private static readonly ConcurrentDictionary<IEqualityComparer<T>, ArrayEqualityComparer<T>> cache = new();

        public static ArrayEqualityComparer<T> Default => Create(EqualityComparer<T>.Default);

        public static ArrayEqualityComparer<T> Create(IEqualityComparer<T> itemEqualityComparer)
        {
            if (itemEqualityComparer is null)
            {
                throw new ArgumentNullException(nameof(itemEqualityComparer));
            }

            return cache.GetOrAdd(itemEqualityComparer, eq => new ArrayEqualityComparer<T>(eq));
        }

        readonly IEqualityComparer<T> itemEqualityComparer;

        private ArrayEqualityComparer(IEqualityComparer<T> itemEqualityComparer)
        {
            this.itemEqualityComparer = itemEqualityComparer;
        }

        public bool Equals(T[] x, T[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (itemEqualityComparer.Equals(x[i], y[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public int GetHashCode(T[] obj)
        {
            unchecked
            {
                int code = 0;

                foreach (var ob in obj)
                {
                    code *= 11;
                    code += itemEqualityComparer.GetHashCode(ob);
                }

                return code;
            }
        }
    }
}
