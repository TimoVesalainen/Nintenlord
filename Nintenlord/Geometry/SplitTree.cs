using Nintenlord.Collections;
using Nintenlord.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Geometry
{
    public static class SplitTree
    {
        public readonly struct Node<TVector>
            where TVector : IAdditionOperators<TVector, TVector, TVector>
        {
            public TVector Start => start;

            private readonly TVector start;
            readonly TVector[] edges;

            public IEnumerable<TVector> Ends => GetEnds();
            public IEnumerable<TVector> Edges => edges;

            public Node(TVector start, TVector[] edges)
            {
                this.start = start;
                this.edges = edges ?? throw new ArgumentNullException(nameof(edges));
            }

            private IEnumerable<TVector> GetEnds()
            {
                var start = this.start;
                return edges.Select(edge => start + edge);
            }
        }

        public static ITree<Node<TVector>> GetTree<TVector>(TVector start, TVector[] edges)
            where TVector : IAdditionOperators<TVector, TVector, TVector>,
            IDivisionOperators<TVector, double, TVector>
        {
            var edgeCount = edges.Length;

            IEnumerable<Node<TVector>> GetChildren(Node<TVector> node)
            {
                var nextEdges = node.Edges.Select(edge => edge / 2).ToArray();
                return BitArrayHelpers.GetAllArrays(edgeCount).Select(bits => {
                    var start = bits.EnumerateBits()
                    .Zip(nextEdges, (include, end) => (include, end))
                    .Where(t => t.include)
                    .Aggregate(node.Start, (accum, t) =>  accum + t.end);

                    return new Node<TVector>(start, nextEdges);
                });
            }

            return new LambdaTree<Node<TVector>>(new Node<TVector>(start, edges), GetChildren);
        }
    }
}
