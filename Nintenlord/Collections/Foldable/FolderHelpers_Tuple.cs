using Nintenlord.Collections.Foldable.Combinatorics;
using System;

namespace Nintenlord.Collections.Foldable
{
    public static partial class FolderHelpers
    {
        public static Combiner<TIn, TState0, TState1, TOut0, TOut1, TOut> Combine<TIn, TState0, TState1, TOut0, TOut1, TOut>(this
            IFolder<TIn, TState0, TOut0> folder0,
            IFolder<TIn, TState1, TOut1> folder1,
            Func<TOut0, TOut1, TOut> combiner)
        {
            return new Combiner<TIn, TState0, TState1, TOut0, TOut1, TOut>(folder0, folder1, combiner);
        }

        public static EitherFolder<TIn0, TIn1,  TState0, TState1, TOut0, TOut1> Either<TIn0, TIn1, TState0, TState1, TOut0, TOut1>(
            this  IFolder<TIn0, TState0, TOut0> folder0,  IFolder<TIn1, TState1, TOut1> folder1)
        {
            return new EitherFolder<TIn0, TIn1,  TState0, TState1, TOut0, TOut1>(folder0, folder1);
        }

        public static Combiner<TIn, TState0, TState1, TState2, TOut0, TOut1, TOut2, TOut> Combine<TIn, TState0, TState1, TState2, TOut0, TOut1, TOut2, TOut>(this
            IFolder<TIn, TState0, TOut0> folder0,
            IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            Func<TOut0, TOut1, TOut2, TOut> combiner)
        {
            return new Combiner<TIn, TState0, TState1, TState2, TOut0, TOut1, TOut2, TOut>(folder0, folder1, folder2, combiner);
        }

        public static EitherFolder<TIn0, TIn1, TIn2,  TState0, TState1, TState2, TOut0, TOut1, TOut2> Either<TIn0, TIn1, TIn2, TState0, TState1, TState2, TOut0, TOut1, TOut2>(
            this  IFolder<TIn0, TState0, TOut0> folder0,  IFolder<TIn1, TState1, TOut1> folder1,  IFolder<TIn2, TState2, TOut2> folder2)
        {
            return new EitherFolder<TIn0, TIn1, TIn2,  TState0, TState1, TState2, TOut0, TOut1, TOut2>(folder0, folder1, folder2);
        }

        public static Combiner<TIn, TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3, TOut> Combine<TIn, TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3, TOut>(this
            IFolder<TIn, TState0, TOut0> folder0,
            IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            IFolder<TIn, TState3, TOut3> folder3,
            Func<TOut0, TOut1, TOut2, TOut3, TOut> combiner)
        {
            return new Combiner<TIn, TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3, TOut>(folder0, folder1, folder2, folder3, combiner);
        }

        public static EitherFolder<TIn0, TIn1, TIn2, TIn3,  TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3> Either<TIn0, TIn1, TIn2, TIn3, TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3>(
            this  IFolder<TIn0, TState0, TOut0> folder0,  IFolder<TIn1, TState1, TOut1> folder1,  IFolder<TIn2, TState2, TOut2> folder2,  IFolder<TIn3, TState3, TOut3> folder3)
        {
            return new EitherFolder<TIn0, TIn1, TIn2, TIn3,  TState0, TState1, TState2, TState3, TOut0, TOut1, TOut2, TOut3>(folder0, folder1, folder2, folder3);
        }

        public static Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4, TOut> Combine<TIn, TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4, TOut>(this
            IFolder<TIn, TState0, TOut0> folder0,
            IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            IFolder<TIn, TState3, TOut3> folder3,
            IFolder<TIn, TState4, TOut4> folder4,
            Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut> combiner)
        {
            return new Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4, TOut>(folder0, folder1, folder2, folder3, folder4, combiner);
        }

        public static EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4,  TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4> Either<TIn0, TIn1, TIn2, TIn3, TIn4, TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4>(
            this  IFolder<TIn0, TState0, TOut0> folder0,  IFolder<TIn1, TState1, TOut1> folder1,  IFolder<TIn2, TState2, TOut2> folder2,  IFolder<TIn3, TState3, TOut3> folder3,  IFolder<TIn4, TState4, TOut4> folder4)
        {
            return new EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4,  TState0, TState1, TState2, TState3, TState4, TOut0, TOut1, TOut2, TOut3, TOut4>(folder0, folder1, folder2, folder3, folder4);
        }

        public static Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut> Combine<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut>(this
            IFolder<TIn, TState0, TOut0> folder0,
            IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            IFolder<TIn, TState3, TOut3> folder3,
            IFolder<TIn, TState4, TOut4> folder4,
            IFolder<TIn, TState5, TOut5> folder5,
            Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut> combiner)
        {
            return new Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut>(folder0, folder1, folder2, folder3, folder4, folder5, combiner);
        }

        public static EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5,  TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5> Either<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>(
            this  IFolder<TIn0, TState0, TOut0> folder0,  IFolder<TIn1, TState1, TOut1> folder1,  IFolder<TIn2, TState2, TOut2> folder2,  IFolder<TIn3, TState3, TOut3> folder3,  IFolder<TIn4, TState4, TOut4> folder4,  IFolder<TIn5, TState5, TOut5> folder5)
        {
            return new EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5,  TState0, TState1, TState2, TState3, TState4, TState5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>(folder0, folder1, folder2, folder3, folder4, folder5);
        }

        public static Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut> Combine<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut>(this
            IFolder<TIn, TState0, TOut0> folder0,
            IFolder<TIn, TState1, TOut1> folder1,
            IFolder<TIn, TState2, TOut2> folder2,
            IFolder<TIn, TState3, TOut3> folder3,
            IFolder<TIn, TState4, TOut4> folder4,
            IFolder<TIn, TState5, TOut5> folder5,
            IFolder<TIn, TState6, TOut6> folder6,
            Func<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut> combiner)
        {
            return new Combiner<TIn, TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut>(folder0, folder1, folder2, folder3, folder4, folder5, folder6, combiner);
        }

        public static EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6,  TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6> Either<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>(
            this  IFolder<TIn0, TState0, TOut0> folder0,  IFolder<TIn1, TState1, TOut1> folder1,  IFolder<TIn2, TState2, TOut2> folder2,  IFolder<TIn3, TState3, TOut3> folder3,  IFolder<TIn4, TState4, TOut4> folder4,  IFolder<TIn5, TState5, TOut5> folder5,  IFolder<TIn6, TState6, TOut6> folder6)
        {
            return new EitherFolder<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6,  TState0, TState1, TState2, TState3, TState4, TState5, TState6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>(folder0, folder1, folder2, folder3, folder4, folder5, folder6);
        }

    }
}