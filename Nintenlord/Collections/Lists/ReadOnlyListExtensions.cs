using Nintenlord.Collections.Comparers;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Lists
{
    public static class ReadOnlyListExtensions
    {
        public static IEnumerable<int> Indicis<T>(this IReadOnlyList<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return Enumerable.Range(0, list.Count);
        }

        public static int FirstIndex<T>(this IReadOnlyList<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return 0;
        }

        public static int LastIndex<T>(this IReadOnlyList<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return list.Count - 1;
        }

        public static int LastIndexOf<T>(this IReadOnlyList<T> list, Predicate<T> predicate)
        {
            int result = list.Count;
            while (result > 0)
            {
                result--;
                if (predicate(list[result]))
                {
                    break;
                }
            }
            return result;
        }

        public static int IndexOf<T>(this IReadOnlyList<T> list, Predicate<T> predicate)
        {
            int result = 0;
            while (result < list.Count)
            {
                if (predicate(list[result]))
                {
                    break;
                }
                result++;
            }
            return result != list.Count ? result : -1;
        }

        public static int GetEqualsInBeginning<T>(this IReadOnlyList<T> a, IReadOnlyList<T> b, IEqualityComparer<T> comp)
        {
            int max = Math.Min(a.Count, b.Count);
            int count;
            for (count = 0; count < max; count++)
            {
                if (!comp.Equals(a[count], b[count]))
                {
                    break;
                }
            }
            return count;
        }

        public static int GetEqualsInBeginning<T>(this IReadOnlyList<T> a, IReadOnlyList<T> b)
        {
            return a.GetEqualsInBeginning(b, EqualityComparer<T>.Default);
        }

        public static IEnumerable<T> GetItems<T>(this IReadOnlyList<T> list, IEnumerable<int> indicis)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (indicis is null)
            {
                throw new ArgumentNullException(nameof(indicis));
            }

            return indicis.Select(i => list[i]);
        }

        public static IEnumerable<T> GetBestIncreasingSubsequence<T>(this IReadOnlyList<T> items, Func<T, int> order, Func<T, int> reward)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (reward is null)
            {
                throw new ArgumentNullException(nameof(reward));
            }

            var comparer = (FunctionComparer<int>)(index => order(items[index]));

            (IEnumerable<int>, int reward) GetRest(int start)
            {
                var nexts = Enumerable.Range(start + 1, items.Count - start - 1)
                    .Where(comparer.GreaterOrEqualThan(start))
                    .MinScan(comparer);

                var (tail, rewardValue) = nexts.Select(GetRest)
                    .MaxBySafe(x => x.reward)
                    .GetValueOrDefault((Enumerable.Empty<int>(), 0));

                return (tail.Prepend(start), rewardValue + reward(items[start]));
            }

            var (indicis, _) = items.Indicis().MinScan(comparer).Select(GetRest).MaxBy(x => x.reward);

            return items.GetItems(indicis);
        }

        public static IEnumerable<T> GetLongestIncreasingSubsequence<T>(this IReadOnlyList<T> items, Func<T, int> order)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return items.GetBestIncreasingSubsequence(order, _ => 1);
        }

        public static IEnumerable<int> GetLongestIncreasingSubsequence(this IReadOnlyList<int> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetBestIncreasingSubsequence(n => n, _ => 1);
        }

        public static int IndexOfMax<T>(this IReadOnlyList<T> list, IComparer<T> comparer)
        {
            int maxIndex = -1;
            T currentMax = default;

            for (int i = 0; i < list.Count; i++)
            {
                if (maxIndex < 0 || comparer.Compare(list[i], currentMax) > 0)
                {
                    maxIndex = i;
                    currentMax = list[i];
                }
            }

            return maxIndex;
        }

        public static int IndexOfMin<T>(this IReadOnlyList<T> list, IComparer<T> comparer)
        {
            int minIndex = -1;
            T currentMin = default;

            for (int i = 0; i < list.Count; i++)
            {
                if (minIndex < 0 || comparer.Compare(list[i], currentMin) < 0)
                {
                    minIndex = i;
                    currentMin = list[i];
                }
            }

            return minIndex;
        }

        /// <param name="items">Sorted list of items</param>
        /// <returns>Index i such that items[i] <= item (< items[i+i] if exists)</returns>
        public static int FindSortedIndex<T>(this IReadOnlyList<T> items, T item, IComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= Comparer<T>.Default;
            return GetSortedIndexInner(items, item, comparer);
        }


        private static int GetSortedIndexInner<T>(IReadOnlyList<T> items, T item, IComparer<T> comparer)
        {
            int minIndex = 0;
            int maxIndex = items.Count;

            while (maxIndex - minIndex > 1)
            {
                var midIndex = (minIndex + maxIndex) / 2;

                var midItem = items[midIndex];
                if (comparer.Compare(midItem, item) > 0)
                {
                    maxIndex = midIndex;
                }
                else
                {
                    minIndex = midIndex;
                }
            }

            return minIndex;
        }

        public static bool IsSorted<T>(this IReadOnlyList<T> list, IComparer<T> comparer = null)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            comparer ??= Comparer<T>.Default;

            for (int i = 0; i < list.Count - 1; i++)
            {
                if (comparer.Compare(list[i], list[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
