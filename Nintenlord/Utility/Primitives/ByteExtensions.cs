using System;
using System.Text;

namespace Nintenlord.Utility.Primitives
{
    /// <summary>
    /// Extensions for byte and arrays of it
    /// </summary>
    public static class ByteExtensions
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

        public static bool IsInRange(this byte i, byte min, byte max)
        {
            return i <= max && i >= min;
        }

        public static int Clamp(this byte i, byte min, byte max)
        {
            return i < min ? min :
                   i > max ? max : i;
        }

        public static string ToHexString(this byte i, string prefix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper();
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
            int firstByteBits = (8 - position & 0x7) & 0x7;

            int endIndex = (position + length) / 8;
            int lastByteBits = (position + length) & 0x7;

            int resultLength = endIndex;

            if (lastByteBits != 0)
            {
                resultLength++;
            }
            result = new byte[resultLength];
            if (((position & 0x7) + length) < 9)
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


        public static string ToString(this byte[] i, int bytesPerWord)
        {
            StringBuilder result = new StringBuilder();
            for (int j = 0; j < i.Length; j++)
            {
                result.Append(i[j].ToHexString("").PadLeft(2, '0'));
                if (j % bytesPerWord == bytesPerWord - 1)
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
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
                return ShiftLeft(array, toShift);
            }
            else
            {
                return ShiftRight(array, -toShift);
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
                byte value = (byte)((result[i] << bitsToMove) | (temp >> (8 - bitsToMove)));
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
                byte value = (byte)((result[i] >> bitsToMove) | (temp << (8 - bitsToMove)));
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
                result[i] = (byte)(~array[i]);
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
                array[i] = (byte)(~array[i]);
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
            value = (byte)(((value & 0xF0) >> 4) | ((value & 0x0F) << 4));
            //Swap 2 bit groups
            value = (byte)(((value & 0xCC) >> 2) | ((value & 0x33) << 2));
            //Swap 1 bit groups
            value = (byte)(((value & 0xAA) >> 1) | ((value & 0x55) << 1));
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
    }
}
