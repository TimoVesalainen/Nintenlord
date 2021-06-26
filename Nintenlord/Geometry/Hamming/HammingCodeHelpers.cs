using Nintenlord.Utility.Primitives;

namespace Nintenlord.Geometry.Hamming
{
    public static class HammingCodeHelpers
    {
        public static int HammingDistance(this int value, int other)
        {
            var xor = value ^ other;

            return xor.CountOneBits();
        }
    }
}
