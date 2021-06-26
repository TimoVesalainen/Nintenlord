using System;

namespace Nintenlord.Matricis
{
    public sealed class TransposeMatrix<T> : IMatrix<T>
    {
        readonly IMatrix<T> baseMatrix;

        public TransposeMatrix(IMatrix<T> baseMatrix)
        {
            this.baseMatrix = baseMatrix ?? throw new ArgumentNullException(nameof(baseMatrix));
        }

        public T this[int x, int y] => baseMatrix[y, x];

        public int Width => baseMatrix.Height;

        public int Height => baseMatrix.Width;

        public IMatrix<T> BaseMatrix => baseMatrix;
    }
}