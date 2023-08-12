using System.Collections.Concurrent;
using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class CountLongFolder<T> : IFolder<T, long, long>
    {
        public readonly static CountLongFolder<T> Instance = new();

        private CountLongFolder()
        {

        }

        public long Start => 0;

        public long Fold(long state, T input)
        {
            return state + 1;
        }

        public long Transform(long state)
        {
            return state;
        }
    }
}