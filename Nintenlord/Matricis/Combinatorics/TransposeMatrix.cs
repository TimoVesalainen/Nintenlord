using System;

namespace Nintenlord.Matricis.Combinatorics
{
    public sealed class TransposeMatrix<T> : IMatrix<T>
    {
        readonly IMatrix<T> baseMatrix;

        public static IMatrix<T> CreateTranspose(IMatrix<T> matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (matrix is TransposeMatrix<T> transpose)
            {
                return transpose.BaseMatrix;
            }
            else if (matrix is SymmetricMatrix<T>)
            {
                return matrix;
            }
            else if (matrix is DiagonalMatrix<T>)
            {
                return matrix;
            }
            else
            {
                return new TransposeMatrix<T>(matrix);
            }
        }

        private TransposeMatrix(IMatrix<T> baseMatrix)
        {
            this.baseMatrix = baseMatrix ?? throw new ArgumentNullException(nameof(baseMatrix));
        }

        public T this[int x, int y] => baseMatrix[y, x];

        public int Width => baseMatrix.Height;

        public int Height => baseMatrix.Width;

        public IMatrix<T> BaseMatrix => baseMatrix;
    }
}