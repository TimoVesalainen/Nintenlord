using Nintenlord.Collections.Comparers;
using Nintenlord.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public sealed class MinFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        static readonly ConcurrentDictionary<IComparer<T>, MinFolder<T>> cache = new();

        public static MinFolder<T> Create(IComparer<T> comparer)
        {
            return cache.GetOrAdd(comparer, c => new MinFolder<T>(c));
        }

        public static readonly MinFolder<T> Default = Create(Comparer<T>.Default);

        readonly IComparer<T> comparer;

        private MinFolder(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            return state.Select(previous => comparer.Min(previous, input)).GetValueOrDefault(input);
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state;
        }
    }
}
