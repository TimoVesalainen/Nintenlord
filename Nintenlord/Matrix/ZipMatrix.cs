using System;

namespace Nintenlord.Matrix
{
    public sealed class ZipMatrix<TIn1, TIn2, TOut> : IMatrix<TOut>
    {
        readonly IMatrix<TIn1> first;
        readonly IMatrix<TIn2> second;
        readonly Func<TIn1, TIn2, TOut> sum;

        public ZipMatrix(IMatrix<TIn1> first, IMatrix<TIn2> second, Func<TIn1, TIn2, TOut> sum)
        {
            if (first.Width != second.Width)
            {
                throw new ArgumentException("Invalid size matricis", nameof(second));
            }
            if (first.Height != second.Height)
            {
                throw new ArgumentException("Invalid size matricis", nameof(second));
            }
            this.first = first;
            this.second = second;
            this.sum = sum;
        }

        public TOut this[int x, int y] => sum(first[x, y], second[x, y]);

        public int Width => first.Width;

        public int Height => first.Height;
    }
}