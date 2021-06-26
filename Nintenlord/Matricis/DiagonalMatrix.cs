using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Matricis
{
    public sealed class DiagonalMatrix<T> : IMatrix<T>
    {
        readonly T[] diagonal;
        readonly T other;

        public DiagonalMatrix(IEnumerable<T> diagonal, T other = default)
        {
            if (diagonal is null)
            {
                throw new ArgumentNullException(nameof(diagonal));
            }

            this.diagonal = diagonal.ToArray();
            this.other = other;
        }

        public T this[int x, int y] => x == y ? diagonal[x] : other;

        public int Width => diagonal.Length;

        public int Height => diagonal.Length;
    }
}