using System;
using System.Collections.Generic;

namespace Nintenlord.IO.Scanners
{
    public sealed class StringScanner : IStoringScanner<char>
    {
        private readonly string s;
        private int i;
        private readonly int length;
        private readonly int startIndex;


        public StringScanner(string s) : this(s, 0, s.Length)
        {

        }
        public StringScanner(string s, int index) : this(s, index, s.Length - index)
        {

        }
        public StringScanner(string s, int index, int length)
        {
            this.s = s;
            this.i = index;
            this.length = length;
            this.startIndex = index;
        }

        #region IScanner<char> Members

        public bool IsAtEnd => i - startIndex >= length;

        public long Offset
        {
            get => i - startIndex;
            set => i = (int)(value - startIndex);
        }

        public bool MoveNext()
        {
            int oldIndex = i;
            int newIndex = oldIndex + 1;
            i = newIndex;
            return newIndex < startIndex + length;
        }

        public char Current => s[i];

        public bool CanSeek => true;

        public IEnumerable<char> Substring(long Offset, int Length)
        {
            return s.Substring((int)Offset, length);
        }

        public bool CanTakeSubstring => true;

        #endregion

        #region IStoringScanner<char> Members

        public char this[long offset]
        {
            get
            {
                if (offset < length && offset >= 0)
                {
                    return s[(int)offset + startIndex];
                }
                else
                {
                    throw new IndexOutOfRangeException("offset");
                }
            }
        }

        #endregion


        public bool IsStored(long offset)
        {
            return offset < length && offset >= 0;
        }

        public bool IsStored(long offset, long length)
        {
            return offset + length <= this.length && offset >= 0;
        }
    }
}
