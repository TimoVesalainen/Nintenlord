using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public sealed class FirstFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        public readonly static FirstFolder<T> Instance = new();

        private FirstFolder() { }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            if (state.HasValue)
            {
                return state;
            }
            else
            {
                return input;
            }
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state.Value;
        }
    }
}
