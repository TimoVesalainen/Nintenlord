using System;

namespace Nintenlord.Matricis
{
    public readonly struct ElementComplementMatrix<T> : IMatrix<T>
    {
        readonly IMatrix<T> parentMatrix;
        readonly int elementX;
        readonly int elementY;

        public ElementComplementMatrix(IMatrix<T> parentMatrix, int elementX, int elementY)
        {
            if (parentMatrix.Width <= elementX || elementX < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(elementX));
            }
            if (parentMatrix.Height <= elementY || elementX < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(elementY));
            }

            this.parentMatrix = parentMatrix;
            this.elementX = elementX;
            this.elementY = elementY;
        }

        public T this[int x, int y] => parentMatrix[GetItemX(x), GetItemY(y)];

        public int Width => parentMatrix.Width - 1;

        public int Height => parentMatrix.Height - 1;

        int GetItemX(int x)
        {
            if (x < elementX)
            {
                return x;
            }
            else
            {
                return x + 1;
            }
        }

        int GetItemY(int y)
        {
            if (y < elementY)
            {
                return y;
            }
            else
            {
                return y + 1;
            }
        }
    }
}
