using System;
using System.Collections.Generic;

namespace Nintenlord.Matrix
{
    public static class MatrixHelpers
    {
        public static IEnumerable<T> Column<T>(this IMatrix<T> matrix, int x)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            for (int y = 0; y < matrix.Height; y++)
            {
                yield return matrix[x, y];
            }
        }

        public static IEnumerable<T> Row<T>(this IMatrix<T> matrix, int y)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            for (int x = 0; x < matrix.Width; x++)
            {
                yield return matrix[x, y];
            }
        }

        public static IEnumerable<IEnumerable<T>> Columns<T>(this IMatrix<T> matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            for (int x = 0; x < matrix.Width; x++)
            {
                yield return matrix.Column(x);
            }
        }

        public static IEnumerable<IEnumerable<T>> Rows<T>(this IMatrix<T> matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            for (int y = 0; y < matrix.Height; y++)
            {
                yield return matrix.Row(y);
            }
        }

        public static ArrayMatrix<T> ToArrayMatrix<T>(this IMatrix<T> matrix)
        {
            if (matrix is ArrayMatrix<T> array)
            {
                return array;
            }
            else
            {
                return new ArrayMatrix<T>((x, y) => matrix[x, y], matrix.Width, matrix.Height);
            }
        }

        public static IMatrix<T> Transpose<T>(this IMatrix<T> matrix)
        {
            if (matrix is TransposeMatrix<T> transpose)
            {
                return transpose.BaseMatrix;
            }
            else
            {
                return new TransposeMatrix<T>(matrix);
            }
        }

        public static BinaryMatrix GetBinaryMatrix(this IMatrix<bool> matrix)
        {
            if (matrix is BinaryMatrix binaryMatrix)
            {
                return binaryMatrix;
            }
            else
            {
                return BinaryMatrix.Create((x, y) => matrix[x,y], matrix.Width, matrix.Height);
            }
        }
    }
}