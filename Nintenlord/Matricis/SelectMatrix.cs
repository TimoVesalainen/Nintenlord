using System;

namespace Nintenlord.Matricis
{
    public sealed class SelectMatrix<TIn, TOut> : IMatrix<TOut>
    {
        readonly IMatrix<TIn> original;
        readonly Func<TIn, TOut> select;


        public TOut this[int x, int y] => select(original[x, y]);

        public int Width => original.Width;

        public int Height => original.Height;
    }
}