using System.Numerics;

namespace Nintenlord.Words
{
    public sealed class StringWord : IWord<char>
    {
        readonly string text;

        public StringWord(string text)
        {
            this.text = text;
        }

        public char this[BigInteger index] => text[(int)index];

        public BigInteger? Length => text.Length;
    }
}
