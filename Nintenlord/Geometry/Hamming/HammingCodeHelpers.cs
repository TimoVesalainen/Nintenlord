using Nintenlord.Numerics;
using System.Numerics;

namespace Nintenlord.Geometry.Hamming
{
    public static class HammingCodeHelpers
    {
        public static int HammingDistance(this int value, int other)
        {
            return value.HammingDistance(other);
        }

        public static int HammingDistance<T>(this T value, T other)
            where T :
            IBitwiseOperators<T, T, T>,
            IComparisonOperators<T, T, bool>,
            IAdditiveIdentity<T, T>,
            IDecrementOperators<T>
        {
            var xor = value ^ other;

            return xor.CountOneBits<T, int>();
        }
    }
}
