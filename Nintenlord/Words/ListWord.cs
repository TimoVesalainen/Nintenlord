using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Words
{
    public sealed class ListWord<T> : IWord<T>
    {
        readonly IReadOnlyList<T> list;

        public ListWord(IReadOnlyList<T> list)
        {
            this.list = list;
        }

        public T this[BigInteger index] => list[(int)index];

        public BigInteger? Length => list.Count;
    }
}
