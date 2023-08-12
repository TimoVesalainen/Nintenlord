using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public class ImmutableListFolder<T> : IFolder<T, ImmutableList<T>, ImmutableList<T>>
    {
        public readonly static ImmutableListFolder<T> Value = new();

        public ImmutableList<T> Start => ImmutableList<T>.Empty;

        public ImmutableList<T> Fold(ImmutableList<T> state, T input)
        {
            return state.Add(input);
        }

        public ImmutableList<T> Transform(ImmutableList<T> state)
        {
            return state;
        }
    }
}
