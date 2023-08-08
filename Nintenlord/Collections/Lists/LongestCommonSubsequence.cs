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

            var items = new List<T>(Math.Min(first.Count, second.Count));
            items.AddRange(Backtrack(matrix, first, second, comparer));
            items.Reverse();
            return items;
        }

        private static IMatrix<int> LCSMatrix<T>(IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer)
        {
            var firsts = 0.Repeat();

            int GetNewCost(int i, int left, int j, int top, int topLeft)
            {
                return comparer.Equals(first[i - 1], second[j - 1])
                        ? topLeft + 1
                        : Math.Max(top, left);
            }

            return MatrixHelpers.BuildFrom(firsts, firsts, first.Count + 1, second.Count + 1, GetNewCost);
        }

        private static IEnumerable<T> Backtrack<T>(IMatrix<int> matrix, IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer)
        {
            int length1 = first.Count;
            int length2 = second.Count;

            while (length1 != 0 && length2 != 0)
            {
                if (comparer.Equals(first[length1 - 1], second[length2 - 1]))
                {
                    yield return first[length1 - 1];
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
        }
    }
}
