using Nintenlord.Trees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.DisjointSet
{
    public sealed class DisjointSet<T> : IDisjointSet<T>, IParentForest<(T item, int index)>, IForest<(T item, int index)>, IForest<T>
    {
        readonly DisjointIntSet indexSet;
        readonly T[] items;

        public int ElementCount => items.Length;

        public IEnumerable<T> Items => items;

        public DisjointSet(IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items.ToArray();
            indexSet = new DisjointIntSet(this.items.Length);
        }

        public DisjointSet(T[] items, DisjointIntSet indexSet)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items));
            this.indexSet = indexSet ?? throw new ArgumentNullException(nameof(indexSet));
        }

        private int GetIndex(T item1)
        {
            return Array.IndexOf(items, item1);
        }

        public T FindRepresentative(T item)
        {
            var index = GetIndex(item);
            var parentIndex = indexSet.FindRepresentative(index);
            return items[parentIndex];
        }


        public bool Union(T item1, T item2)
        {
            var index1 = GetIndex(item1);
            var index2 = GetIndex(item2);
            return indexSet.Union(index1, index2);
        }

        public bool AreSameSet(T item1, T item2)
        {
            int index1 = GetIndex(item1);
            var index2 = GetIndex(item2);

            return indexSet.AreSameSet(index1, index2);
        }

        public bool TryGetParent((T item, int index) child, out (T item, int index) parent)
        {
            var (item, index) = child;

            if (indexSet.TryGetParent(index, out var parentIndex))
            {
                parent = (items[parentIndex], parentIndex);
                return true;
            }
            else
            {
                parent = child;
                return false;
            }
        }

        public IEnumerable<(T item, int index)> GetChildren((T item, int index) node)
        {
            return indexSet.GetChildren(node.index).Select(index => (items[index], index));
        }

        public IEnumerable<T> GetChildren(T node)
        {
            var index = GetIndex(node);
            return indexSet.GetChildren(index).Select(index => items[index]);
        }
    }
}
