using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Nintenlord.Utility.Primitives
{
    public static class BigIntegerHelpers
    {
        public static IEnumerable<BigInteger> BaseNRepresentation(this BigInteger value, BigInteger baseSize)
        {
            if (baseSize == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseSize), "Base can't be zero");
            }

            while (value > 0)
            {
                yield return value % baseSize;
                value /= baseSize;
            }
        }

        public static IEnumerable<int> BaseNRepresentation(this BigInteger value, int baseSize)
        {
            return value.BaseNRepresentation((BigInteger)baseSize).Select(i => (int)i);
        }
    }
}
