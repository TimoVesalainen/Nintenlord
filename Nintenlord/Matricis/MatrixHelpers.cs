using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Matricis
{
    public static class MatrixHelpers
    {
        public static IEnumerable<T> Entries<T>(this IMatrix<T> matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            for (int y = 0; y < matrix.Height; y++)
            {
                for (int x = 0; x < matrix.Width; x++)
                {
                    yield return matrix[x, y];
                }
            }
        }

        public static bool TryGetValue<T>(this IMatrix<T> matrix, int x, int y, out T value)
        {
            if (x < 0 || y < 0 || x >= matrix.Width || y >= matrix.Height)
            {
                value = default;
                return false;
            }

            value = matrix[x, y];
            return true;
        }

        public static Maybe<T> TryGetValue<T>(this IMatrix<T> matrix, int x, int y)
        {
            if (matrix.TryGetValue(x, y, out var value))
            {
                return value;
            }
            return Maybe<T>.Nothing;
        }

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
                var newArray = new ArrayMatrix<T>(matrix.Width, matrix.Height);
                for (int y = 0; y < matrix.Height; y++)
                {
                    for (int x = 0; x < matrix.Width; x++)
                    {
                        newArray[x, y] = matrix[x, y];
                    }
                }
                return newArray;
            }
        }

        public static T[,] ToArray<T>(this IMatrix<T> matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var result = new T[matrix.Height, matrix.Width];

            if (matrix is ArrayMatrix<T> arrayMatrix)
            {
                arrayMatrix.CopyTo(result);
            }
            else
            {
                for (int y = 0; y < result.GetLength(0); y++)
                {
                    for (int x = 0; x < result.GetLength(1); x++)
                    {
                        result[x, y] = matrix[x, y];
                    }
                }
            }

            return result;
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

        public static bool TryGetSymmetric<T>(this IMatrix<T> matrix, out SymmetricMatrix<T> symmetricMatrix, IEqualityComparer<T> comparer = null)
        {
            if (matrix.Width != matrix.Height)
            {
                symmetricMatrix = null;
                return false;
            }

            symmetricMatrix = new SymmetricMatrix<T>(matrix.Width);
            comparer ??= EqualityComparer<T>.Default;

            for (int y = 0; y < matrix.Height; y++)
            {
                for (int x = y; x < matrix.Width; x++)
                {
                    var value = matrix[x, y];
                    if (x != y && !comparer.Equals(value, matrix[y, x]))
                    {
                        return false;
                    }

                    symmetricMatrix[x, y] = matrix[x, y];
                }
            }

            return true;
        }

        public static Maybe<SymmetricMatrix<T>> TryGetSymmetric<T>(this IMatrix<T> matrix, IEqualityComparer<T> comparer = null)
        {
            if (matrix.TryGetSymmetric(out var symmetrix, comparer))
            {
                return symmetrix;
            }
            else
            {
                return Maybe<SymmetricMatrix<T>>.Nothing;
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

        public static ArrayMatrix<T> BuildFrom<T>(IEnumerable<T> firstRow, IEnumerable<T> firstColumn, int width, int height, Func<int, T, int, T, T, T> getNew)
        {
            var matrix = new ArrayMatrix<T>(width, height);

            using (var rowEnumerable = firstRow.GetEnumerator())
            {
                for (int i = 0; i < matrix.Width; i++)
                {
                    if (!rowEnumerable.MoveNext())
                    {
                        throw new ArgumentException("Not enough items in enumerable", nameof(firstRow));
                    }
                    matrix[i, 0] = rowEnumerable.Current;
                }
            }

            using (var columnEnumerable = firstColumn.GetEnumerator())
            {
                for (int j = 0; j < matrix.Height; j++)
                {
                    if (!columnEnumerable.MoveNext())
                    {
                        throw new ArgumentException("Not enough items in enumerable", nameof(firstColumn));
                    }
                    matrix[0, j] = columnEnumerable.Current;
                }
            }

            for (int i = 1; i < matrix.Width; i++)
            {
                for (int j = 1; j < matrix.Height; j++)
                {
                    matrix[i, j] = getNew(i, matrix[i - 1, j], j, matrix[i, j - 1], matrix[i - 1, j - 1]);
                }
            }

            return matrix;
        }

        public static (int x, int y, T max) FindMax<T>(this IMatrix<T> matrix, IComparer<T> comparer = null)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (matrix.Width == 0 || matrix.Height == 0)
            {
                throw new ArgumentException("Empty matrix", nameof(matrix));
            }

            comparer ??= Comparer<T>.Default;

            var result = (0, 0, matrix[0, 0]);

            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Height; j++)
                {
                    if (comparer.Compare(matrix[i,j], result.Item3) > 0)
                    {
                        result = (i, j, matrix[i, j]);
                    }
                }
            }

            return result;
        }

        public static (int x, int y, T min) FindMin<T>(this IMatrix<T> matrix, IComparer<T> comparer = null)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (matrix.Width == 0 || matrix.Height == 0)
            {
                throw new ArgumentException("Empty matrix", nameof(matrix));
            }

            comparer ??= Comparer<T>.Default;

            var result = (0, 0, matrix[0, 0]);

            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Height; j++)
                {
                    if (comparer.Compare(matrix[i, j], result.Item3) < 0)
                    {
                        result = (i, j, matrix[i, j]);
                    }
                }
            }

            return result;
        }
    }
}