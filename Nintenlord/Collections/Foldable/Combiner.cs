using System;

namespace Nintenlord.Collections.Foldable
{
    // TODO: Templatize
    public sealed class Combiner<TIn, TState1, TState2, TOut1, TOut2, TOut> : IFolder<TIn, (TState1, TState2), TOut>
    {
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly IFolder<TIn, TState2, TOut2> folder2;
        readonly Func<TOut1, TOut2, TOut> transform;

        public Combiner(IFolder<TIn, TState1, TOut1> folder1, IFolder<TIn, TState2, TOut2> folder2, Func<TOut1, TOut2, TOut> transform)
        {
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState1, TState2) Start => (folder1.Start, folder2.Start);

        public (TState1, TState2) Fold((TState1, TState2) state, TIn input)
        {
            var (state1, state2) = state;

            return (folder1.Fold(state1, input), folder2.Fold(state2, input));
        }

        public TOut Transform((TState1, TState2) state)
        {
            var (state1, state2) = state;

            return transform(folder1.Transform(state1), folder2.Transform(state2));
        }
    }
}
