using System;

namespace Nintenlord.Matrix
{
    public sealed class ArrayMatrix<T> : IMatrix<T>
    {
        readonly T[,] items;

        public ArrayMatrix(Func<int, int, T> getter, int width, int height)
        {
            items = new T[height, width];

            for (int y = 0; y < items.GetLength(0); y++)
            {
                for (int x = 0; x < items.GetLength(1); x++)
                {
                    items[y, x] = getter(x, y);
                }
            }
        }

        public T this[int x, int y]
        {
            get => items[y, x];
            set => items[y, x] = value;
        }

        public int Width => items.GetLength(1);

        public int Height => items.GetLength(0);
    }
}