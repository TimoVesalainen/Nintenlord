using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Foldable
{
    public static class FolderHelpers
    {
        public static TOut Fold<TIn, TState, TOut>(this IFolder<TIn, TState, TOut> folder, IEnumerable<TIn> enumerable)
        {
            return folder.Transform(enumerable.Aggregate(folder.Start, folder.Fold));
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
