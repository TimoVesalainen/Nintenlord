using System;
using System.Collections.Concurrent;

namespace Nintenlord.Collections.Foldable
{
    public sealed class CountIntFolder<T> : IFolder<T, int, int>
    {
        public readonly static CountIntFolder<T> Value = new();

        private CountIntFolder()
        {

        }

        public int Start => 0;

        public int Fold(int state, T input)
        {
            return state + 1;
        }

        public int Transform(int state)
        {
            return state;
        }
    }
}
