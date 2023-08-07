using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nintenlord.Geometry.Hamming
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct HammingCode : IEquatable<HammingCode>
    {
        readonly int buffer;
        readonly int dimensions;

        public int Index => buffer;

        public HammingCode(int buffer, int dimensions)
        {
            if (dimensions <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions), dimensions, "Value should be positive");
            }
            if (buffer >= 1 << dimensions || buffer < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer), buffer, "Value should be non-negative, and less than 2^dimensions");
            }

            this.buffer = buffer;
            this.dimensions = dimensions;
        }

        public IEnumerable<HammingCode> GetNeighbors()
        {
            for (int i = 0; i < dimensions; i++)
            {
                yield return new HammingCode(buffer ^ 1 << i, dimensions);
            }
        }

        public IEnumerable<HammingCode> GetLargerNeighbors()
        {
            for (int i = 0; i < dimensions; i++)
            {
                var setBit = new HammingCode(buffer | 1 << i, dimensions);
                if (setBit != this)
                {
                    yield return setBit;
                }
            }
        }

        public HammingCode GetOpposite()
        {
            var mask = (1 << dimensions) - 1;
            return new HammingCode(buffer ^ mask, dimensions);
        }

        public int GetDistance(in HammingCode other)
        {
            if (dimensions != other.dimensions)
            {
                throw new ArgumentException("Code from wrong diemnsion", nameof(other));
            }

            return buffer.HammingDistance(other.buffer);
        }

        public bool Equals(HammingCode other)
        {
            return other.dimensions == dimensions &&
                other.buffer == buffer;
        }

        public static bool operator ==(HammingCode code1, HammingCode code2)
        {
            return code1.Equals(code2);
        }

        public static bool operator !=(HammingCode code1, HammingCode code2)
        {
            return !code1.Equals(code2);
        }

        public override bool Equals(object obj)
        {
            return obj is HammingCode code && Equals(code);
        }

        public override int GetHashCode()
        {
            return buffer;
        }

        private string InnerToString() => Convert.ToString(buffer, 2).PadLeft(dimensions, '0');

        public override string ToString()
        {
            return $"[Code {InnerToString()}]";
        }

        private string GetDebuggerDisplay()
        {
            return InnerToString();
        }
    }
}
