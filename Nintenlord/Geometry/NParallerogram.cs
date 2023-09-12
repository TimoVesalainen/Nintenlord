using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Nintenlord.Collections;

namespace Nintenlord.Geometry
{
    public readonly struct NParallerogram<TVector>
        where TVector : IAdditionOperators<TVector, TVector, TVector>
    {
        public TVector Start => start;

        private readonly TVector start;
        readonly TVector[] edges;

        public IEnumerable<TVector> Ends => GetEnds();
        public IEnumerable<TVector> Edges => edges;
        public TVector Opposite => GetOpposite();

        public NParallerogram(TVector start, TVector[] edges)
        {
            this.start = start;
            this.edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }

        private IEnumerable<TVector> GetEnds()
        {
            var start = this.start;
            return edges.Select(edge => start + edge);
        }
        private TVector GetOpposite()
        {
            return edges.Sum(this.start);
        }
    }
}
