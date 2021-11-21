using Nintenlord.Collections.Comparers;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Lists
{
    public static class ListExtensions
    {
        public static void BubbleSort<T>(this IList<T> list) where T : IComparable<T>
        {
            list.BubbleSort<T>(Comparer<T>.Default);
        }

        public static void BubbleSort<T>(this IList<T> list, IComparer<T> comp)
        {
            bool isSorted = true;
            int length = list.Count;
            do
            {
                for (int i = 0; i < length - 1; i++)
                {
                    if (comp.Compare(list[i], list[i + 1]) > 0)
                    {
                        Swap(list, i, i + 1);
                        isSorted = false;
                    }
                }
            } while (!isSorted);
        }


        public static void SelectionSort<T>(this IList<T> list) where T : IComparable<T>
        {
            list.SelectionSort(Comparer<T>.Default);
        }

        public static void SelectionSort<T>(this IList<T> list, IComparer<T> comp)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (comp.Compare(list[minIndex], list[j]) > 0)
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    list.Swap(minIndex, i);
                }
            }
        }


        public static void InsertionSort<T>(this IList<T> list) where T : IComparable<T>
        {
            list.InsertionSort(Comparer<T>.Default);
        }

        public static void InsertionSort<T>(this IList<T> list, IComparer<T> comp)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (comp.Compare(list[j], list[i]) > 0)
                    {
                        T temp = list[i];
                        list.RemoveAt(i);
                        list.Insert(j, temp);
                        break;
                    }
                }
            }
        }


        public static void ShellSort<T>(this IList<T> list) where T : IComparable<T>
        {
            list.ShellSort(Comparer<T>.Default);
        }

        public static void ShellSort<T>(this IList<T> list, IComparer<T> comp)
        {
            int inc = list.Count / 2;
            while (inc > 0)
            {
                for (int i = inc; i < list.Count; i++)
                {
                    T temp = list[i];
                    int j = i;
                    while (j < inc && comp.Compare(list[j - inc], temp) > 0)
                    {
                        list[j] = list[j - inc];
                        j -= inc;
                    }
                    list[j] = temp;
                }
                inc = (int)(inc / 2.2);
            }
        }


        public static void CombSort<T>(this IList<T> list) where T : IComparable<T>
        {
            list.CombSort(Comparer<T>.Default);
        }

        public static void CombSort<T>(this IList<T> list, IComparer<T> comp)
        {
            int gap = list.Count;
            bool swapped = true;
            while (gap > 1 || swapped)
            {
                if (gap > 1)
                {
                    gap = (int)(gap / 1.247330950103979);
                }

                swapped = false;
                for (int i = 0; i < list.Count - gap; i++)
                {
                    if (comp.Compare(list[i], list[i + gap]) > 0)
                    {
                        list.Swap(i, i + gap);
                        swapped = true;
                    }
                }
            }
        }


        public static void MergeSort<T>(this IList<T> list) where T : IComparable<T>
        {
            list.MergeSort(Comparer<T>.Default);
        }

        public static void MergeSort<T>(this IList<T> list, IComparer<T> comp)
        {
            if (list.Count > 1)
            {
                SubList<T> first = new SubList<T>(list, 0, list.Count / 2);
                SubList<T> second = new SubList<T>(list, first.Length, list.Count - first.Length);
                first.MergeSort(comp);
                second.MergeSort(comp);
                SubList<T>.SortedMerge(first, second, comp);
            }
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
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

        public static Maybe<T> LastSafe<T>(this IReadOnlyList<T> list)
        {
            if (list.Count > 0)
            {
                return list[list.Count - 1];
            }
            else
            {
                return Maybe<T>.Nothing;
            }
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

        public static IEnumerable<int> Indicis<T>(this IReadOnlyList<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return Enumerable.Range(0, list.Count);
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
    }
}
