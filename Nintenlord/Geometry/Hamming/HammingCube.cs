using Nintenlord.Graph;
using Nintenlord.Trees;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Geometry.Hamming
{
    public sealed class HammingCube : IGraph<HammingCode>, ITree<HammingCode>
    {
        readonly static ConcurrentDictionary<int, HammingCube> values = new();

        public static HammingCube ForDimension(int dimension)
        {
            return values.GetOrAdd(dimension, d => new HammingCube(d));
        }

        public int Dimensions { get; }

        private HammingCube(int dimensions)
        {
            Dimensions = dimensions;
        }

        public IEnumerable<HammingCode> Nodes => Enumerable.Range(0, 1 << Dimensions).Select(i => new HammingCode(i, Dimensions));

        public HammingCode Root => new(0, Dimensions);

        public IEnumerable<HammingCode> GetNeighbours(HammingCode node)
        {
            return node.GetNeighbors();
        }

        public bool IsEdge(HammingCode from, HammingCode to)
        {
            return from.GetDistance(to) == 1;
        }

        public IEnumerable<HammingCode> GetChildren(HammingCode node)
        {
            return node.GetLargerNeighbors();
        }
    }
}
