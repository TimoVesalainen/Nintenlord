using Nintenlord.Collections;
using Nintenlord.Trees;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Geometry
{
    public static partial class SplitTree
    {
        public static ITree<NParallerogram<TVector>> GetTree<TVector>(TVector start, TVector[] edges)
            where TVector : IAdditionOperators<TVector, TVector, TVector>,
            IDivisionOperators<TVector, double, TVector>
        {
            var edgeCount = edges.Length;

            IEnumerable<NParallerogram<TVector>> GetChildren(NParallerogram<TVector> node)
            {
                var nextEdges = node.Edges.Select(edge => edge / 2).ToArray();
                return BitArrayHelpers.GetAllArrays(edgeCount).Select(bits => {
                    var start = bits.EnumerateBits()
                    .Zip(nextEdges, (include, end) => (include, end))
                    .Where(t => t.include)
                    .Aggregate(node.Start, (accum, t) =>  accum + t.end);

                    return new NParallerogram<TVector>(start, nextEdges);
                });
            }

            return new LambdaTree<NParallerogram<TVector>>(new NParallerogram<TVector>(start, edges), GetChildren);
        }
    }
}
