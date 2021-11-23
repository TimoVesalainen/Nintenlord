using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Matricis
{
    public sealed class SymmetricMatrix<T> : IMatrix<T>
    {
        T[] values;
        int size;

        public SymmetricMatrix(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), $"Negative size {size}");
            }

            this.size = size;
            values = new T[TriangularNumber(size)];
        }

        public T this[int x, int y]
        {
            get
            {
                return Get(Math.Max(x, y), Math.Min(x, y));
            }
            set
            {
                Set(Math.Max(x, y), Math.Min(x, y), value);
            }
        }

        private void Set(int u, int v, T value)
        {
            values[GetIndex(u, v)] = value;
        }

        private T Get(int u, int v)
        {
            return values[GetIndex(u,v)];
        }

        int GetIndex(int u, int v)
        {
            int matrixIndex = v * size - u;

            return matrixIndex - TriangularNumber(v);
        }

        public int Width => size;

        public int Height => size;

        /// <summary>
        /// https://en.wikipedia.org/wiki/Triangular_number
        /// </summary>
        private static int TriangularNumber(int n)
        {
            return n * (n + 1) / 2;
        }

    }
}
