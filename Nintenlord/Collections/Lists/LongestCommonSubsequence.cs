using Nintenlord.Matricis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Lists
{
    public static class LongestCommonSubsequence
    {
        public static IEnumerable<T> GetLongestCommonSubsequence<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer = null)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            comparer ??= EqualityComparer<T>.Default;
            var matrix = LCSMatrix(first, second, comparer);
            return Backtrack(matrix, first, second, comparer);
        }

        private static IMatrix<int> LCSMatrix<T>(IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer)
        {
            var matrix = new ArrayMatrix<int>(first.Count + 1, second.Count + 1);

            for (int i = 0; i < first.Count; i++)
            {
                matrix[i, 0] = 0;
            }

            for (int j = 0; j < second.Count; j++)
            {
                matrix[0, j] = 0;
            }

            for (int i = 1; i <= first.Count; i++)
            {
                for (int j = 1; j <= second.Count; j++)
                {
                    matrix[i, j] = comparer.Equals(first[i - 1], second[j - 1])
                        ? matrix[i - 1, j - 1] + 1
                        : Math.Max(matrix[i, j - 1], matrix[i - 1, j]);
                }
            }

            return matrix;
        }

        private static IEnumerable<T> Backtrack<T>(IMatrix<int> matrix, IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer)
        {
            int length1 = first.Count;
            int length2 = second.Count;
            var items = new List<T>(Math.Min(first.Count, second.Count));

            while (length1 != 0 && length2 != 0)
            {
                if (comparer.Equals(first[length1 - 1], second[length2 - 1]))
                {
                    items.Add(first[length1 - 1]);
                    length1--;
                    length2--;
                }
                else if (matrix[length1, length2 - 1] > matrix[length1 - 1, length2])
                {
                    length2--;
                }
                else
                {
                    length1--;
                }
            }
            items.Reverse();
            return items;
        }
    }
}
