using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        readonly IEqualityComparer<T> itemEqualityComparer;

        public ArrayEqualityComparer(IEqualityComparer<T> itemEqualityComparer)
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
