using System;

namespace Nintenlord.Geometry.Hamming
{
    public sealed class HammingCubeColouring<T> : IHammingCubeColouring<T>
    {
        readonly int dimensions;
        readonly T[] colours;

        public HammingCubeColouring(int dimensions, T[] colours)
        {
            if (dimensions <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            }

            if (1 << dimensions != colours.Length)
            {
                throw new ArgumentException(nameof(colours), "Array length is wrong");
            }

            this.dimensions = dimensions;
            this.colours = colours ?? throw new ArgumentNullException(nameof(colours));
        }

        public int Dimensions => dimensions;

        public T GetColour(in HammingCode vertex)
        {
            if (vertex.Dimensions != dimensions)
            {
                throw new ArgumentException("Vertex is from wrong dimension count", nameof(vertex));
            }

            return colours[vertex.Index];
        }
    }
}
