using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists
{
    public static class ListExtensions
    {
        public static void BubbleSort<T>(this IList<T> list, IComparer<T> comp = null)
        {
            comp ??= Comparer<T>.Default;
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

        public static void SelectionSort<T>(this IList<T> list, IComparer<T> comp = null)
        {
            comp ??= Comparer<T>.Default;
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

        public static void InsertionSort<T>(this IList<T> list, IComparer<T> comp = null)
        {
            comp ??= Comparer<T>.Default;
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

        public static void ShellSort<T>(this IList<T> list, IComparer<T> comp = null)
        {
            comp ??= Comparer<T>.Default;
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

        public static void CombSort<T>(this IList<T> list, IComparer<T> comp = null)
        {
            comp ??= Comparer<T>.Default;
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

        public static void MergeSort<T>(this IList<T> list, IComparer<T> comp = null)
        {
            comp ??= Comparer<T>.Default;
            if (list.Count > 1)
            {
                SubList<T> first = new(list, 0, list.Count / 2);
                SubList<T> second = new(list, first.Length, list.Count - first.Length);
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

        public static IReadOnlyList<T> GetReadOnlyAccess<T>(this IList<T> list)
        {
            if (list is IReadOnlyList<T> readonlyList)
            {
                return readonlyList;
            }
            else
            {
                return new ReadOnlyListAccess<T>(list);
            }
        }

        public static void SortedInsert<T>(this IList<T> items, T item, IComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= Comparer<T>.Default;
            var index = items.GetReadOnlyAccess().FindSortedIndex(item, comparer);

            if (index + 1 < items.Count)
            {
                items.Insert(index + 1, item);
            }
            else
            {
                items.Add(item);
            }
        }

        public static void SortedDelete<T>(this IList<T> items, T item,
            IComparer<T> comparer = null,
            IEqualityComparer<T> equalityComparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= Comparer<T>.Default;
            equalityComparer ??= EqualityComparer<T>.Default;

            var index = items.GetReadOnlyAccess().FindSortedIndex(item, comparer);
            int indexToDelete = index;


            while (indexToDelete >= 0 && !equalityComparer.Equals(items[indexToDelete], item))
            {
                indexToDelete--;
            }

            if (indexToDelete >= 0)
            {
                items.RemoveAt(indexToDelete);
            }
        }
    }
}
