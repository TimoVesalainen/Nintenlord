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

                var tmp = buffer1;
                buffer1 = buffer2;
                buffer2 = tmp;
            }

            return buffer1[second.Count];
        }
    }
}
