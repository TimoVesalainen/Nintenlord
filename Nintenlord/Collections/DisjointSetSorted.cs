﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections
{
    public sealed class DisjointSetSorted<T>
    {
        readonly DisjointIntSet indexSet;
        readonly List<T> items;
        readonly IComparer<T> comparer;

        public DisjointSetSorted(IEnumerable<T> items, IComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.comparer = comparer ?? Comparer<T>.Default;
            this.items = items.OrderBy(x => x, this.comparer).ToList();
            this.indexSet = new DisjointIntSet(this.items.Count);
        }

        public T FindRepresentative(T item)
        {
            var index = items.BinarySearch(item, comparer);
            var parentIndex = indexSet.FindRepresentative(index);
            return items[parentIndex];
        }


        public void Union(T item1, T item2)
        {
            var index1 = items.BinarySearch(item1, comparer);
            var index2 = items.BinarySearch(item2, comparer);
            indexSet.Union(index1, index2);
        }

        public bool AreSameSet(T item1, T item2)
        {
            var index1 = items.BinarySearch(item1, comparer);
            var index2 = items.BinarySearch(item2, comparer);

            return indexSet.AreSameSet(index1, index2);
        }
    }
}
