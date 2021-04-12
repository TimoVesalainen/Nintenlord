using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections
{
    public static class ArrayExtensions
    {
        public static int IndexOf<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        public static int LastIndexOf<T>(this T[] array, Predicate<T> match)
        {
            return Array.LastIndexOf(array, match);
        }


        public static int IndexOf<T>(this T[] array, T[] toFind)
        {
            return array.IndexOf<T>(toFind, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this T[] array, T[] toFind, IEqualityComparer<T> eq)
        {
            for (int i = 0; i < array.Length - toFind.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < toFind.Length; j++)
                {
                    if (!eq.Equals(array[i + j], toFind[j]))
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }
            return -1;
        }


        public static bool ContainsAnyOf<T>(this T[] array, T[] toContain)
        {
            return array.ContainsAnyOf(toContain, EqualityComparer<T>.Default);
        }

        public static bool ContainsAnyOf<T>(this T[] array, T[] toContain, IEqualityComparer<T> eq)
        {
            return array.Any(t => toContain.Any(t1 => eq.Equals(t, t1)));
        }

        public static bool Equals<T>(this T[] array1, int index1, T[] array2, int index2, int length)
        {
            return array1.Equals(index1, array2, index2, length, EqualityComparer<T>.Default);
        }

        public static bool Equals<T>(this T[] array1, int index1, T[] array2, int index2, int length, IEqualityComparer<T> eq)
        {
            if (index1 < 0 ||
                index2 < 0 ||
                index1 + length > array1.Length ||
                index2 + length > array2.Length)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < length; i++)
            {
                if (!eq.Equals(array1[i + index1], array2[i + index2]))
                {
                    return false;
                }
            }
            return true;
        }


        public static void Move<T>(this T[] array, int from, int to, int length)
        {
            if (from + length > array.Length || to + length > array.Length || length < 0)
            {
                throw new IndexOutOfRangeException();
            }
            T[] temp = new T[length];
            Array.Copy(array, from, temp, 0, length);
            Array.Copy(temp, 0, array, to, length);
        }

        public static T[] GetArray<T>(this T item)
        {
            return new[] { item };
        }

        public static T[] GetArray<T>(params T[] items)
        {
            return items;
        }

        public static T[] GetRange<T>(this T[] array, int index)
        {
            return array.GetRange(index, array.Length - index);
        }

        public static T[] GetRange<T>(this T[] array, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }

        public static IEnumerable<T> EnumerateSublist<T>(this T[] array, int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                yield return array[i];
            }
        }

        public static int Total(this int[] array)
        {
            return array.Aggregate(1, (current, item) => current * item);
        }

        public static int AmountOfSame<T>(T[] array1, int index1, T[] array2, int index2)
        {
            int length = Math.Min(array1.Length - index1, array2.Length - index2);
            int i;
            for (i = 0; i < length; i++)
            {
                if (!array1[index1 + i].Equals(array2[index2 + i]))
                {
                    return i;
                }
            }
            return i;
        }

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
    }
}
