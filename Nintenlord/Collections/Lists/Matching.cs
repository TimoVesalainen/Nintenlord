using Nintenlord.Trees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Lists
{
    public static class Matching
    {
        private static int[] NWScore<T>(
            ListSequence<T> sequence1,
            ListSequence<T> sequence2,
            Func<T, int> delCost,
            Func<T, int> insCost,
            Func<T, T, int> replaceCost)
        {
            int[] buffer1 = new int[sequence2.Count + 1];
            int[] buffer2 = new int[sequence2.Count + 1];

            buffer1[0] = 0;

            for (int i = 1; i < buffer1.Length; i++)
            {
                buffer1[i] = buffer1[i - 1] + insCost(sequence2[i - 1]);
            }

            for (int i = 0; i < sequence1.Count; i++)
            {
                buffer2[0] = buffer1[0] + delCost(sequence1[i]);

                for (int j = 0; j < sequence2.Count; j++)
                {
                    var scoreSub = buffer1[j] + replaceCost(sequence1[i], sequence2[j]);
                    var scoreDel = buffer1[j + 1] + delCost(sequence1[i]);
                    var scoreIns = buffer2[j] + insCost(sequence2[j]);
                    buffer2[j + 1] = Math.Max(Math.Max(scoreSub, scoreDel), scoreIns);
                }
                Array.Copy(buffer2, buffer1, buffer1.Length);
            }

            return buffer2;
        }

        public readonly struct Match
        {
            public int Start1 { get; }
            public int Length1 { get; }
            public int Start2 { get; }
            public int Length2 { get; }

            public Match(int start1, int length1, int start2, int length2)
            {
                if (length1 < 0)
                {
                    throw new ArgumentException(nameof(length1), "Negative length");
                }
                if (length2 < 0)
                {
                    throw new ArgumentException(nameof(length2), "Negative length");
                }


                Start1 = start1;
                Length1 = length1;
                Start2 = start2;
                Length2 = length2;
            }

            public Match Reverse()
            {
                return new Match(
                    Start2, Length2,
                    Start1, Length1);
            }

            public (Match, Match) SplitAt(int position1, int position2)
            {
                if (position1 < 0)
                {
                    throw new ArgumentException(nameof(position1), "Negative length");
                }
                if (position2 < 0)
                {
                    throw new ArgumentException(nameof(position2), "Negative length");
                }
                if (position1 > Length1)
                {
                    throw new ArgumentException(nameof(position1), "Longer than parent length");
                }
                if (position2 > Length2)
                {
                    throw new ArgumentException(nameof(position2), "Longer than parent length");
                }

                return (
                    new Match(
                        Start1, position1,
                        Start2, position2),
                    new Match(
                        Start1 + position1, Length1 - position1,
                        Start2 + position2, Length2 - position2));
            }

            public override string ToString()
            {
                return $"{{{nameof(Match)}: {nameof(Start1)}={Start1}, {nameof(Length1)}={Length1}, {nameof(Start2)}={Start2}, {nameof(Length2)}={Length2}}}";
            }
        }

        /// <summary>
        /// Hirschberg algorith matching tree
        /// </summary>
        public static ITree<Match> AlingSequences<T>(IReadOnlyList<T> sequence1, IReadOnlyList<T> sequence2,
            Func<T, int> delCost,
            Func<T, int> insCost,
            Func<T, T, int> replaceCost)
        {
            IEnumerable<Match> LastSplit(T only, int index, IReadOnlyList<T> sequence, int start, int length,
                Func<T, int> delCost,
                Func<T, int> insCost,
                Func<T, T, int> replaceCost)
            {
                var delCosts = sequence.GetItems(Enumerable.Range(start, length)).Select(delCost).ToArray();
                var replaceCosts = sequence.GetItems(Enumerable.Range(start, length)).Select(t => replaceCost(t, only)).ToArray();
                var insCosts = insCost(only);
                var allDelete = delCosts.Sum();

                int minReplaceCost = int.MinValue;
                int minReplaceIndex = -1;
                for (int i = 0; i < replaceCosts.Length; i++)
                {
                    var replace = replaceCosts[i];
                    var deleteRest = allDelete - delCosts[i];

                    if (minReplaceCost < replace + deleteRest)
                    {
                        minReplaceCost = replace + deleteRest;
                        minReplaceIndex = i;
                    }
                }

                if (minReplaceCost < allDelete + insCosts)
                {
                    yield return new Match(index, 0, start, length);
                    yield return new Match(index, 1, start, 0);
                }
                else
                {
                    if (minReplaceIndex > 0)
                    {
                        yield return new Match(index, 0, start, minReplaceIndex);
                    }
                    yield return new Match(index, 1, start + minReplaceIndex, 1);
                    if (minReplaceIndex < length - 1)
                    {
                        yield return new Match(index + 1, 0, start + minReplaceIndex + 1, length - minReplaceIndex - 1);
                    }
                }
            }

            IEnumerable<Match> GetChildren(Match parent)
            {
                if (parent.Length1 == 0 || parent.Length2 == 0)
                {
                    yield break;
                }

                if (parent.Length1 == 1 && parent.Length2 == 1)
                {
                    yield break;
                }
                else if (parent.Length1 == 1)
                {
                    foreach (var item in LastSplit(sequence1[parent.Start1], parent.Start1,
                        sequence2, parent.Start2, parent.Length2,
                        delCost, insCost, (x, y) => replaceCost(y, x)))
                    {
                        yield return item;
                    }
                }
                else if (parent.Length2 == 1)
                {
                    foreach (var item in LastSplit(sequence2[parent.Start2], parent.Start2,
                        sequence1, parent.Start1, parent.Length1,
                        insCost, delCost, replaceCost))
                    {
                        yield return item.Reverse();
                    }
                }
                else
                {
                    var mid1 = parent.Length1 / 2;

                    var scoreLeft = NWScore(
                        new ListSequence<T>(sequence1, parent.Start1, mid1),
                        new ListSequence<T>(sequence2, parent.Start2, parent.Length2),
                        delCost, insCost, replaceCost);
                    var scoreRight = NWScore(
                        new ListSequence<T>(sequence1, parent.Start1 + parent.Length1 - 1, parent.Length1 - mid1, -1),
                        new ListSequence<T>(sequence2, parent.Start2 + parent.Length2 - 1, parent.Length2, -1),
                        delCost, insCost, replaceCost);

                    int mid2 = -1;
                    int mad2Val = 0;

                    for (int i = 0; i < scoreLeft.Length; i++)
                    {
                        var currentVal = scoreLeft[i] + scoreRight[scoreRight.Length - i - 1];
                        if (mid2 < 0 || currentVal > mad2Val)
                        {
                            mid2 = i;
                            mad2Val = currentVal;
                        }
                    }

                    var (start, end) = parent.SplitAt(mid1, mid2);
                    yield return start;
                    yield return end;
                }
            }

            return TreeHelpers.Create(new Match(0, sequence1.Count, 0, sequence2.Count), GetChildren);
        }

        public static List<(T, T)> ToMatchingSequence<T>(this ITree<Match> tree, IReadOnlyList<T> sequence1, IReadOnlyList<T> sequence2, T blank)
        {
            var result = new List<(T, T)>(sequence1.Count + sequence2.Count);

            foreach (var match in tree.GetLeaves())
            {
                var maxLength = Math.Max(match.Length1, match.Length2);

                for (int i = 0; i < maxLength; i++)
                {
                    T char1 = i < match.Length1 ? sequence1[match.Start1 + i] : blank;
                    T char2 = i < match.Length2 ? sequence2[match.Start2 + i] : blank;

                    result.Add((char1, char2));
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates optimal matching tree with Hirschberg algorithm
        /// </summary>
        public static List<(T, T)> MatchSequences<T>(this IReadOnlyList<T> sequence1, IReadOnlyList<T> sequence2, T blank,
            Func<T, int> delCost,
            Func<T, int> insCost,
            Func<T, T, int> replaceCost)
        {
            return AlingSequences(sequence1, sequence2, delCost, insCost, replaceCost)
                .ToMatchingSequence(sequence1, sequence2, blank);
        }
    }
}
