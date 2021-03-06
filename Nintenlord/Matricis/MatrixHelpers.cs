using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Matricis
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
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

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
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

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
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (matrix is BinaryMatrix binaryMatrix)
            {
                return binaryMatrix;
            }
            else
            {
                return BinaryMatrix.Create((x, y) => matrix[x,y], matrix.Width, matrix.Height);
            }
        }

        public static IEnumerable<TOut> LinearTransformation<TIn1, TIn2, TOut>(this IMatrix<TIn1> matrix, IEnumerable<TIn2> vector, Func<TIn1, TIn2, TOut, TOut> sum, TOut zero = default)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (vector is null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (sum is null)
            {
                throw new ArgumentNullException(nameof(sum));
            }

            return matrix.Rows().Select(x => x.Zip(vector, (a, b) => (a, b)).Aggregate(zero, (s,t) => sum(t.a, t.b, s)));
        }

        /// <summary>
        /// Calculates the determinant of the whole matrix using a specific row
        /// </summary>
        public static T GetDeterminantFromRow<T>(this IMatrix<T> matrix, Func<T, T, T> product, Func<T, T, T> sum, Func<T, T> negative, T zero, int row)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (sum is null)
            {
                throw new ArgumentNullException(nameof(sum));
            }

            if (negative is null)
            {
                throw new ArgumentNullException(nameof(negative));
            }

            if (matrix.Width != matrix.Height)
            {
                throw new ArgumentException("Determinant of non-square matrix is not defined", nameof(matrix));
            }

            T GetDeterminantInner(IMatrix<T> matrixInner)
            {
                if (matrixInner.Width == 1 && matrixInner.Height == 1)
                {
                    return matrixInner[0, 0];
                }

                var complements = new ComplementsMatrix<T>(matrixInner);

                var sign = (row & 1) == 0;

                var determinant = zero;
                for (int x = 0; x < matrixInner.Width; x++)
                {
                    var t = product(matrixInner[x, row], GetDeterminantInner(complements[x, row]));
                    determinant = sum(determinant, sign ? t : negative(t));

                    sign = !sign;
                }

                return determinant;
            }

            return GetDeterminantInner(matrix);
        }

        /// <summary>
        /// Calculates the determinant of the whole matrix using a specific row
        /// </summary>
        public static T GetDeterminantFromColumn<T>(this IMatrix<T> matrix, Func<T, T, T> product, Func<T, T, T> sum, Func<T, T> negative, T zero, int column)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (sum is null)
            {
                throw new ArgumentNullException(nameof(sum));
            }

            if (negative is null)
            {
                throw new ArgumentNullException(nameof(negative));
            }

            if (matrix.Width != matrix.Height)
            {
                throw new ArgumentException("Determinant of non-square matrix is not defined", nameof(matrix));
            }

            T GetDeterminantInner(IMatrix<T> matrixInner)
            {
                if (matrixInner.Width == 1 && matrixInner.Height == 1)
                {
                    return matrixInner[0, 0];
                }

                var complements = new ComplementsMatrix<T>(matrixInner);

                var sign = (column & 1) == 0;

                var determinant = zero;
                for (int y = 0; y < matrixInner.Height; y++)
                {
                    var t = product(matrixInner[column, y], GetDeterminantInner(complements[column, y]));
                    determinant = sum(determinant, sign ? t : negative(t));

                    sign = !sign;
                }

                return determinant;
            }

            return GetDeterminantInner(matrix);
        }
    }
}