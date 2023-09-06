using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class Combiner<TIn, TState0, TState1, TOut0, TOut1, TOut> : IFolder<TIn, (TState0, TState1), TOut>
    {
        readonly IFolder<TIn, TState0, TOut0> folder0;
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly Func<TOut0, TOut1, TOut> transform;

        public Combiner(IFolder<TIn, TState0, TOut0> folder0, IFolder<TIn, TState1, TOut1> folder1, Func<TOut0, TOut1, TOut> transform)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState0, TState1) Start => (folder0.Start, folder1.Start);

        public (TState0, TState1) Fold((TState0, TState1) state, TIn input)
        {
            var (state0, state1) = state;

            return (folder0.Fold(state0, input), folder1.Fold(state1, input));
        }

        public TOut Transform((TState0, TState1) state)
        {
            var (state0, state1) = state;

            return transform(folder0.Transform(state0), folder1.Transform(state1));
        }
    }
    public sealed class Combiner<TIn, TState0, TState1, TState2, TOut0, TOut1, TOut2, TOut> : IFolder<TIn, (TState0, TState1, TState2), TOut>
    {
        readonly IFolder<TIn, TState0, TOut0> folder0;
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly IFolder<TIn, TState2, TOut2> folder2;
        readonly Func<TOut0, TOut1, TOut2, TOut> transform;

        public Combiner(IFolder<TIn, TState0, TOut0> folder0, IFolder<TIn, TState1, TOut1> folder1, IFolder<TIn, TState2, TOut2> folder2, Func<TOut0, TOut1, TOut2, TOut> transform)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState0, TState1, TState2) Start => (folder0.Start, folder1.Start, folder2.Start);

        public (TState0, TState1, TState2) Fold((TState0, TState1, TState2) state, TIn input)
        {
            var (state0, state1, state2) = state;

            return (folder0.Fold(state0, input), folder1.Fold(state1, input), folder2.Fold(state2, input));
        }

        public TOut Transform((TState0, TState1, TState2) state)
        {
            var (state0, state1, state2) = state;

            return transform(folder0.Transform(state0), folder1.Transform(state1), folder2.Transform(state2));
        }
    }
    public sealed class Combiner<TIn, TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3, TOut> : IFolder<TIn, (TState0, TState1, TState2, TState3), TOut>
    {
        readonly IFolder<TIn, TState0, TOut0> folder0;
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly IFolder<TIn, TState2, TOut2> folder2;
        readonly IFolder<TIn, TState3, TOut3> folder3;
        readonly Func<TOut0, TOut1, TOut2, TOut3, TOut> transform;

        public Combiner(IFolder<TIn, TState0, TOut0> folder0, IFolder<TIn, TState1, TOut1> folder1, IFolder<TIn, TState2, TOut2> folder2, IFolder<TIn, TState3, TOut3> folder3, Func<TOut0, TOut1, TOut2, TOut3, TOut> transform)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState0, TState1, TState2, TState3) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start);

        public (TState0, TState1, TState2, TState3) Fold((TState0, TState1, TState2, TState3) state, TIn input)
        {
            var (state0, state1, state2, state3) = state;

            return (folder0.Fold(state0, input), folder1.Fold(state1, input), folder2.Fold(state2, input), folder3.Fold(state3, input));
        }

        public TOut Transform((TState0, TState1, TState2, TState3) state)
        {
            var (state0, state1, state2, state3) = state;

            return transform(folder0.Transform(state0), folder1.Transform(state1), folder2.Transform(state2), folder3.Transform(state3));
        }
    }
    public sealed class Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4, TOut> : IFolder<TIn, (TState0, TState1, TState2, TState3, TState4), TOut>
    {
        readonly IFolder<TIn, TState0, TOut0> folder0;
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly IFolder<TIn, TState2, TOut2> folder2;
        readonly IFolder<TIn, TState3, TOut3> folder3;
        readonly IFolder<TIn, TState4, TOut4> folder4;
        readonly Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut> transform;

        public Combiner(IFolder<TIn, TState0, TOut0> folder0, IFolder<TIn, TState1, TOut1> folder1, IFolder<TIn, TState2, TOut2> folder2, IFolder<TIn, TState3, TOut3> folder3, IFolder<TIn, TState4, TOut4> folder4, Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut> transform)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.folder4 = folder4 ?? throw new ArgumentNullException(nameof(folder4));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState0, TState1, TState2, TState3, TState4) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start, folder4.Start);

        public (TState0, TState1, TState2, TState3, TState4) Fold((TState0, TState1, TState2, TState3, TState4) state, TIn input)
        {
            var (state0, state1, state2, state3, state4) = state;

            return (folder0.Fold(state0, input), folder1.Fold(state1, input), folder2.Fold(state2, input), folder3.Fold(state3, input), folder4.Fold(state4, input));
        }

        public TOut Transform((TState0, TState1, TState2, TState3, TState4) state)
        {
            var (state0, state1, state2, state3, state4) = state;

            return transform(folder0.Transform(state0), folder1.Transform(state1), folder2.Transform(state2), folder3.Transform(state3), folder4.Transform(state4));
        }
    }
    public sealed class Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut> : IFolder<TIn, (TState0, TState1, TState2, TState3, TState4, TState5), TOut>
    {
        readonly IFolder<TIn, TState0, TOut0> folder0;
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly IFolder<TIn, TState2, TOut2> folder2;
        readonly IFolder<TIn, TState3, TOut3> folder3;
        readonly IFolder<TIn, TState4, TOut4> folder4;
        readonly IFolder<TIn, TState5, TOut5> folder5;
        readonly Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut> transform;

        public Combiner(IFolder<TIn, TState0, TOut0> folder0, IFolder<TIn, TState1, TOut1> folder1, IFolder<TIn, TState2, TOut2> folder2, IFolder<TIn, TState3, TOut3> folder3, IFolder<TIn, TState4, TOut4> folder4, IFolder<TIn, TState5, TOut5> folder5, Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut> transform)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.folder4 = folder4 ?? throw new ArgumentNullException(nameof(folder4));
            this.folder5 = folder5 ?? throw new ArgumentNullException(nameof(folder5));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start, folder4.Start, folder5.Start);

        public (TState0, TState1, TState2, TState3, TState4, TState5) Fold((TState0, TState1, TState2, TState3, TState4, TState5) state, TIn input)
        {
            var (state0, state1, state2, state3, state4, state5) = state;

            return (folder0.Fold(state0, input), folder1.Fold(state1, input), folder2.Fold(state2, input), folder3.Fold(state3, input), folder4.Fold(state4, input), folder5.Fold(state5, input));
        }

        public TOut Transform((TState0, TState1, TState2, TState3, TState4, TState5) state)
        {
            var (state0, state1, state2, state3, state4, state5) = state;

            return transform(folder0.Transform(state0), folder1.Transform(state1), folder2.Transform(state2), folder3.Transform(state3), folder4.Transform(state4), folder5.Transform(state5));
        }
    }
    public sealed class Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut> : IFolder<TIn, (TState0, TState1, TState2, TState3, TState4, TState5, TState6), TOut>
    {
        readonly IFolder<TIn, TState0, TOut0> folder0;
        readonly IFolder<TIn, TState1, TOut1> folder1;
        readonly IFolder<TIn, TState2, TOut2> folder2;
        readonly IFolder<TIn, TState3, TOut3> folder3;
        readonly IFolder<TIn, TState4, TOut4> folder4;
        readonly IFolder<TIn, TState5, TOut5> folder5;
        readonly IFolder<TIn, TState6, TOut6> folder6;
        readonly Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut> transform;

        public Combiner(IFolder<TIn, TState0, TOut0> folder0, IFolder<TIn, TState1, TOut1> folder1, IFolder<TIn, TState2, TOut2> folder2, IFolder<TIn, TState3, TOut3> folder3, IFolder<TIn, TState4, TOut4> folder4, IFolder<TIn, TState5, TOut5> folder5, IFolder<TIn, TState6, TOut6> folder6, Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut> transform)
        {
            this.folder0 = folder0 ?? throw new ArgumentNullException(nameof(folder0));
            this.folder1 = folder1 ?? throw new ArgumentNullException(nameof(folder1));
            this.folder2 = folder2 ?? throw new ArgumentNullException(nameof(folder2));
            this.folder3 = folder3 ?? throw new ArgumentNullException(nameof(folder3));
            this.folder4 = folder4 ?? throw new ArgumentNullException(nameof(folder4));
            this.folder5 = folder5 ?? throw new ArgumentNullException(nameof(folder5));
            this.folder6 = folder6 ?? throw new ArgumentNullException(nameof(folder6));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) Start => (folder0.Start, folder1.Start, folder2.Start, folder3.Start, folder4.Start, folder5.Start, folder6.Start);

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) Fold((TState0, TState1, TState2, TState3, TState4, TState5, TState6) state, TIn input)
        {
            var (state0, state1, state2, state3, state4, state5, state6) = state;

            return (folder0.Fold(state0, input), folder1.Fold(state1, input), folder2.Fold(state2, input), folder3.Fold(state3, input), folder4.Fold(state4, input), folder5.Fold(state5, input), folder6.Fold(state6, input));
        }

        public TOut Transform((TState0, TState1, TState2, TState3, TState4, TState5, TState6) state)
        {
            var (state0, state1, state2, state3, state4, state5, state6) = state;

            return transform(folder0.Transform(state0), folder1.Transform(state1), folder2.Transform(state2), folder3.Transform(state3), folder4.Transform(state4), folder5.Transform(state5), folder6.Transform(state6));
        }
    }
}
