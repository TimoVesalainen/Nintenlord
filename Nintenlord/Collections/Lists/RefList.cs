using System;
using System.Collections;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists
{
    public sealed class RefList<T> : IList<T>, IReadOnlyList<T> where T : struct
    {
        T[] items;
        int count;

        public RefList() : this(4) 
        {

        }

        public RefList(int capacity)
        {
            this.items = new T[capacity];
            count = 0;
        }

        public T this[int index] { get => items[index]; set => items[index] = value; }

        public int Count => count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (count == items.Length)
            {
                Array.Resize(ref items, count * 2);
            }
            items[count] = item;
            count++;
        }

        public void Clear()
        {
            count = 0;
        }

        bool ICollection<T>.Contains(T item)
        {
            return Contains(item, null);
        }

        public bool Contains(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(item, items[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(items, array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        int IList<T>.IndexOf(T item)
        {
            return IndexOf(item, null);
        }

        public int IndexOf(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(item, items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            items[index] = item;
        }

        public bool Remove(T item)
        {
            return Remove(item, null);
        }

        public bool Remove(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(item, items[i]))
                {
                    return RemoveAtInner(i);
                }
            }
            return false;
        }

        private bool RemoveAtInner(int index)
        {
            var rest = count - 1 - index;
            if (rest > 0)
            {
                for (int j = 0; j < rest; j++)
                {
                    items[j] = items[j + 1];
                }
            }
            else
            {
                items[index] = default(T);
            }
            count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < count)
            {
                RemoveAtInner(index);
            }
        }


        public ref T GetReference(int index)
        {
            return ref items[index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
