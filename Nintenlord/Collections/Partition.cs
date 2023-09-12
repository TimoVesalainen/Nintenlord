﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Collections.Comparers;
using Nintenlord.Collections.EqualityComparer;

namespace Nintenlord.Collections
{
    public sealed class Partition<T>
    {
        readonly List<T> items;
        readonly List<int> splitIndicis = new();

        public IEnumerable<T> Items => items;

        public int Count => items.Count;

        public int PartitionCount => splitIndicis.Count + 1;

        public Partition(IEnumerable<T> items)
        {
            this.items = items?.ToList() ?? throw new ArgumentNullException(nameof(items));
        }

        int GetPartitionIndex(int index)
        {
            var partition = splitIndicis.BinarySearch(index);

            if (partition < 0)
            {
                return ~partition;
            }
            else
            {
                return partition;
            }
        }

        IEnumerable<T> GetPartition(int partitionIndex)
        {
            var start = GetPartitionStartIndex(partitionIndex);
            var end = GetPartitionEnd(partitionIndex);
            return Enumerable.Range(start, end - start).Select(i => items[i]);
        }

        public IEnumerable<T> GetPartition(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var index = items.FindIndex(other => comparer.Equals(other, item));
            if (index < 0)
            {
                return Enumerable.Empty<T>();
            }
            var partitionIndex = GetPartitionIndex(index);
            return GetPartition(partitionIndex);
        }

        /// <summary>
        /// Splits existing partitions based on comparison returning zero
        /// </summary>
        /// <returns>True if a split occured, false otherwise</returns>
        public bool Split(IComparer<T> comparer)
        {
            bool splitOccurred = false;

            int SortItems(int i, int start, int end)
            {
                var splitCount = 0;
                items.Sort(start, end - start, comparer);

                for (var j = start; j < end - 1; j++)
                {
                    if (comparer.Compare(items[j], items[j + 1]) != 0)
                    {
                        splitIndicis.Insert(i + splitCount, j);
                        splitCount++;
                    }
                }
                return splitCount;
            }

            for (var i = 0; i <= splitIndicis.Count; i++)
            {
                var start = GetPartitionStartIndex(i);
                var end = GetPartitionEnd(i);
                var splitCount = SortItems(i, start, end);
                splitOccurred = splitOccurred || splitCount > 0;
                i += splitCount;
            }

            return splitOccurred;
        }

        private int GetPartitionEnd(int i)
        {
            if (splitIndicis.Count == 0)
            {
                return items.Count;
            }

            return i == splitIndicis.Count ? splitIndicis[i - 1] + 1 : splitIndicis[i];
        }

        private int GetPartitionStartIndex(int i)
        {
            return i == 0 ? 0 : splitIndicis[i - 1];
        }

        public IEnumerable<IEnumerable<T>> GetPartitions()
        {
            var index = 0;
            foreach (var item in splitIndicis)
            {
                yield return Enumerable.Range(index, item - index).Select(i => items[i]);
                index = item;
            }
            yield return Enumerable.Range(index, items.Count - index).Select(i => items[i]);
        }
    }
}
