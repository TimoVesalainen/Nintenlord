using System.Collections.Immutable;

namespace Nintenlord.Collections.Foldable.Collections
{
    public sealed class ImmutableHashSetFolder<T> : IFolder<T, ImmutableHashSet<T>, ImmutableHashSet<T>>
    {
        public readonly static ImmutableHashSetFolder<T> Instance = new();

        private ImmutableHashSetFolder() { }

        public ImmutableHashSet<T> Start => ImmutableHashSet<T>.Empty;

        public ImmutableHashSet<T> Fold(ImmutableHashSet<T> state, T input)
        {
            return state.Add(input);
        }

        public ImmutableHashSet<T> Transform(ImmutableHashSet<T> state)
        {
            return state;
        }
    }
}
