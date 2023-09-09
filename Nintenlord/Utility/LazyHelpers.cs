using System;

namespace Nintenlord.Utility
{
    public static class LazyHelpers
    {
        public static Lazy<TResult> SelectWhere<TSource, TLazy, TResult>(
            this Lazy<TSource> source,
            Func<TSource, Lazy<TLazy>> lazySelector,
            Func<TSource, TLazy, TResult> resultSelector)
        {
            return new Lazy<TResult>(() => resultSelector(source.Value, lazySelector(source.Value).Value));
        }

        public static Lazy<TResult> Select<TSource, TResult>(
            this Lazy<TSource> source,
            Func<TSource, TResult> selector)
        {
            return new Lazy<TResult>(() => selector(source.Value));
        }
    }
}
