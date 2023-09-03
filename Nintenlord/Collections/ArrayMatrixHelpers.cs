using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections
{
    public static partial class ArrayMatrixHelpers
    {
        public static IEnumerable<T[,]> EmbedTo<T>(this T[,] array, int newRows, int newColums, T toUse = default)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (newRows < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newRows));
            }
            if (newColums < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newColums));
            }

            int originalWidth = array.GetLength(1);
            int originalHeight = array.GetLength(0);

            int width = originalWidth + newRows;
            int height = originalHeight + newColums;
            for (int j = 0; j <= newColums; j++)
            {
                for (int i = 0; i <= newRows; i++)
                {
                    var result = new T[height, width];

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            result[y, x] =
                                x >= i && x < i + originalWidth &&
                                y >= j && y < j + originalHeight
                                ? array[y - j, x - i]
                                : toUse;
                        }
                    }

                    yield return result;
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> GetRows<T>(this T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                IEnumerable<T> GetRow()
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        yield return matrix[i, j];
                    }
                }

                yield return GetRow();
            }
        }

        public static IEnumerable<IEnumerable<T>> GetColumns<T>(this T[,] matrix)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                IEnumerable<T> GetColumn()
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        yield return matrix[i, j];
                    }
                }

                yield return GetColumn();
            }
        }

        public static string PrintMatrix<T>(this T[,] matrix)
        {
            return "{" + string.Join(", ", matrix.GetRows().Select(row => "{" + string.Join(", ", row) + "}")) + "}";
        }

        public static int[,] GetIncidencyMatrix<T>(this Func<T, IEnumerable<T>> morphism, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            if (morphism is null)
            {
                throw new ArgumentNullException(nameof(morphism));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            comparer ??= EqualityComparer<T>.Default;

            (T, int)[] valuesArray = values.Select((x, i) => (x, i)).ToArray();

            int[,] matrix = new int[valuesArray.Length, valuesArray.Length];

            foreach (var (itemY, column) in valuesArray)
            {
                foreach (var (itemX, row) in valuesArray)
                {
                    matrix[column, row] = morphism(itemY).Count(item => comparer.Equals(item, itemX));
                }
            }

            return matrix;
        }

        public static T[,] FromRows<T>(this IEnumerable<IEnumerable<T>> rows, int width, int height)
        {
            T[,] result = new T[height, width];

            int y = 0;
            foreach (var row in rows)
            {
                int x = 0;
                foreach (var item in row)
                {
                    result[y, x] = item;
                    x++;
                }
                y++;
            }

            return result;
        }

        public static T[,] FromRows<T>(params T[][] rows)
        {
            var height = rows.Length;
            var width = rows[0].Length;

            return FromRows(rows, width, height);
        }

        public static T[,] FromColumns<T>(this IEnumerable<IEnumerable<T>> columns, int width, int height)
        {
            T[,] result = new T[height, width];

            int x = 0;
            foreach (var column in columns)
            {
                int y = 0;
                foreach (var item in column)
                {
                    result[y, x] = item;
                    y++;
                }
                x++;
            }

            return result;
        }

        public static T[,] FromColumns<T>(params T[][] columns)
        {
            var height = columns[0].Length;
            var width = columns.Length;

            return FromColumns(columns, width, height);
        }

        public static TOut[,] MatrixMultiply<TOut, TIn1, TIn2>(this TIn1[,] matrix1, TIn2[,] matrix2,
            Func<TIn1, TIn2, TOut> multiply, Func<IEnumerable<TOut>, TOut> sum)
        {
            if (matrix1 is null)
            {
                throw new ArgumentNullException(nameof(matrix1));
            }

            if (matrix2 is null)
            {
                throw new ArgumentNullException(nameof(matrix2));
            }

            if (multiply is null)
            {
                throw new ArgumentNullException(nameof(multiply));
            }

            if (sum is null)
            {
                throw new ArgumentNullException(nameof(sum));
            }

            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                throw new ArgumentException($"Width of {nameof(matrix1)} is different from height of {nameof(matrix2)}");
            }

            TOut[,] result = new TOut[matrix1.GetLength(0), matrix2.GetLength(1)];
            foreach (var ((row, y), (column, x)) in GetRows(matrix1).Select((x, i) => (x, i)).Zip(GetColumns(matrix2).Select((x, i) => (x, i)),
                (x, y) => (x, y)))
            {
                result[y, x] = sum(row.Zip(column, multiply));
            }
            return result;
        }

        public static TResult[,] MatrixMultiply<T1, T2, TResult>(this T1[,] matrix1, T2[,] matrix2)
            where T1 : IMultiplyOperators<T1, T2, TResult>
            where TResult : IAdditionOperators<TResult, TResult, TResult>, IAdditiveIdentity<TResult, TResult>
        {
            if (matrix1 is null)
            {
                throw new ArgumentNullException(nameof(matrix1));
            }

            if (matrix2 is null)
            {
                throw new ArgumentNullException(nameof(matrix2));
            }

            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                throw new ArgumentException($"Width of {nameof(matrix1)} is different from height of {nameof(matrix2)}");
            }

            TResult[,] result = new TResult[matrix1.GetLength(0), matrix2.GetLength(1)];
            foreach (var ((row, y), (column, x)) in GetRows(matrix1).Select((x, i) => (x, i)).Zip(GetColumns(matrix2).Select((x, i) => (x, i)),
                (x, y) => (x, y)))
            {
                result[y, x] = row.Zip(column, (a, b) => a * b).Sum();
            }
            return result;
        }

        public static T[,] MatrixMultiply<T>(this T[,] matrix1, T[,] matrix2)
            where T : INumberBase<T>
        {
            return MatrixMultiply<T, T, T>(matrix1, matrix2);
        }

    }
}
