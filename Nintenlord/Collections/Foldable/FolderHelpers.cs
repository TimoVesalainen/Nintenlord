using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public static partial class FolderHelpers
    {
        public static TOut Fold<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IEnumerable<TIn> enumerable)
        {
            var state = folder.Start;
            foreach (var item in enumerable)
            {
                var (newState, skipRest) = folder.FoldMayEnd(state, item);

                if (skipRest)
                {
                    return folder.Transform(newState);
                }
                state = newState;
            }
            return folder.Transform(state);
        }

        public static IEnumerable<TOut> Scan<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IEnumerable<TIn> enumerable)
        {
            return enumerable.Scan(folder.Start, folder.Fold).Select(folder.Transform);
        }

        public static async Task<TOut> FoldAsync<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IAsyncEnumerable<TIn> enumerable)
        {
            TState state = folder.Start;

            await foreach (var item in enumerable)
            {
                var (newState, skipRest) = folder.FoldMayEnd(state, item);

                if (skipRest)
                {
                    return folder.Transform(newState);
                }
                state = newState;
            }

            return folder.Transform(state);
        }

        public static async IAsyncEnumerable<TOut> ScanAsync<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IAsyncEnumerable<TIn> enumerable)
        {
            TState state = folder.Start;
            yield return folder.Transform(state);

            await foreach (var item in enumerable)
            {
                state = folder.Fold(state, item);
                yield return folder.Transform(state);
            }
        }

        public static IObservable<TOut> Fold<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IObservable<TIn> observable)
        {
            return observable.Aggregate(folder.Start, folder.Fold).Select(folder.Transform);
        }

        public static IFolder<TIn, TState, TOut2> Select<TIn, TState, TOut1, TOut2>(this IFolder<TIn, TState, TOut1> folder, Func<TOut1, TOut2> selector)
        {
            return new SelectFolder<TIn, TState, TOut1, TOut2>(folder, selector);
        }
    }
}
