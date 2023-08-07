using System;
using System.Collections;

namespace Nintenlord.Geometry.Hamming
{
    public sealed class HammingCubeBitColouring : IHammingCubeColouring<bool>
    {
        readonly int dimensions;
        readonly BitArray colours;

        public HammingCubeBitColouring(int dimensions, BitArray colours)
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

        public bool GetColour(in HammingCode vertex)
        {
            if (vertex.Dimensions != dimensions)
            {
                throw new ArgumentException("Vertex is from wrong dimension count", nameof(vertex));
            }

            return colours[vertex.Index];
        }
    }
}
