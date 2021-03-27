using System;
using System.Collections.Generic;

namespace Nintenlord.IO.Scanners
{
    public sealed class EnumerableScanner<T> : IScanner<T>
    {
        private readonly IEnumerator<T> enumerator;
        private int offset;

        public EnumerableScanner(IEnumerable<T> toEnum)
        {
            enumerator = toEnum.GetEnumerator();
            IsAtEnd = !enumerator.MoveNext();
            offset = 0;
        }

        #region IScanner<T> Members

        public bool IsAtEnd
        {
            get;
            private set;
        }

        public long Offset
        {
            get => offset;
            set => throw new NotSupportedException();
        }

        public bool MoveNext()
        {
            offset++;
            return (IsAtEnd = !enumerator.MoveNext());
        }

        public T Current => enumerator.Current;

        public bool CanSeek => false;

        #endregion
    }
}
