using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public static class FolderHelpers
    {
        public static TOut Fold<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IEnumerable<TIn> enumerable)
        {
            return folder.Transform(enumerable.Aggregate(folder.Start, folder.Fold));
        }

        public static async Task<TOut> FoldAsync<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IAsyncEnumerable<TIn> enumerable)
        {
            TState state = folder.Start;

            await foreach (var item in enumerable)
            {
                state = folder.Fold(state, item);
            }

            return folder.Transform(state);
        }

        public static IObservable<TOut> Fold<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IObservable<TIn> observable)
        {
            return observable.Aggregate(folder.Start, folder.Fold).Select(folder.Transform);
        }

        public static IFolder<TIn, TState, TOut2> Select<TIn, TState, TOut1, TOut2>(this IFolder<TIn, TState, TOut1> folder, Func<TOut1, TOut2> selector)
        {
            return new SelectFolder<TIn, TState, TOut1, TOut2>(folder, selector);
        }

        public static IFolder<TIn, (TState1, TState2), TOut> Combine<TIn, TState1, TState2, TOut1, TOut2, TOut>(
            this IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            Func<TOut1, TOut2, TOut> combiner)
        {
            return new Combiner<TIn, TState1, TState2, TOut1, TOut2, TOut>(folder1, folder2, combiner);
        }
    }
}
