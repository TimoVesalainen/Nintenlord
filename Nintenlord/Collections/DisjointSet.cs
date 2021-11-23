using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections
{
    public sealed class DisjointSet<T>
    {
        readonly DisjointIntSet indexSet;
        readonly T[] items;

        public DisjointSet(IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items.ToArray();
            this.indexSet = new DisjointIntSet(this.items.Length);
        }

        public T FindRepresentative(T item)
        {
            var index = Array.IndexOf(items, item);
            var parentIndex = indexSet.FindRepresentative(index);
            return items[parentIndex];
        }


        public void Union(T item1, T item2)
        {
            var index1 = Array.IndexOf(items, item1);
            var index2 = Array.IndexOf(items, item2);
            indexSet.Union(index1, index2);
        }

        public bool AreSameSet(T item1, T item2)
        {
            var index1 = Array.IndexOf(items, item1);
            var index2 = Array.IndexOf(items, item2);

            return indexSet.AreSameSet(index1, index2);
        }
    }
}
