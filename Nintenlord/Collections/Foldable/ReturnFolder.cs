using System.Collections.Concurrent;
using System.Reactive;

namespace Nintenlord.Collections.Foldable
{
    public sealed class ReturnFolder<TIn, T> : IFolder<TIn, Unit, T>
    {
        private static readonly ConcurrentDictionary<T, ReturnFolder<TIn, T>> cache = new();

        public static ReturnFolder<TIn, T> Create(T result)
        {
            return cache.GetOrAdd(result, r => new ReturnFolder<TIn, T>(r));
        }

        readonly T result;

        private ReturnFolder(T result)
        {
            this.result = result;
        }

        public Unit Start => Unit.Default;

        public (Unit state, bool skipRest) FoldMayEnd(Unit state, TIn input)
        {
            return (state, true);
        }

        public T Transform(Unit state)
        {
            return result;
        }
    }
}
