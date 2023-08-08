using Nintenlord.Matricis;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Lists
{
    public static class Metrics
    {
        /// <summary>
        /// Calculates the Levenshtein edit distance of two lists.
        /// </summary>
        /// <remarks>Uses Wagner–Fischer algorithm</remarks>
        /// <returns>Levenshtein edit distance</returns>
        public static int Levenshtein<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second,
            Func<T, int> delCost, Func<T, int> insCost, Func<T, T, int> replaceCost)
        {
            var buffer1 = first.Scan(0, (sum, item) => sum + delCost(item)).ToArray();
            var buffer2 = new int[buffer1.Length];

            for (int i = 0; i < first.Count; i++)
            {
                buffer2[0] = i + 1;

                for (int j = 0; j < buffer2.Length; j++)
                {
                    var deletionCost = buffer1[j + 1] + delCost(first[i]);
                    var insertionCost = buffer2[j] + insCost(second[j]);

                    int substitutionCost = buffer1[j] + replaceCost(first[i], second[j]);
                    buffer2[j + 1] = Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);
                }

                (buffer2, buffer1) = (buffer1, buffer2);
            }

            return buffer1[second.Count];
        }

        /// <summary>
        /// Calculates the Levenshtein matrix of two lists.
        /// </summary>
        /// <remarks>Uses Wagner–Fischer algorithm</remarks>
        /// <returns>Levenshtein matrix</returns>
        public static IMatrix<int> LevenshteinMatrix<T1, T2>(this IReadOnlyList<T1> first, IReadOnlyList<T2> second,
            Func<T1, int> delCost, Func<T2, int> insCost, Func<T1, T2, int> replaceCost)
        {
            var firstRow = first.Scan(0, (cost, character) => cost + delCost(character));
            var firstColumn = second.Scan(0, (cost, character) => cost + insCost(character));

            int GetNewCost(int i, int left, int j, int top, int topLeft)
            {
                var deletionCost = left + delCost(first[i - 1]);
                var insertionCost = top + insCost(second[j - 1]);
                int substitutionCost = topLeft + replaceCost(first[i - 1], second[j - 1]);
                return Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);
            }

            return MatrixHelpers.BuildFrom(firstRow, firstColumn, first.Count + 1, second.Count + 1, GetNewCost);
        }

        public static IMatrix<int> NeedlemanWunschMatrix<T1, T2>(this IReadOnlyList<T1> first, IReadOnlyList<T2> second, Func<T1, T2, int> similarity, int gapPenalty = -1)
        {
            var firstRow = Enumerable.Range(0, first.Count + 1).Select(x => x * gapPenalty);
            var firstColumn = Enumerable.Range(0, second.Count + 1).Select(x => x * gapPenalty);

            int GetNewCost(int i, int left, int j, int top, int topLeft)
            {
                var deletionCost = left + gapPenalty;
                var insertionCost = top + gapPenalty;
                int substitutionCost = topLeft + similarity(first[i - 1], second[j - 1]);
                return Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);
            }

            return MatrixHelpers.BuildFrom(firstRow, firstColumn, first.Count + 1, second.Count + 1, GetNewCost);
        }

        public static IEnumerable<(Maybe<T1>, Maybe<T2>)> NeedlemanWunschMatching<T1, T2>(this IReadOnlyList<T1> first, IReadOnlyList<T2> second, Func<T1, T2, int> similarity, int gapPenalty = -1)
        {
            var matrix = NeedlemanWunschMatrix(first, second, similarity, gapPenalty);

            var list = new List<(Maybe<T1>, Maybe<T2>)>(first.Count + second.Count);
            list.AddRange(BackTrack(matrix, first, second, similarity, gapPenalty));
            list.Reverse();
            return list;
        }

        public static IEnumerable<(Maybe<T1>, Maybe<T2>)> BackTrack<T1, T2>(IMatrix<int> matrix, IReadOnlyList<T1> first, IReadOnlyList<T2> second, Func<T1, T2, int> similarity, int gapPenalty)
        {
            int i = first.Count;
            int j = second.Count;

            while (i > 0 || j > 0)
            {
                if (i > 0 && j > 0 && matrix[i, j] == matrix[i - 1, j - 1] + similarity(first[i - 1], second[j - 1]))
                {
                    yield return (first[i - 1], second[j - 1]);
                    i--;
                    j--;
                }
                else if (i > 0 && matrix[i, j] == matrix[i - 1, j] + gapPenalty)
                {
                    yield return (first[i - 1], Maybe<T2>.Nothing);
                    i--;
                }
                else
                {
                    yield return (Maybe<T1>.Nothing, second[j - 1]);
                    j--;
                }
            }
        }
    }
}
