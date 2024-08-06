using System;
using System.Collections;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists
{
    public sealed class OriginArrayList<T> : IList<T>
    {
        T[] cache;
        int cacheStartIndex;

        int itemStartCacheIndex;
        int itemEndCacheIndex;

        readonly IEqualityComparer<T> itemEqualityComparer;

        public T this[int index]
        {
            get => cache[ToCacheIndex(index)];
            set
            {
                var cacheIndex = ToCacheIndex(index);

                itemEndCacheIndex = Math.Max(itemEndCacheIndex, cacheIndex + 1);
                itemStartCacheIndex = Math.Min(itemStartCacheIndex, cacheIndex);

                cache[cacheIndex] = value;
            }
        }

        public int Count => itemEndCacheIndex - itemStartCacheIndex;

        public int FirstIndex
        {
            get
            {
                AssertNotEmpty();
                return ToIndex(itemStartCacheIndex);
            }
        }
        public int LastIndex
        {
            get
            {
                AssertNotEmpty();
                return ToIndex(itemEndCacheIndex - 1);
            }
        }

        public T FirstItem
        {
            get
            {
                AssertNotEmpty();
                return cache[itemStartCacheIndex];
            }
        }

        public T LastItem
        {
            get
            {
                AssertNotEmpty();
                return cache[itemEndCacheIndex - 1];
            }
        }

        public bool IsReadOnly => false;

        public bool IsEmpty => itemEndCacheIndex == itemStartCacheIndex;

        public OriginArrayList()
            : this(4)
        {

        }

        public OriginArrayList(int capacity, IEqualityComparer<T> comparer = null)
        {
            itemEqualityComparer = comparer ?? EqualityComparer<T>.Default;
            var (newCache, newCacheStartIndex) = GetCacheAndIndex(capacity);
            cache = newCache;
            cacheStartIndex = newCacheStartIndex;
            itemStartCacheIndex = ToCacheIndex(0);
            itemEndCacheIndex = ToCacheIndex(0);
        }

        public void AddFirst(T item)
        {
            if (itemStartCacheIndex == 0)
            {
                Enlarge();
            }

            if (itemStartCacheIndex == itemEndCacheIndex)
            {
                cache[itemEndCacheIndex] = item;
                itemEndCacheIndex++;
            }
            else
            {
                itemStartCacheIndex--;
                cache[itemStartCacheIndex] = item;
            }
        }

        public void AddLast(T item)
        {
            if (itemEndCacheIndex == cache.Length - 1)
            {
                Enlarge();
            }

            cache[itemEndCacheIndex] = item;
            itemEndCacheIndex++;
        }

        public void RemoveFirst()
        {
            if (itemStartCacheIndex >= itemEndCacheIndex)
            {
                throw new InvalidOperationException("Trying to remove from empty collection");
            }
            cache[itemStartCacheIndex] = default;
            itemStartCacheIndex++;
        }

        public void RemoveLast()
        {
            if (itemStartCacheIndex >= itemEndCacheIndex)
            {
                throw new InvalidOperationException("Trying to remove from empty collection");
            }
            itemEndCacheIndex--;
            cache[itemEndCacheIndex] = default;
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Clear()
        {
            itemStartCacheIndex = ToCacheIndex(0);
            itemEndCacheIndex = ToCacheIndex(0);
            Array.Clear(cache, 0, cache.Length);
        }

        public bool Contains(T item)
        {
            for (int cacheIndex = itemStartCacheIndex; cacheIndex < itemEndCacheIndex; cacheIndex++)
            {
                if (itemEqualityComparer.Equals(cache[cacheIndex], item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int cacheIndex = itemStartCacheIndex; cacheIndex < itemEndCacheIndex; cacheIndex++)
            {
                array[arrayIndex + ToIndex(cacheIndex)] = cache[cacheIndex];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int cacheIndex = itemStartCacheIndex; cacheIndex < itemEndCacheIndex; cacheIndex++)
            {
                yield return cache[cacheIndex];
            }
        }

        public int IndexOf(T item)
        {
            for (int cacheIndex = itemStartCacheIndex; cacheIndex < itemEndCacheIndex; cacheIndex++)
            {
                if (itemEqualityComparer.Equals(cache[cacheIndex], item))
                {
                    return ToIndex(cacheIndex);
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index");
            }
            var cacheIndex = ToCacheIndex(index);

            if (cacheIndex == itemStartCacheIndex)
            {
                AddFirst(item);
            }
            else if (cacheIndex == itemEndCacheIndex - 1)
            {
                AddLast(item);
            }
            else
            {
                //TODO: This is suspect, test
                if (index < 0)
                {
                    Array.Copy(cache, itemStartCacheIndex, cache, itemStartCacheIndex - 1, cacheIndex - itemStartCacheIndex);
                    itemStartCacheIndex--;
                }
                else
                {
                    Array.Copy(cache, cacheIndex, cache, cacheIndex + 1, itemEndCacheIndex - cacheIndex);
                    itemEndCacheIndex++;
                }
                cache[cacheIndex] = item;
            }
        }

        public bool Remove(T item)
        {
            var toRemoveIndex = IndexOf(item);

            if (toRemoveIndex == -1)
            {
                return false;
            }
            else
            {
                RemoveAtInner(toRemoveIndex);
                return true;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index");
            }

            RemoveAtInner(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool IsContainedIndex(int index)
        {
            var cacheIndex = ToCacheIndex(index);
            return cacheIndex >= itemStartCacheIndex && cacheIndex < itemEndCacheIndex;
        }

        private void RemoveAtInner(int index)
        {
            var cacheIndex = ToCacheIndex(index);

            if (cacheIndex == itemStartCacheIndex)
            {
                cache[cacheIndex] = default;
                itemStartCacheIndex++;
            }
            else if (cacheIndex == itemEndCacheIndex)
            {
                cache[cacheIndex] = default;
                itemEndCacheIndex--;
            }
            else
            {
                //TODO: This is suspect, test
                if (index < 0)
                {
                    Array.Copy(cache, itemStartCacheIndex, cache, itemStartCacheIndex + 1, cacheIndex - itemStartCacheIndex);
                    cache[itemStartCacheIndex] = default;
                    itemStartCacheIndex++;
                }
                else
                {
                    Array.Copy(cache, cacheIndex, cache, cacheIndex - 1, itemEndCacheIndex - cacheIndex);
                    itemEndCacheIndex--;
                    cache[itemEndCacheIndex] = default;
                }
            }
        }

        private void Enlarge()
        {
            var (newCache, newCacheStartIndex) = GetCacheAndIndex(cache.Length);

            int oldCacheStartCacheIndexInNew = cache.Length / 2;
            Array.Copy(cache, 0, newCache, oldCacheStartCacheIndexInNew, cache.Length);

            var newItemStartCacheIndex = itemStartCacheIndex + oldCacheStartCacheIndexInNew;
            var newItemEndCacheIndex = itemEndCacheIndex + oldCacheStartCacheIndexInNew;

            cache = newCache;
            cacheStartIndex = newCacheStartIndex;
            itemStartCacheIndex = newItemStartCacheIndex;
            itemEndCacheIndex = newItemEndCacheIndex;
        }

        private (T[] newCache, int newCacheStartIndex) GetCacheAndIndex(int distance)
        {
            if (distance <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), "Non-positive distance");
            }

            var newCache = new T[distance * 2 - 1];
            var newCacheStartIndex = -distance + 1;
            return (newCache, newCacheStartIndex);
        }

        private int ToCacheIndex(int index)
        {
            return index - cacheStartIndex;
        }

        private int ToIndex(int cacheIndex)
        {
            return cacheIndex + cacheStartIndex;
        }

        private void AssertNotEmpty()
        {
            if (itemEndCacheIndex <= itemStartCacheIndex)
            {
                throw new InvalidOperationException("Collection is empty");
            }
        }
    }
}
