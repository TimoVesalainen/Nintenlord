using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nintenlord.Collections
{
    public sealed class BitArrayComparer : IEqualityComparer<BitArray>
    {
        public static readonly BitArrayComparer Instance = new();

        private BitArrayComparer()
        {
        }

        public bool Equals(BitArray x, BitArray y)
        {
            if (x == y)
            {
                return true;
            }
            if (x?.Count != y?.Count)
            {
                return false;
            }
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode([DisallowNull] BitArray obj)
        {
            int hash = 0;
            var length = Math.Min(obj.Count, 31);
            for (int i = 0; i < length; i++)
            {
                hash |= (obj[i] ? 1 : 0) << i;
            }
            return hash;
        }
    }
}
