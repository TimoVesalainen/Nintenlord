using System.Collections.Immutable;

namespace Nintenlord.Collections.Foldable.Collections
{
    public sealed class ImmutableDictionaryFolder<TKey, TValue> : IFolder<(TKey key, TValue value), ImmutableDictionary<TKey, TValue>, ImmutableDictionary<TKey, TValue>>
    {
        public readonly static ImmutableDictionaryFolder<TKey, TValue> Instance = new();

        private ImmutableDictionaryFolder() { }

        public ImmutableDictionary<TKey, TValue> Start => ImmutableDictionary<TKey, TValue>.Empty;

        public ImmutableDictionary<TKey, TValue> Fold(ImmutableDictionary<TKey, TValue> state, (TKey key, TValue value) input)
        {
            return state.Add(input.key, input.value);
        }

        public ImmutableDictionary<TKey, TValue> Transform(ImmutableDictionary<TKey, TValue> state)
        {
            return state;
        }
    }
}
