using System;

namespace Nintenlord.Matricis
{
    public sealed class ArrayMatrix<T> : IMatrix<T>
    {
        readonly T[,] items;

        public ArrayMatrix(int width, int height)
        {
            items = new T[height, width];
        }

        public T this[int x, int y]
        {
            get => items[y, x];
            set => items[y, x] = value;
        }

        public int Width => items.GetLength(1);

        public int Height => items.GetLength(0);

        public void CopyTo(T[,] array)
        {
            Array.Copy(items, array, array.Length);
        }
    }
}