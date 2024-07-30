using System;
using System.Collections.Generic;

namespace Nintenlord.IO.Scanners
{
    public sealed class SingletonScanner<T> : IScanner<T>
    {
        private readonly T item;
        private bool read;

        public SingletonScanner(T item)
        {
            this.item = item;
            read = false;
        }

        #region IScanner<T> Members

        public bool IsAtEnd => read;

        public long Offset
        {
            get => read ? 1 : 0;
            set
            {
                switch (value)
                {
                    case 0:
                        read = false;
                        break;
                    case 1:
                        read = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
        }

        public bool MoveNext()
        {
            if (read)
            {
                return false;
            }
            else
            {
                read = true;
                return true;
            }
        }

        public T Current => item;

        public bool CanSeek => true;

        public IEnumerable<T> Substring(long offset, int length)
        {
            if (offset != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            if (length < 0 || length > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length == 1)
            {
                yield return item;
            }
        }

        public bool CanTakeSubstring => true;

        #endregion
    }
}
