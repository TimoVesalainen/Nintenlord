using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Collections.Comparers;
using Nintenlord.Collections.DisjointSet;
using Nintenlord.Collections.EqualityComparer;

namespace Nintenlord.Collections
{
    public sealed class Partitions<T>
    {
        readonly List<T> items;
        readonly List<int> splitIndicis = new();

        public IEnumerable<T> Items => items;

        public int Count => items.Count;

        public int PartitionCount => splitIndicis.Count + 1;

        public Partitions(IEnumerable<T> items)
        {
            this.items = items?.ToList() ?? throw new ArgumentNullException(nameof(items));
        }

        /// <summary>
        /// Splits existing partitions based on comparison returning zero
        /// </summary>
        /// <returns>True if a split occured, false otherwise</returns>
        public bool Split(IComparer<T> comparer)
        {
            bool splitOccurred = false;

            for (var i = 0; i <= splitIndicis.Count; i++)
            {
                var start = GetPartitionStartIndex(i);
                var end = GetPartitionEnd(i);
                var splitCount = 0;
                items.Sort(start, end - start, comparer);

                for (var j = start; j < end - 1; j++)
                {
                    if (comparer.Compare(items[j], items[j + 1]) != 0)
                    {
                        splitIndicis.Insert(i + splitCount, j + 1);
                        splitCount++;
                    }
                }
                splitOccurred = splitOccurred || splitCount > 0;
                i += splitCount;
            }

            return splitOccurred;
        }

        /// <summary>
        /// Splits existing partitions to lower and upper halfs
        /// </summary>
        /// <returns>True if a split occured, false otherwise</returns>
        public bool SplitToHalf(IComparer<T> comparer)
        {
            bool splitOccurred = false;

            for (var i = 0; i <= splitIndicis.Count; i++)
            {
                var start = GetPartitionStartIndex(i);
                var end = GetPartitionEnd(i);
                int length = end - start;
                if (length < 2)
                {
                    continue;
                }
                splitOccurred = true;
                items.Sort(start, length, comparer);
                splitIndicis.Insert(i, start + length / 2);
                i += 1;
            }

            return splitOccurred;
        }

        public DisjointSet<T> GetDisjointSet()
        {
            var indexSet = DisjointIntSet.Create(GetPartitionIndexs());
            return new DisjointSet<T>(items.ToArray(), indexSet);
        }

        private IEnumerable<IEnumerable<int>> GetPartitionIndexs()
        {
            for (var i = 0; i <= splitIndicis.Count; i++)
            {
                var start = GetPartitionStartIndex(i);
                var end = GetPartitionEnd(i);
                yield return Enumerable.Range(start, end - start);
            }
        }

        public Partition GetPartition(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var index = items.FindIndex(comparer.GetPredicate(item));
            if (index < 0)
            {
                return new Partition(this, 0, 0);
            }
            var partitionIndex = GetPartitionIndex(index);
            return GetPartition(partitionIndex);
        }

        public IEnumerable<Partition> GetPartitions()
        {
            for (var i = 0; i <= splitIndicis.Count; i++)
            {
                yield return GetPartition(i);
            }
        }

        public readonly struct Partition
        {
            readonly Partitions<T> partition;
            readonly int start;
            readonly int length;

            public Partition(Partitions<T> partition, int start, int length)
            {
                this.partition = partition ?? throw new ArgumentNullException(nameof(partition));
                this.start = start;
                this.length = length;
            }

            public int Count => length;

            public IEnumerable<T> Items => GetItems();

            private IEnumerable<T> GetItems()
            {
                var partition = this.partition;
                return Enumerable.Range(start, length).Select(index => partition.items[index]);
            }
        }

        private int GetPartitionEnd(int i)
        {
            if (splitIndicis.Count == 0)
            {
                return items.Count;
            }

            return i == splitIndicis.Count ? items.Count : splitIndicis[i];
        }

        private int GetPartitionStartIndex(int i)
        {
            return i == 0 ? 0 : splitIndicis[i - 1];
        }

        private Partition GetPartition(int index)
        {
            var start = GetPartitionStartIndex(index);
            var end = GetPartitionEnd(index);
            var length = end - start;
            return new Partition(this, start, length);
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
    }
}
