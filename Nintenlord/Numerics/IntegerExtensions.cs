using Nintenlord.Collections;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Numerics
{
    /// <summary>
    /// Extensions and helper methods to integers
    /// </summary>
    public static class IntegerExtensions
    {
        public static bool IsInRange(this int i, int min, int max)
        {
            return i <= max && i >= min;
        }

        public static bool IsInRangeHO(this int i, int min, int max)
        {
            return i < max && i >= min;
        }

        public static void Clamp(ref int i, int min, int max)
        {
            if (i < min)
            {
                i = min;
            }
            else if (i > max)
            {
                i = max;
            }
        }
        public static int Clamp(this int i, int min, int max)
        {
            return i < min ? min :
                   i > max ? max : i;
        }

        public static int ToMod(this int i, int mod)
        {
            if (i % mod != 0)
            {
                i += mod - i % mod;
            }
            return i;
        }

        public static void ToMod(ref int i, int mod)
        {
            if (i % mod != 0)
            {
                i += mod - i % mod;
            }
        }

        public static string ToHexString(this int i, string prefix)
        {
            return i.ToHexString(prefix, "");
        }
        public static string ToHexString(this int i, string prefix, string postfix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper() + postfix;
        }

        public static string ToBinString(this int i, string postfix)
        {
            return i.ToBinString("", postfix);
        }
        public static string ToBinString(this int i, string prefix, string postfix)
        {
            return prefix + Convert.ToString(i, 2) + postfix;
        }

        public static bool Intersects(int index1, int length1, int index2, int length2)
        {
            if (length1 == 0 || length2 == 0)
            {
                return false;
            }

            return index1 < index2 + length2 && index1 >= index2 ||
                   index2 < index1 + length1 && index2 >= index1;
        }

        public static int ToPower2(this int value)
        {
            ToPower2(ref value);
            return value;
        }

        public static void ToPower2(ref int value)
        {
            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value++;
        }

        public static int TrailingZeroCount(this int value)
        {
            if (value == 0)
            {
                return 32;
            }

            int result = 0;

            if ((value & 0x0000FFFF) == 0)
            {
                result += 16;
                value >>= 16;
            }
            if ((value & 0x000000FF) == 0)
            {
                result += 8;
                value >>= 8;
            }
            if ((value & 0x0000000F) == 0)
            {
                result += 4;
                value >>= 4;
            }
            if ((value & 0x00000003) == 0)
            {
                result += 2;
                value >>= 2;
            }
            if ((value & 0x00000001) == 0)
            {
                result += 1;
                //value >>= 1;
            }

            return result;
        }

        public static int LeadingZeroCount(this int value)
        {
            if (value == 0)
            {
                return 32;
            }

            int result = 0;

            if ((value & 0xFFFF0000) == 0)
            {
                result += 16;
                value <<= 16;
            }
            if ((value & 0xFF000000) == 0)
            {
                result += 8;
                value <<= 8;
            }
            if ((value & 0xF0000000) == 0)
            {
                result += 4;
                value <<= 4;
            }
            if ((value & 0xC0000000) == 0)
            {
                result += 2;
                value <<= 2;
            }
            if ((value & 0x80000000) == 0)
            {
                result += 1;
                //value <<= 1;
            }

            return result;
        }

        public static int TrailingZeroCount(this long value)
        {
            if (value == 0)
            {
                return 64;
            }

            int result = 0;

            if ((value & 0x00000000FFFFFFFF) == 0)
            {
                result += 32;
                value >>= 32;
            }
            if ((value & 0x000000000000FFFF) == 0)
            {
                result += 16;
                value >>= 16;
            }
            if ((value & 0x00000000000000FF) == 0)
            {
                result += 8;
                value >>= 8;
            }
            if ((value & 0x000000000000000F) == 0)
            {
                result += 4;
                value >>= 4;
            }
            if ((value & 0x0000000000000003) == 0)
            {
                result += 2;
                value >>= 2;
            }
            if ((value & 0x0000000000000001) == 0)
            {
                result += 1;
                //value >>= 1;
            }

            return result;
        }

        public static int LeadingZeroCount(this ulong value)
        {
            if (value == 0)
            {
                return 64;
            }

            int result = 0;

            if ((value & 0xFFFFFFFF00000000) == 0)
            {
                result += 32;
                value <<= 32;
            }
            if ((value & 0xFFFF000000000000) == 0)
            {
                result += 16;
                value <<= 16;
            }
            if ((value & 0xFF00000000000000) == 0)
            {
                result += 8;
                value <<= 8;
            }
            if ((value & 0xF000000000000000) == 0)
            {
                result += 4;
                value <<= 4;
            }
            if ((value & 0xC000000000000000) == 0)
            {
                result += 2;
                value <<= 2;
            }
            if ((value & 0x8000000000000000) == 0)
            {
                result += 1;
                //value <<= 1;
            }

            return result;
        }

        public static IEnumerable<int> GetIntegers(int start, int end, int step = 1)
        {
            for (int i = start; i <= end; i += step)
            {
                yield return i;
            }
        }

        [Obsolete("Use System.Linq.Enumerable.Range instead", true)]
        public static IEnumerable<int> GetRange(int start, int length)
        {
            for (int i = start; i < start + length; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<int> GetIntegersNear(this int x, int range)
        {
            for (int i = x - range; i <= x + range; i++)
            {
                yield return i;
            }
        }

        public static int Lerp(int min, int max, double val, MidpointRounding roundingMode)
        {
            return min + (int)Math.Round((max - min) * val, roundingMode);
        }

        public static IEnumerable<IEnumerable<int>> Compositions(this int number)
        {
            if (number == 0)
            {
                yield return Enumerable.Empty<int>();
            }
            if (number == 1)
            {
                yield return 1.Return();
            }
            else
            {
                for (int i = 0; i < number; i++)
                {
                    foreach (var item in i.Compositions())
                    {
                        yield return item.Prepend(number - i);
                    }
                }
            }
        }

        public static IEnumerable<int> BaseNRepresentation(this int value, int baseSize)
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

        public static int CountOneBits(this int number)
        {
            int result = 0;
            while (number > 0)
            {
                number &= number - 1;
                result++;
            }
            return result;
        }

        public static int CountOneBits(this long number)
        {
            int result = 0;
            while (number > 0)
            {
                number &= number - 1;
                result++;
            }
            return result;
        }

        public static int Shift(this int value, int by)
        {
            if (by < 0)
            {
                return value >> -by;
            }
            else
            {
                return value << by;
            }
        }

        public static int SwapBits(this int value, int a, int b)
        {
            int maskA = 1 << a;
            int maskB = 1 << b;

            var diffAB = a - b;
            var diffBA = b - a;
            var valA = (value & maskA).Shift(-diffAB);
            var valB = (value & maskB).Shift(-diffBA);
            var rest = value & ~(maskA | maskB);

            return rest | valA | valB;
        }

        public static int SwapBits(this int value, int a, int b, int c)
        {
            int maskA = 1 << a;
            int maskB = 1 << b;
            int maskC = 1 << c;

            var diffAB = a - b;
            var diffBC = b - c;
            var diffCA = c - a;
            var valA = (value & maskA).Shift(-diffAB);
            var valB = (value & maskB).Shift(-diffBC);
            var valC = (value & maskC).Shift(-diffCA);
            var rest = value & ~(maskA | maskB | maskC);

            return rest | valA | valB | valC;
        }

        public static int SwapBits(this int value, int a, int b, int c, int d)
        {
            int maskA = 1 << a;
            int maskB = 1 << b;
            int maskC = 1 << c;
            int maskD = 1 << d;

            var diffAB = a - b;
            var diffBC = b - c;
            var diffCD = c - d;
            var diffDA = d - a;
            var valA = (value & maskA).Shift(-diffAB);
            var valB = (value & maskB).Shift(-diffBC);
            var valC = (value & maskC).Shift(-diffCD);
            var valD = (value & maskD).Shift(-diffDA);
            var rest = value & ~(maskA | maskB | maskC | maskD);

            return rest | valA | valB | valC | valD;
        }

        public static IEnumerable<bool> GetBits(this int value)
        {
            for (int i = 0; i < 32; i++)
            {
                yield return (value & 1 << i) != 0;
            }
        }

        public static IEnumerable<int> GetOneIndicis(this int value)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((value & 1 << i) != 0)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<int> GetZeroIndicis(this int value)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((value & 1 << i) == 0)
                {
                    yield return i;
                }
            }
        }

        public static int BinomialCoefficient(int n, int k)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n needs to be non-negative");
            }

            if (k < 0 || k > n)
            {
                return 0;
            }

            static int Inner(int n, int k)
            {
                if (k == 0 || k == n)
                {
                    return 1;
                }
                return Inner(n - 1, k - 1) + Inner(n - 1, k);
            }

            return Inner(n, k);
        }

        public static IEnumerable<int> BinomialCoefficients(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n needs to be non-negative");
            }

            return Enumerable.Range(1, n)
                .Select(k => (n + 1 - k, k))
                .Scan(1, (acc, tuple) => acc * tuple.Item1 / tuple.k);
        }

        public static int GreatestCommonDivisor(this int a, int b)
        {
            static int GDC(int n, int m) => m == 0 ? n : GDC(m, n % m);

            return a > b ? GDC(a, b) : GDC(b, a);
        }

        public static int GreatestCommonDivisor(this IEnumerable<int> numbers)
        {
            if (numbers is null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            return numbers.Aggregate(0, GreatestCommonDivisor);
        }

        public static int LeastCommonDivider(this int a, int b)
        {
            return a * b / a.GreatestCommonDivisor(b);
        }

        public static int LeastCommonDivider(this IEnumerable<int> numbers)
        {
            if (numbers is null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            return numbers.Aggregate(1, LeastCommonDivider);
        }
    }
}
