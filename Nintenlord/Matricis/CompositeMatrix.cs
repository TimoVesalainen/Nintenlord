using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Matricis
{
    public sealed class CompositeMatrix<T> : IMatrix<T>
    {
        readonly IMatrix<T> topLeft;
        readonly IMatrix<T> bottomLeft;
        readonly IMatrix<T> topRight;
        readonly IMatrix<T> bottomRight;

        public CompositeMatrix(IMatrix<T> topLeft, IMatrix<T> bottomLeft, IMatrix<T> topRight, IMatrix<T> bottomRight)
        {
            if (topLeft.Height != topRight.Height)
            {
                throw new ArgumentException("Matrix sizes don't match");
            }
            if (bottomLeft.Height != bottomRight.Height)
            {
                throw new ArgumentException("Matrix sizes don't match");
            }

            if (topLeft.Width != bottomLeft.Width)
            {
                throw new ArgumentException("Matrix sizes don't match");
            }

            if (bottomRight.Width != topRight.Width)
            {
                throw new ArgumentException("Matrix sizes don't match");
            }

            this.topLeft = topLeft;
            this.bottomLeft = bottomLeft;
            this.topRight = topRight;
            this.bottomRight = bottomRight;
        }

        public T this[int x, int y] => Get(x, y);

        public int Width => topLeft.Width + topRight.Width;

        public int Height => topLeft.Height + bottomLeft.Height;

        private T Get(int x, int y)
        {
            var xIsLeft = x < topLeft.Width;
            var yIsTop = y < topLeft.Height;

            if (xIsLeft && yIsTop)
            {
                return topLeft[x, y];
            }
            else if (xIsLeft)
            {
                return bottomLeft[x, y - topLeft.Height];
            }
            else if (yIsTop)
            {
                return topRight[x - topLeft.Width, y];
            }
            else
            {
                return bottomRight[x - topLeft.Width, y - topLeft.Height];
            }
        }
    }
}
