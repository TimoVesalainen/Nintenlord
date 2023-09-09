using Nintenlord.Utility;
using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class EitherFolder<TIn0, TIn1, TState0, TState1, TOut0, TOut1> : IFolder<Either<TIn0, TIn1>, (TState0, TState1), (TOut0, TOut1)>
    {
        readonly IFolder<TIn0, TState0, TOut0> folder0;
        readonly IFolder<TIn1, TState1, TOut1> folder1;

        public EitherFolder(IFolder<TIn0, TState0, TOut0> folder0, IFolder<TIn1, TState1, TOut1> folder1)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
        }

        public (TState0, TState1) Start => (folder0.Start, folder1.Start);

        public (TState0, TState1) Fold((TState0, TState1) state, Either<TIn0, TIn1> input)
        {
            return input.Apply(input0 => (folder0.Fold(state.Item1, input0), state.Item2), input1 => (state.Item1, folder1.Fold(state.Item2, input1)));
        }

        public (TOut0, TOut1) Transform((TState0, TState1) state)
        {
            return (folder0.Transform(state.Item1), folder1.Transform(state.Item2));
        }
    }
    public sealed class EitherFolder<TIn0, TIn1, TIn2, TState0, TState1, TState2, TOut0, TOut1, TOut2> : IFolder<Either<TIn0, TIn1, TIn2>, (TState0, TState1, TState2), (TOut0, TOut1, TOut2)>
    {
        readonly IFolder<TIn0, TState0, TOut0> folder0;
        readonly IFolder<TIn1, TState1, TOut1> folder1;
        readonly IFolder<TIn2, TState2, TOut2> folder2;

        public EitherFolder(IFolder<TIn0, TState0, TOut0> folder0, IFolder<TIn1, TState1, TOut1> folder1, IFolder<TIn2, TState2, TOut2> folder2)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
        }

        public (TState0, TState1, TState2) Start => (folder0.Start, folder1.Start, folder2.Start);

        public (TState0, TState1, TState2) Fold((TState0, TState1, TState2) state, Either<TIn0, TIn1, TIn2> input)
        {
            return input.Apply(input0 => (folder0.Fold(state.Item1, input0), state.Item2, state.Item3), input1 => (state.Item1, folder1.Fold(state.Item2, input1), state.Item3), input2 => (state.Item1, state.Item2, folder2.Fold(state.Item3, input2)));
        }

        public (TOut0, TOut1, TOut2) Transform((TState0, TState1, TState2) state)
        {
            return (folder0.Transform(state.Item1), folder1.Transform(state.Item2), folder2.Transform(state.Item3));
        }
    }
    public sealed class EitherFolder<TIn0, TIn1, TIn2, TIn3, TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3> : IFolder<Either<TIn0, TIn1, TIn2, TIn3>, (TState0, TState1, TState2, TState3), (TOut0, TOut1, TOut2, TOut3)>
    {
        readonly IFolder<TIn0, TState0, TOut0> folder0;
        readonly IFolder<TIn1, TState1, TOut1> folder1;
        readonly IFolder<TIn2, TState2, TOut2> folder2;
        readonly IFolder<TIn3, TState3, TOut3> folder3;

        public EitherFolder(IFolder<TIn0, TState0, TOut0> folder0, IFolder<TIn1, TState1, TOut1> folder1, IFolder<TIn2, TState2, TOut2> folder2, IFolder<TIn3, TState3, TOut3> folder3)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
        }

        public (TState0, TState1, TState2, TState3) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start);

        public (TState0, TState1, TState2, TState3) Fold((TState0, TState1, TState2, TState3) state, Either<TIn0, TIn1, TIn2, TIn3> input)
        {
            return input.Apply(input0 => (folder0.Fold(state.Item1, input0), state.Item2, state.Item3, state.Item4), input1 => (state.Item1, folder1.Fold(state.Item2, input1), state.Item3, state.Item4), input2 => (state.Item1, state.Item2, folder2.Fold(state.Item3, input2), state.Item4), input3 => (state.Item1, state.Item2, state.Item3, folder3.Fold(state.Item4, input3)));
        }

        public (TOut0, TOut1, TOut2, TOut3) Transform((TState0, TState1, TState2, TState3) state)
        {
            return (folder0.Transform(state.Item1), folder1.Transform(state.Item2), folder2.Transform(state.Item3), folder3.Transform(state.Item4));
        }
    }
    public sealed class EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4> : IFolder<Either<TIn0, TIn1, TIn2, TIn3, TIn4>, (TState0, TState1, TState2, TState3, TState4), (TOut0, TOut1, TOut2, TOut3, TOut4)>
    {
        readonly IFolder<TIn0, TState0, TOut0> folder0;
        readonly IFolder<TIn1, TState1, TOut1> folder1;
        readonly IFolder<TIn2, TState2, TOut2> folder2;
        readonly IFolder<TIn3, TState3, TOut3> folder3;
        readonly IFolder<TIn4, TState4, TOut4> folder4;

        public EitherFolder(IFolder<TIn0, TState0, TOut0> folder0, IFolder<TIn1, TState1, TOut1> folder1, IFolder<TIn2, TState2, TOut2> folder2, IFolder<TIn3, TState3, TOut3> folder3, IFolder<TIn4, TState4, TOut4> folder4)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.folder4 = folder4 ?? throw new ArgumentNullException(nameof(folder4));
        }

        public (TState0, TState1, TState2, TState3, TState4) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start, folder4.Start);

        public (TState0, TState1, TState2, TState3, TState4) Fold((TState0, TState1, TState2, TState3, TState4) state, Either<TIn0, TIn1, TIn2, TIn3, TIn4> input)
        {
            return input.Apply(input0 => (folder0.Fold(state.Item1, input0), state.Item2, state.Item3, state.Item4, state.Item5), input1 => (state.Item1, folder1.Fold(state.Item2, input1), state.Item3, state.Item4, state.Item5), input2 => (state.Item1, state.Item2, folder2.Fold(state.Item3, input2), state.Item4, state.Item5), input3 => (state.Item1, state.Item2, state.Item3, folder3.Fold(state.Item4, input3), state.Item5), input4 => (state.Item1, state.Item2, state.Item3, state.Item4, folder4.Fold(state.Item5, input4)));
        }

        public (TOut0, TOut1, TOut2, TOut3, TOut4) Transform((TState0, TState1, TState2, TState3, TState4) state)
        {
            return (folder0.Transform(state.Item1), folder1.Transform(state.Item2), folder2.Transform(state.Item3), folder3.Transform(state.Item4), folder4.Transform(state.Item5));
        }
    }
    public sealed class EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5> : IFolder<Either<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>, (TState0, TState1, TState2, TState3, TState4, TState5), (TOut0, TOut1, TOut2, TOut3, TOut4, TOut5)>
    {
        readonly IFolder<TIn0, TState0, TOut0> folder0;
        readonly IFolder<TIn1, TState1, TOut1> folder1;
        readonly IFolder<TIn2, TState2, TOut2> folder2;
        readonly IFolder<TIn3, TState3, TOut3> folder3;
        readonly IFolder<TIn4, TState4, TOut4> folder4;
        readonly IFolder<TIn5, TState5, TOut5> folder5;

        public EitherFolder(IFolder<TIn0, TState0, TOut0> folder0, IFolder<TIn1, TState1, TOut1> folder1, IFolder<TIn2, TState2, TOut2> folder2, IFolder<TIn3, TState3, TOut3> folder3, IFolder<TIn4, TState4, TOut4> folder4, IFolder<TIn5, TState5, TOut5> folder5)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.folder4 = folder4 ?? throw new ArgumentNullException(nameof(folder4));
            this.folder5 = folder5 ?? throw new ArgumentNullException(nameof(folder5));
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start, folder4.Start, folder5.Start);

        public (TState0, TState1, TState2, TState3, TState4, TState5) Fold((TState0, TState1, TState2, TState3, TState4, TState5) state, Either<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> input)
        {
            return input.Apply(input0 => (folder0.Fold(state.Item1, input0), state.Item2, state.Item3, state.Item4, state.Item5, state.Item6), input1 => (state.Item1, folder1.Fold(state.Item2, input1), state.Item3, state.Item4, state.Item5, state.Item6), input2 => (state.Item1, state.Item2, folder2.Fold(state.Item3, input2), state.Item4, state.Item5, state.Item6), input3 => (state.Item1, state.Item2, state.Item3, folder3.Fold(state.Item4, input3), state.Item5, state.Item6), input4 => (state.Item1, state.Item2, state.Item3, state.Item4, folder4.Fold(state.Item5, input4), state.Item6), input5 => (state.Item1, state.Item2, state.Item3, state.Item4, state.Item5, folder5.Fold(state.Item6, input5)));
        }

        public (TOut0, TOut1, TOut2, TOut3, TOut4, TOut5) Transform((TState0, TState1, TState2, TState3, TState4, TState5) state)
        {
            return (folder0.Transform(state.Item1), folder1.Transform(state.Item2), folder2.Transform(state.Item3), folder3.Transform(state.Item4), folder4.Transform(state.Item5), folder5.Transform(state.Item6));
        }
    }
    public sealed class EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6> : IFolder<Either<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>, (TState0, TState1, TState2, TState3, TState4, TState5, TState6), (TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6)>
    {
        readonly IFolder<TIn0, TState0, TOut0> folder0;
        readonly IFolder<TIn1, TState1, TOut1> folder1;
        readonly IFolder<TIn2, TState2, TOut2> folder2;
        readonly IFolder<TIn3, TState3, TOut3> folder3;
        readonly IFolder<TIn4, TState4, TOut4> folder4;
        readonly IFolder<TIn5, TState5, TOut5> folder5;
        readonly IFolder<TIn6, TState6, TOut6> folder6;

        public EitherFolder(IFolder<TIn0, TState0, TOut0> folder0, IFolder<TIn1, TState1, TOut1> folder1, IFolder<TIn2, TState2, TOut2> folder2, IFolder<TIn3, TState3, TOut3> folder3, IFolder<TIn4, TState4, TOut4> folder4, IFolder<TIn5, TState5, TOut5> folder5, IFolder<TIn6, TState6, TOut6> folder6)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.folder4 = folder4 ?? throw new ArgumentNullException(nameof(folder4));
            this.folder5 = folder5 ?? throw new ArgumentNullException(nameof(folder5));
            this.folder6 = folder6 ?? throw new ArgumentNullException(nameof(folder6));
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start, folder4.Start, folder5.Start, folder6.Start);

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) Fold((TState0, TState1, TState2, TState3, TState4, TState5, TState6) state, Either<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> input)
        {
            return input.Apply(input0 => (folder0.Fold(state.Item1, input0), state.Item2, state.Item3, state.Item4, state.Item5, state.Item6, state.Item7), input1 => (state.Item1, folder1.Fold(state.Item2, input1), state.Item3, state.Item4, state.Item5, state.Item6, state.Item7), input2 => (state.Item1, state.Item2, folder2.Fold(state.Item3, input2), state.Item4, state.Item5, state.Item6, state.Item7), input3 => (state.Item1, state.Item2, state.Item3, folder3.Fold(state.Item4, input3), state.Item5, state.Item6, state.Item7), input4 => (state.Item1, state.Item2, state.Item3, state.Item4, folder4.Fold(state.Item5, input4), state.Item6, state.Item7), input5 => (state.Item1, state.Item2, state.Item3, state.Item4, state.Item5, folder5.Fold(state.Item6, input5), state.Item7), input6 => (state.Item1, state.Item2, state.Item3, state.Item4, state.Item5, state.Item6, folder6.Fold(state.Item7, input6)));
        }

        public (TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6) Transform((TState0, TState1, TState2, TState3, TState4, TState5, TState6) state)
        {
            return (folder0.Transform(state.Item1), folder1.Transform(state.Item2), folder2.Transform(state.Item3), folder3.Transform(state.Item4), folder4.Transform(state.Item5), folder5.Transform(state.Item6), folder6.Transform(state.Item7));
        }
    }
}
