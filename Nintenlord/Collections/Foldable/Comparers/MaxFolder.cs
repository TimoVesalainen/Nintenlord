using Nintenlord.Collections;
using Nintenlord.Collections.Comparers;
using Nintenlord.Collections.Foldable;
using Nintenlord.Utility;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Foldable.Comparers
{
    public sealed class MaxFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        static readonly ConcurrentDictionary<IComparer<T>, MaxFolder<T>> cache = new();

        public static MaxFolder<T> Create(IComparer<T> comparer)
        {
            return cache.GetOrAdd(comparer, c => new MaxFolder<T>(c));
        }

        public static readonly MaxFolder<T> Default = Create(Comparer<T>.Default);

        readonly IComparer<T> comparer;

        private MaxFolder(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            return state.Select(previous => comparer.Max(previous, input)).GetValueOrDefault(input);
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state;
        }
    }
}
