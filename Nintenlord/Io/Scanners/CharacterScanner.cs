using System;
using System.Collections.Generic;
using System.IO;

namespace Nintenlord.IO.Scanners
{
    public sealed class CharacterScanner : IScanner<char>
    {
        private readonly Stream stream;
        private char current;

        public CharacterScanner(Stream stream)
        {
            this.stream = stream;
            int val = stream.ReadByte();
            if (val != -1)
            {
                current = Convert.ToChar(val);
            }
        }

        #region IScanner<char> Members

        public bool IsAtEnd => stream.IsAtEnd();

        public long Offset
        {
            get => stream.Position;
            set => stream.Position = value;
        }

        public bool MoveNext()
        {
            int val = stream.ReadByte();
            if (val != -1)
            {
                current = Convert.ToChar(val);
            }
            return val == -1;
        }

        public char Current => current;

        public bool CanSeek => true;

        public IEnumerable<char> Substring(long Offset, int Length)
        {
            throw new NotSupportedException();
        }

        public bool CanTakeSubstring => false;

        #endregion
    }
}
