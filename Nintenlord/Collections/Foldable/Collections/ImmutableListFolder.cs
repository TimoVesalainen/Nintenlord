using System.Collections.Immutable;

namespace Nintenlord.Collections.Foldable.Collections
{
    public sealed class ImmutableListFolder<T> : IFolder<T, ImmutableList<T>, ImmutableList<T>>
    {
        public readonly static ImmutableListFolder<T> Instance = new();

        private ImmutableListFolder() { }

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
