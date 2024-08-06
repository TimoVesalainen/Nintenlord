using Nintenlord.Matricis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Matricis.Combinatorics
{
    public sealed class OuterProductMatrix<TIn1, TIn2, TOut> : IMatrix<TOut>
    {
        readonly TIn1[] vector1;
        readonly TIn2[] vector2;
        readonly Func<TIn1, TIn2, TOut> product;

        public OuterProductMatrix(IEnumerable<TIn1> vector1, IEnumerable<TIn2> vector2, Func<TIn1, TIn2, TOut> product)
        {
            this.vector1 = vector1.ToArray();
            this.vector2 = vector2.ToArray();
            this.product = product;
        }

        public TOut this[int x, int y] => product(vector1[x], vector2[y]);

        public int Width => vector1.Length;

        public int Height => vector2.Length;
    }
}
