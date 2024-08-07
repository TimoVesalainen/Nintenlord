﻿using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Lists
{
    public sealed class SubList<T> : IList<T>, IReadOnlyList<T>
    {
        public IList<T> MainList
        {
            get;
        }
        public int Index
        {
            get;
        }
        public int Length
        {
            get;
        }

        public SubList(IList<T> mainList, int index, int length)
        {
            this.MainList = mainList;
            this.Length = length;
            this.Index = index;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            for (int i = Index; i < Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(MainList[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int Index, T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int Index)
        {
            throw new NotSupportedException();
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return MainList[this.Index + index];
            }
            set => throw new NotSupportedException();
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = Index; i < Length; i++)
            {
                array[arrayIndex + i] = MainList[Index + i];
            }
        }

        public int Count => Length;

        public bool IsReadOnly => true;

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new SublistEnumerator<T>(MainList, Index, Length);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        public static SubList<T> SortedMerge(SubList<T> first, SubList<T> second, IComparer<T> comp)
        {
            if ((first.Index + first.Length != second.Index &&
                second.Index + second.Length != first.Index) ||
                first.MainList != second.MainList)
            {
                throw new InvalidOperationException();
            }

            SubList<T> result = new(first.MainList,
                Math.Min(first.Index, second.Index),
                first.Length + second.Length);

            var mainList = first.MainList;
            for (int i = Math.Max(first.Index, second.Index); i < result.Length; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (comp.Compare(mainList[j], mainList[i]) < 0)
                    {
                        if (i != j + 1)
                        {
                            T temp = mainList[i];
                            mainList.RemoveAt(i);
                            mainList.Insert(j + 1, temp);
                        }
                        break;
                    }
                }
            }

            return result;
        }
    }
}
