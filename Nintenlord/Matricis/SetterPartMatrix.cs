using System;

namespace Nintenlord.Matricis
{
    internal readonly struct SetterPartMatrix<T> : IMatrix<T>
    {
        readonly ArrayMatrix<T> parentMatrix;
        readonly int width;
        readonly int height;
        readonly int xStart;
        readonly int yStart;

        public SetterPartMatrix(ArrayMatrix<T> parentMatrix, int width, int height, int xStart, int yStart)
        {
            if (width + xStart > parentMatrix.Width)
            {
                throw new ArgumentException("X coord overflow", nameof(width));
            }
            if (height + yStart > parentMatrix.Height)
            {
                throw new ArgumentException("Y coord overflow", nameof(height));
            }
            if (xStart < 0)
            {
                throw new ArgumentException("X coord underflow", nameof(xStart));
            }
            if (yStart < 0)
            {
                throw new ArgumentException("Y coord underflow", nameof(yStart));
            }

            this.parentMatrix = parentMatrix ?? throw new ArgumentNullException(nameof(parentMatrix));
            this.width = width;
            this.height = height;
            this.xStart = xStart;
            this.yStart = yStart;
        }

        public T this[int x, int y]
        {
            get => parentMatrix[x + xStart, y + yStart];
            set => parentMatrix[x + xStart, y + yStart] = value;
        }

        public int Width => width;

        public int Height => height;
    }
}
