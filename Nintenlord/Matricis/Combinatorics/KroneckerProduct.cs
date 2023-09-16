using System;

namespace Nintenlord.Matricis.Combinatorics
{
    public sealed class KroneckerProduct<TIn1, TIn2, TOut> : IMatrix<TOut>
    {
        readonly IMatrix<TIn1> vector1;
        readonly IMatrix<TIn2> vector2;
        readonly Func<TIn1, TIn2, TOut> product;

        public KroneckerProduct(IMatrix<TIn1> vector1, IMatrix<TIn2> vector2, Func<TIn1, TIn2, TOut> product)
        {
            this.vector1 = vector1;
            this.vector2 = vector2;
            this.product = product;
        }

        public TOut this[int x, int y]
        {
            get
            {
                var x1 = Math.DivRem(x, vector1.Width, out var x2);
                var y1 = Math.DivRem(y, vector1.Height, out var y2);

                return product(vector1[x1, y1], vector2[x2, y2]);
            }
        }

        public int Width => vector1.Width * vector2.Width;

        public int Height => vector1.Height * vector2.Height;
    }
}
