using Nintenlord.Matricis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var buffer1 = Enumerable.Range(0, second.Count).Append(0).ToArray();
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
        public static IMatrix<int> LevenshteinMatrix<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second,
            Func<T, int> delCost, Func<T, int> insCost, Func<T, T, int> replaceCost)
        {
            var matrix = new ArrayMatrix<int>(first.Count + 1, second.Count + 1);

            for (int i = 0; i < matrix.Width; i++)
            {
                matrix[i, 0] = i;
            }
            for (int j = 0; j < matrix.Height; j++)
            {
                matrix[0, j] = j;
            }
            for (int i = 1; i < matrix.Width; i++)
            {
                for (int j = 1; j < matrix.Height; j++)
                {
                    var deletionCost = matrix[i - 1, j] + delCost(first[i-1]);
                    var insertionCost = matrix[i, j - 1] + insCost(second[j-1]);
                    int substitutionCost = matrix[i - 1, j - 1] + replaceCost(first[i - 1], second[j - 1]);
                    matrix[i, j] = Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);
                }
            }

            return matrix;
        }
    }
}
