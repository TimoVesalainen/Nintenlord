using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Numerics
{
    public static class BitHelpers
    {
        public static byte Shift(this byte i, int amount)
        {
            if (amount >= 0)
            {
                return (byte)(i << amount);
            }
            else
            {
                return (byte)(i >> -amount);
            }
        }

        public static byte GetBits(this byte i, int position, int length)
        {
            return (byte)(i & GetMask(position, length));
        }


        public static byte GetMask(int position, int length)
        {
            if (length < 0 || position < 0 || position + length > sizeof(byte) * 8)
            {
                throw new IndexOutOfRangeException();
            }
            byte mask = byte.MaxValue;
            unchecked
            {
                mask >>= sizeof(byte) * 8 - length;
                mask <<= position;
            }

            return mask;
        }

        public static byte[] GetMaskArray(int position, int length)
        {
            byte[] result;

            int begIndex = position / 8;
            int firstByteBits = 8 - position & 0x7 & 0x7;

            int endIndex = (position + length) / 8;
            int lastByteBits = position + length & 0x7;

            int resultLength = endIndex;

            if (lastByteBits != 0)
            {
                resultLength++;
            }
            result = new byte[resultLength];
            if ((position & 0x7) + length < 9)
            {
                result[begIndex] = GetMask(position & 0x7, length);
            }
            else
            {
                if (firstByteBits != 0)
                {
                    result[begIndex] = GetMask(8 - firstByteBits, firstByteBits);
                    begIndex++;
                }

                if (lastByteBits != 0)
                {
                    result[endIndex] = GetMask(0, lastByteBits);
                    //endIndex--;
                }

                for (int j = begIndex; j < endIndex; j++)
                {
                    result[j] = 0xFF;
                }

            }

            return result;
        }

        public static byte[] GetBits(this byte[] i, int position, int length)
        {
            if (position < 0 || length < 0 || position + length > i.Length * 8)
            {
                throw new IndexOutOfRangeException();
            }

            if (length == 0)
            {
                return new byte[0];
            }

            int byteIndex = position / 8;
            int bitIndex = position % 8;

            int byteLength = length / 8;
            int bitLength = length % 8;
            int bitTail = (position + length) % 8;

            int resultLength = byteLength;
            if (bitTail > 0)
            {
                resultLength++;
            }

            if (bitIndex > 0)
            {
                resultLength++;
            }

            byte[] result = new byte[resultLength];

            int toTrim = resultLength - byteLength;
            if (bitLength > 0)
            {
                toTrim--;
            }

            Array.Copy(i, byteIndex, result, 0, Math.Min(result.Length, i.Length));

            if (bitIndex != 0)
            {
                result = result.ShiftRight(bitIndex);
            }

            if (toTrim > 0)
            {
                Array.Resize(ref result, result.Length - toTrim);
            }

            if (bitLength != 0)
            {
                result[result.Length - 1] &= GetMask(0, bitLength);
            }

            return result;
        }

        /// <summary>
        /// Shifts bytes in array. Assumes bytes are in little endian order and 
        /// low priority bits are first
        /// </summary>
        /// <remarks>Could be made faster with using uints...</remarks>
        /// <param name="array">Array to shift</param>
        /// <param name="toShift">Positive means left shifting, negative right</param>
        /// <returns>New shifted array</returns>
        public static byte[] Shift(this byte[] array, int toShift)
        {
            if (toShift == 0)// <_<
            {
                return array.Clone() as byte[];
            }
            else if (toShift > 0)
            {
                return array.ShiftLeft(toShift);
            }
            else
            {
                return array.ShiftRight(-toShift);
            }
        }

        private static byte[] ShiftLeft(this byte[] array, int toShift)
        {
            int bytesToMove = toShift / 8;
            int bitsToMove = toShift % 8;
            byte[] result = new byte[array.Length + bytesToMove + bitsToMove == 0 ? 0 : 1];

            Array.Copy(array, 0, result, bytesToMove, array.Length);

            byte mask = GetMask(8 - bitsToMove, bitsToMove);
            byte temp = 0;
            for (int i = 0; i < result.Length; i++)
            {
                byte value = (byte)(result[i] << bitsToMove | temp >> 8 - bitsToMove);
                temp = (byte)(result[i] & mask);
                result[i] = value;
            }

            return result;
        }

        private static byte[] ShiftRight(this byte[] array, int toShift)
        {
            if (array.Length * 8 <= toShift)
            {
                return new byte[0];
            }

            int bytesToMove = toShift / 8;
            int bitsToMove = toShift % 8;
            byte[] result = new byte[array.Length - bytesToMove];

            Array.Copy(array, bytesToMove, result, 0, result.Length);

            byte mask = GetMask(0, bitsToMove);
            byte temp = 0;
            for (int i = result.Length - 1; i >= 0; i--)
            {
                byte value = (byte)(result[i] >> bitsToMove | temp << 8 - bitsToMove);
                temp = (byte)(result[i] & mask);
                result[i] = value;
            }

            return result;
        }


        public static byte[] And(this byte[] array, byte[] array2)
        {
            byte[] result = new byte[Math.Max(array.Length, array2.Length)];
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                result[i] = (byte)(array[i] & array2[i]);
            }
            return result;
        }
        public static byte[] Or(this byte[] array, byte[] array2)
        {
            byte[] result = new byte[Math.Max(array.Length, array2.Length)];
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                result[i] = (byte)(array[i] | array2[i]);
            }
            if (array.Length > array2.Length)
            {
                Array.Copy(array, index, result, index, result.Length - index);
            }
            else if (array.Length < array2.Length)
            {
                Array.Copy(array2, index, result, index, result.Length - index);
            }

            return result;
        }
        public static byte[] Xor(this byte[] array, byte[] array2)
        {
            byte[] result = new byte[Math.Max(array.Length, array2.Length)];
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                result[i] = (byte)(array[i] ^ array2[i]);
            }
            if (array.Length > array2.Length)
            {
                Array.Copy(array, index, result, index, result.Length - index);
            }
            else if (array.Length < array2.Length)
            {
                Array.Copy(array2, index, result, index, result.Length - index);
            }
            return result;
        }
        public static byte[] Neg(this byte[] array)
        {
            byte[] result = new byte[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)~array[i];
            }
            return result;
        }
        public static void AndWith(this byte[] array, byte[] array2)
        {
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                array[i] = (byte)(array[i] & array2[i]);
            }
        }
        public static void OrWith(this byte[] array, byte[] array2)
        {
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                array[i] = (byte)(array[i] | array2[i]);
            }
        }
        public static void XorWith(this byte[] array, byte[] array2)
        {
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                array[i] = (byte)(array[i] ^ array2[i]);
            }
        }
        public static void NegWith(this byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)~array[i];
            }
        }

        public static void WriteTo(this byte[] array, int destination, byte[] source, int length)
        {
            int sourceIndex = 0;
            if (destination + length > array.Length * 8
             || sourceIndex + length > source.Length * 8
             || destination < 0
             || sourceIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < length; i++)
            {
                int destIndex = destination + i;
                int destByteIndex = destIndex / 8;
                int destbitIndex = destIndex % 8;
                int srcByteIndex = i / 8;
                int srcBitIndex = i % 8;
                byte srcMask = GetMask(srcBitIndex, 1);
                byte destMask = GetMask(destbitIndex, 1);
                array[destByteIndex] &= (byte)~destMask;
                if ((source[srcByteIndex] & srcMask) != 0)
                {
                    array[destByteIndex] |= (byte)(1 << destbitIndex);
                }
            }
        }

        /// <summary>
        /// Reverses bit order of a byte.
        /// https://stackoverflow.com/a/2602885
        /// </summary>
        public static byte ReverseBits(this byte value)
        {
            //Swap 4 bit groups
            value = (byte)((value & 0xF0) >> 4 | (value & 0x0F) << 4);
            //Swap 2 bit groups
            value = (byte)((value & 0xCC) >> 2 | (value & 0x33) << 2);
            //Swap 1 bit groups
            value = (byte)((value & 0xAA) >> 1 | (value & 0x55) << 1);
            return value;
        }

        public static byte SwapBits(this byte value, int a, int b)
        {
            int maskA = 1 << a;
            int maskB = 1 << b;

            var diffAB = a - b;
            var diffBA = b - a;
            var valA = (value & maskA).Shift(-diffAB);
            var valB = (value & maskB).Shift(-diffBA);
            var rest = value & ~(maskA | maskB);

            return (byte)(rest | valA | valB);
        }

        public static byte SwapBits(this byte value, int a, int b, int c)
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

            return (byte)(rest | valA | valB | valC);
        }

        public static byte SwapBits(this byte value, int a, int b, int c, int d)
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

            return (byte)(rest | valA | valB | valC | valD);
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

        public static TResult CountOneBits<T, TResult>(this T number)
            where T :
            IBitwiseOperators<T, T, T>,
            IComparisonOperators<T, T, bool>,
            IAdditiveIdentity<T, T>,
            IDecrementOperators<T>
            where TResult : IIncrementOperators<TResult>, IAdditiveIdentity<TResult, TResult>
        {
            TResult result = TResult.AdditiveIdentity;
            while (number > T.AdditiveIdentity)
            {
                number &= --number;
                result++;
            }
            return result;
        }

        public static int CountOneBits(this int number)
        {
            return CountOneBits<int, int>(number);
        }

        public static int CountOneBits(this long number)
        {
            return CountOneBits<long, int>(number);
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

    }
}
