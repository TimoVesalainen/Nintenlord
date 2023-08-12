using Nintenlord.Utility;
using System;

namespace Nintenlord.Collections.Foldable
{
    // TODO: Templatize
    public sealed class EitherFolder<TIn1, TIn2, TState1, TState2, TOut1, TOut2> : IFolder<Either<TIn1, TIn2>, (TState1, TState2), (TOut1, TOut2)>
    {
        readonly IFolder<TIn1, TState1, TOut1> folder1;
        readonly IFolder<TIn2, TState2, TOut2> folder2;

        public EitherFolder(IFolder<TIn1, TState1, TOut1> folder1, IFolder<TIn2, TState2, TOut2> folder2)
        {
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
        }

        public (TState1, TState2) Start => (folder1.Start, folder2.Start);

        public (TState1, TState2) Fold((TState1, TState2) state, Either<TIn1, TIn2> input)
        {
            return input.Apply(input1 => (folder1.Fold(state.Item1, input1), state.Item2),
                input2 => (state.Item1, folder2.Fold(state.Item2, input2)));
        }

        public (TOut1, TOut2) Transform((TState1, TState2) state)
        {
            return (folder1.Transform(state.Item1), folder2.Transform(state.Item2));
        }
    }
}
