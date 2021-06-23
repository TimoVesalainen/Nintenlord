using Nintenlord.Utility.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Utility
{
    public sealed class BinaryMatrix : IEquatable<BinaryMatrix>
    {
        public int Width { get; }
        public int Height { get; }

        readonly byte[] bits;
        readonly int bytesPerRow;
        readonly int bitsInLastByteColumn;

        public BinaryMatrix(int width, int height)
        {
            Width = width;
            Height = height;

            var bytesPerRow = Math.DivRem(width, 8, out var restBits);
            if (restBits > 0)
            {
                bytesPerRow++;
            }
            this.bits = new byte[bytesPerRow * height];
            this.bytesPerRow = bytesPerRow;
            this.bitsInLastByteColumn = restBits == 0 ? 8 : restBits;
        }

        private BinaryMatrix(int width, int height, byte[] bits, int bytesPerRow, int bitsInLastByteRow)
        {
            this.bits = bits;
            this.bytesPerRow = bytesPerRow;
            this.bitsInLastByteColumn = bitsInLastByteRow;
            Width = width;
            Height = height;
        }

        public bool this[int x, int y]
        {
            get
            {
                var (byteIndex, bitIndex) = GetIndex(x, y);
                var byteValue = bits[byteIndex];

                var mask = Mask(bitIndex);

                return (byteValue & mask) > 0;
            }
            set
            {
                var (byteIndex, bitIndex) = GetIndex(x, y);
                var byteValue = bits[byteIndex];

                var (mask, antiMask) = Mask2(bitIndex);

                byteValue = (byte)((antiMask & byteValue) | (value ? mask : 0));

                bits[byteIndex] = byteValue;
            }
        }

        private (int byteIndex, int bitIndex) GetIndex(int x, int y)
        {
            var bitIndex = x & 0x7;
            var xByte = x >> 3;

            var byteIndex = y * bytesPerRow + xByte;

            return (byteIndex, bitIndex);
        }

        static byte Mask(int index)
        {
            var mask = 1 << index;
            return (byte)mask;
        }

        static (byte mask, byte antiMask) Mask2(int index)
        {
            var mask = 1 << index;
            return ((byte)mask, (byte)(~mask & 0xFF));
        }

        public IEnumerable<bool> Row(int y)
        {
            var rowStart = y * bytesPerRow;
            for (int x = 0; x < bytesPerRow; x++)
            {
                var value = bits[x + rowStart];

                var bitCount = (x == bytesPerRow - 1) ? 8 : bitsInLastByteColumn;

                for (int j = 0; j < bitCount; j++)
                {
                    yield return (value & (1 << j)) != 0;
                }
            }
        }

        public IEnumerable<bool> Column(int x)
        {
            var (byteIndex, bitIndex) = GetIndex(x, 0);
            var mask = Mask(bitIndex);

            for (int y = 0; y < Height; y++)
            {
                var value = bits[byteIndex + y * bytesPerRow];
                yield return (value & mask) > 0;
            }
        }

        public bool Equals(BinaryMatrix other)
        {
            if (this.Width != other.Width || this.Height != other.Height)
            {
                return false;
            }

            for (int i = 0; i < this.bits.Length; i++)
            {
                if (this.bits[i] != other.bits[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is BinaryMatrix bin && Equals(bin);
        }

        public override int GetHashCode()
        {
            int value = 0;

            for (int i = 0; i < bits.Length; i++)
            {
                value = value * 13 + bits[i];
            }

            return value;
        }

        private BinaryMatrix WithBits(byte[] bits)
        {
            if (this.bits.Length != bits.Length)
            {
                throw new ArgumentException("Wrong length array", nameof(bits));
            }

            return new BinaryMatrix(Width, Height, bits, bytesPerRow, bitsInLastByteColumn);
        }

        public BinaryMatrix Invert()
        {
            //TODO: Set things in array outside values to zero, to preserve equality
            var invertedBits = ByteExtensions.Neg(bits);

            return this.WithBits(invertedBits);
        }

        public BinaryMatrix And(BinaryMatrix other)
        {
            if (this.Width != other.Width || this.Height != other.Height)
            {
                throw new ArgumentException("Wrong size matrix", nameof(other));
            }

            var bits = this.bits.And(other.bits);

            return this.WithBits(bits);
        }

        public BinaryMatrix Or(BinaryMatrix other)
        {
            if (this.Width != other.Width || this.Height != other.Height)
            {
                throw new ArgumentException("Wrong size matrix", nameof(other));
            }

            var bits = this.bits.Or(other.bits);

            return this.WithBits(bits);
        }

        public BinaryMatrix Xor(BinaryMatrix other)
        {
            if (this.Width != other.Width || this.Height != other.Height)
            {
                throw new ArgumentException("Wrong size matrix", nameof(other));
            }

            var bits = this.bits.Xor(other.bits);

            return this.WithBits(bits);
        }

        public BinaryMatrix Transpose()
        {
            BinaryMatrix transpose = new BinaryMatrix(this.Height, this.Width);

            for (int y = 0; y < Height; y++)
            {
                foreach (var (bit, x) in Row(y).Select((b, x) => (b, x)))
                {
                    transpose[y, x] = bit;
                }
            }

            return transpose;
        }

        public BinaryMatrix Multiplication(BinaryMatrix other)
        {
            if (this.Width != other.Height)
            {
                throw new ArgumentException("Wrong size matrix", nameof(other));
            }

            BinaryMatrix result = new BinaryMatrix(this.Height, other.Width);

            var length = this.Width;
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    bool bit = false;
                    for (int i = 0; i < length; i++)
                    {
                        bit ^= this[i, y] & other[x, i];
                    }
                    result[x, y] = bit;
                }
            }

            return result;
        }

        public BinaryMatrix LogicalMultiplication(BinaryMatrix other)
        {
            if (this.Width != other.Height)
            {
                throw new ArgumentException("Wrong size matrix", nameof(other));
            }

            BinaryMatrix result = new BinaryMatrix(this.Height, other.Width);

            var length = this.Width;
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    bool bit = false;
                    for (int i = 0; i < length; i++)
                    {
                        if (this[i, y] & other[x, i])
                        {
                            bit = true;
                            break;
                        }
                    }
                    result[x, y] = bit;
                }
            }

            return result;
        }

        public static BinaryMatrix Create(Func<int, int, bool> func, int width, int height)
        {
            var bytesPerRow = Math.DivRem(width, 8, out var restBits);
            if (restBits > 0)
            {
                bytesPerRow++;
            }
            var bits = new byte[bytesPerRow * height];
            var bitsInLastByteColumn = restBits == 0 ? 8 : restBits;

            for (int y = 0; y < height; y++)
            {
                int rowIndexStart = y * bytesPerRow;
                for (int x = 0; x < bytesPerRow; x++)
                {
                    byte value = 0;
                    var bitsToGet = x == bytesPerRow - 1 ? bitsInLastByteColumn : 8;

                    for (int i = 0; i < bitsToGet; i++)
                    {
                        if (func(x * 8 + i, y))
                        {
                            value |= (byte)(1 << i);
                        }
                    }

                    bits[rowIndexStart + x] = value;
                }
            }

            return new BinaryMatrix(width, height, bits, bytesPerRow, bitsInLastByteColumn);
        }
    }
}
