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

        public static IEnumerable<TOut> Scan<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IEnumerable<TIn> enumerable)
        {
            return enumerable.Scan(folder.Start, folder.Fold).Select(folder.Transform);
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

        public static IFolder<TIn, (TState1, TState2), TOut> Combine<TIn, TState1, TState2, TOut1, TOut2, TOut>(
            this IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            Func<TOut1, TOut2, TOut> combiner)
        {
            return new Combiner<TIn, TState1, TState2, TOut1, TOut2, TOut>(folder1, folder2, combiner);
        }

        public static EitherFolder<TIn1, TIn2, TState1, TState2, TOut1, TOut2> Either<TIn1, TIn2, TState1, TState2, TOut1, TOut2>(
            this IFolder<TIn1, TState1, TOut1> folder1,
            IFolder<TIn2, TState2, TOut2> folder2)
        {
            return new EitherFolder<TIn1, TIn2, TState1, TState2, TOut1, TOut2>(folder1, folder2);
        }
    }
}
