using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nintenlord.Utility
{
    public sealed class SharedDirtyBuffer<T>
    {
        private readonly T[] array;
        public bool InUse
        {
            get;
            private set;
        }

        public SharedDirtyBuffer(int arrayLength)
        {
            this.array = new T[arrayLength];
        }

        public DirtyBufferUser GetUser()
        {
            return new DirtyBufferUser(this);
        }

        public struct DirtyBufferUser : IDisposable, IEnumerable<T>
        {
            private readonly SharedDirtyBuffer<T> listToUse;

            public T this[int index]
            {
                get => listToUse.array[index];
                set => listToUse.array[index] = value;
            }
            public int Length => listToUse.array.Length;

            public DirtyBufferUser(SharedDirtyBuffer<T> listToUse)
            {
                Contract.Requires<ArgumentException>(!listToUse.InUse, "Argument listToUse is already in use.");

                this.listToUse = listToUse;
                this.listToUse.InUse = true;
            }

            #region IDisposable Members

            public void Dispose()
            {
                listToUse.InUse = false;
            }

            #endregion

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = 0; i < listToUse.array.Length; i++)
                {
                    yield return listToUse.array[i];
                }
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }
    }
}
