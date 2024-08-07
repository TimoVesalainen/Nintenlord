﻿using System;
using System.Reactive;

namespace Nintenlord.Utility
{
    public static partial class FunctionExtensions
    {
        public static T Apply<T, TIn0, TIn1>(this (TIn0, TIn1) tuple, Func<TIn0, TIn1, T> func)
        {
            return func(tuple.Item1, tuple.Item2);
        }

        public static T Apply<T, TIn0, TIn1>(this Tuple<TIn0, TIn1> tuple, Func<TIn0, TIn1, T> func)
        {
            return func(tuple.Item1, tuple.Item2);
        }

        public static T Apply<T, TIn0, TIn1>(this Func<(TIn0, TIn1), T> func, TIn0 in0, TIn1 in1)
        {
            return func((in0, in1));
        }

        public static T Apply<T, TIn0, TIn1>(this Func<Tuple<TIn0, TIn1>, T> func, TIn0 in0, TIn1 in1)
        {
            return func(Tuple.Create(in0, in1));
        }

        public static void Apply<TIn0, TIn1>(this (TIn0, TIn1) tuple, Action<TIn0, TIn1> action)
        {
            action(tuple.Item1, tuple.Item2);
        }

        public static void Apply<TIn0, TIn1>(this Tuple<TIn0, TIn1> tuple, Action<TIn0, TIn1> action)
        {
            action(tuple.Item1, tuple.Item2);
        }

        public static void Apply<TIn0, TIn1>(this Action<(TIn0, TIn1)> action, TIn0 in0, TIn1 in1)
        {
            action((in0, in1));
        }

        public static void Apply<TIn0, TIn1>(this Action<Tuple<TIn0, TIn1>> action, TIn0 in0, TIn1 in1)
        {
            action(Tuple.Create(in0, in1));
        }

        public static Func<TIn0, Func<TIn1, T>> Curry<T, TIn0, TIn1>(Func<TIn0, TIn1, T> func)
        {
            return in0 => in1 => func(in0, in1);
        }

        public static Func<TIn0, TIn1, T> Uncurry<T, TIn0, TIn1>(Func<TIn0, Func<TIn1, T>> func)
        {
            return (in0, in1) => func(in0)(in1);
        }

        public static Func<TIn0, TIn1, Unit> ToFunc<TIn0, TIn1>(this Action<TIn0, TIn1> action)
        {
            return (in0, in1) => { action(in0, in1); return Unit.Default; };
        }

        public static Action<TIn0, TIn1> ToAction<TIn0, TIn1>(this Func<TIn0, TIn1, Unit> func)
        {
            return (in0, in1) => { func(in0, in1); };
        }

        public static Either<T, TException> TryCatch<T, TIn0, TIn1, TException>(this Func<TIn0, TIn1, T> func, TIn0 in0, TIn1 in1) where TException : Exception
        {
            try
            {
                return func(in0, in1);
            }
            catch (TException e)
            {
                return e;
            }
        }

        public static Maybe<TException> TryCatch<TIn0, TIn1, TException>(this Action<TIn0, TIn1> func, TIn0 in0, TIn1 in1) where TException : Exception
        {
            try
            {
                func(in0, in1);
                return Maybe<TException>.Nothing;
            }
            catch (TException e)
            {
                return Maybe<TException>.Just(e);
            }
        }
        public static T Apply<T, TIn0, TIn1, TIn2>(this (TIn0, TIn1, TIn2) tuple, Func<TIn0, TIn1, TIn2, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static T Apply<T, TIn0, TIn1, TIn2>(this Tuple<TIn0, TIn1, TIn2> tuple, Func<TIn0, TIn1, TIn2, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static T Apply<T, TIn0, TIn1, TIn2>(this Func<(TIn0, TIn1, TIn2), T> func, TIn0 in0, TIn1 in1, TIn2 in2)
        {
            return func((in0, in1, in2));
        }

        public static T Apply<T, TIn0, TIn1, TIn2>(this Func<Tuple<TIn0, TIn1, TIn2>, T> func, TIn0 in0, TIn1 in1, TIn2 in2)
        {
            return func(Tuple.Create(in0, in1, in2));
        }

        public static void Apply<TIn0, TIn1, TIn2>(this (TIn0, TIn1, TIn2) tuple, Action<TIn0, TIn1, TIn2> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static void Apply<TIn0, TIn1, TIn2>(this Tuple<TIn0, TIn1, TIn2> tuple, Action<TIn0, TIn1, TIn2> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static void Apply<TIn0, TIn1, TIn2>(this Action<(TIn0, TIn1, TIn2)> action, TIn0 in0, TIn1 in1, TIn2 in2)
        {
            action((in0, in1, in2));
        }

        public static void Apply<TIn0, TIn1, TIn2>(this Action<Tuple<TIn0, TIn1, TIn2>> action, TIn0 in0, TIn1 in1, TIn2 in2)
        {
            action(Tuple.Create(in0, in1, in2));
        }

        public static Func<TIn0, Func<TIn1, Func<TIn2, T>>> Curry<T, TIn0, TIn1, TIn2>(Func<TIn0, TIn1, TIn2, T> func)
        {
            return in0 => in1 => in2 => func(in0, in1, in2);
        }

        public static Func<TIn0, TIn1, TIn2, T> Uncurry<T, TIn0, TIn1, TIn2>(Func<TIn0, Func<TIn1, Func<TIn2, T>>> func)
        {
            return (in0, in1, in2) => func(in0)(in1)(in2);
        }

        public static Func<TIn0, TIn1, TIn2, Unit> ToFunc<TIn0, TIn1, TIn2>(this Action<TIn0, TIn1, TIn2> action)
        {
            return (in0, in1, in2) => { action(in0, in1, in2); return Unit.Default; };
        }

        public static Action<TIn0, TIn1, TIn2> ToAction<TIn0, TIn1, TIn2>(this Func<TIn0, TIn1, TIn2, Unit> func)
        {
            return (in0, in1, in2) => { func(in0, in1, in2); };
        }

        public static Either<T, TException> TryCatch<T, TIn0, TIn1, TIn2, TException>(this Func<TIn0, TIn1, TIn2, T> func, TIn0 in0, TIn1 in1, TIn2 in2) where TException : Exception
        {
            try
            {
                return func(in0, in1, in2);
            }
            catch (TException e)
            {
                return e;
            }
        }

        public static Maybe<TException> TryCatch<TIn0, TIn1, TIn2, TException>(this Action<TIn0, TIn1, TIn2> func, TIn0 in0, TIn1 in1, TIn2 in2) where TException : Exception
        {
            try
            {
                func(in0, in1, in2);
                return Maybe<TException>.Nothing;
            }
            catch (TException e)
            {
                return Maybe<TException>.Just(e);
            }
        }
        public static T Apply<T, TIn0, TIn1, TIn2, TIn3>(this (TIn0, TIn1, TIn2, TIn3) tuple, Func<TIn0, TIn1, TIn2, TIn3, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3>(this Tuple<TIn0, TIn1, TIn2, TIn3> tuple, Func<TIn0, TIn1, TIn2, TIn3, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3>(this Func<(TIn0, TIn1, TIn2, TIn3), T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3)
        {
            return func((in0, in1, in2, in3));
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3>(this Func<Tuple<TIn0, TIn1, TIn2, TIn3>, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3)
        {
            return func(Tuple.Create(in0, in1, in2, in3));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3>(this (TIn0, TIn1, TIn2, TIn3) tuple, Action<TIn0, TIn1, TIn2, TIn3> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3>(this Tuple<TIn0, TIn1, TIn2, TIn3> tuple, Action<TIn0, TIn1, TIn2, TIn3> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3>(this Action<(TIn0, TIn1, TIn2, TIn3)> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3)
        {
            action((in0, in1, in2, in3));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3>(this Action<Tuple<TIn0, TIn1, TIn2, TIn3>> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3)
        {
            action(Tuple.Create(in0, in1, in2, in3));
        }

        public static Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, T>>>> Curry<T, TIn0, TIn1, TIn2, TIn3>(Func<TIn0, TIn1, TIn2, TIn3, T> func)
        {
            return in0 => in1 => in2 => in3 => func(in0, in1, in2, in3);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, T> Uncurry<T, TIn0, TIn1, TIn2, TIn3>(Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, T>>>> func)
        {
            return (in0, in1, in2, in3) => func(in0)(in1)(in2)(in3);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, Unit> ToFunc<TIn0, TIn1, TIn2, TIn3>(this Action<TIn0, TIn1, TIn2, TIn3> action)
        {
            return (in0, in1, in2, in3) => { action(in0, in1, in2, in3); return Unit.Default; };
        }

        public static Action<TIn0, TIn1, TIn2, TIn3> ToAction<TIn0, TIn1, TIn2, TIn3>(this Func<TIn0, TIn1, TIn2, TIn3, Unit> func)
        {
            return (in0, in1, in2, in3) => { func(in0, in1, in2, in3); };
        }

        public static Either<T, TException> TryCatch<T, TIn0, TIn1, TIn2, TIn3, TException>(this Func<TIn0, TIn1, TIn2, TIn3, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3) where TException : Exception
        {
            try
            {
                return func(in0, in1, in2, in3);
            }
            catch (TException e)
            {
                return e;
            }
        }

        public static Maybe<TException> TryCatch<TIn0, TIn1, TIn2, TIn3, TException>(this Action<TIn0, TIn1, TIn2, TIn3> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3) where TException : Exception
        {
            try
            {
                func(in0, in1, in2, in3);
                return Maybe<TException>.Nothing;
            }
            catch (TException e)
            {
                return Maybe<TException>.Just(e);
            }
        }
        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4>(this (TIn0, TIn1, TIn2, TIn3, TIn4) tuple, Func<TIn0, TIn1, TIn2, TIn3, TIn4, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4>(this Tuple<TIn0, TIn1, TIn2, TIn3, TIn4> tuple, Func<TIn0, TIn1, TIn2, TIn3, TIn4, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4>(this Func<(TIn0, TIn1, TIn2, TIn3, TIn4), T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4)
        {
            return func((in0, in1, in2, in3, in4));
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4>(this Func<Tuple<TIn0, TIn1, TIn2, TIn3, TIn4>, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4)
        {
            return func(Tuple.Create(in0, in1, in2, in3, in4));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4>(this (TIn0, TIn1, TIn2, TIn3, TIn4) tuple, Action<TIn0, TIn1, TIn2, TIn3, TIn4> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4>(this Tuple<TIn0, TIn1, TIn2, TIn3, TIn4> tuple, Action<TIn0, TIn1, TIn2, TIn3, TIn4> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4>(this Action<(TIn0, TIn1, TIn2, TIn3, TIn4)> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4)
        {
            action((in0, in1, in2, in3, in4));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4>(this Action<Tuple<TIn0, TIn1, TIn2, TIn3, TIn4>> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4)
        {
            action(Tuple.Create(in0, in1, in2, in3, in4));
        }

        public static Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, Func<TIn4, T>>>>> Curry<T, TIn0, TIn1, TIn2, TIn3, TIn4>(Func<TIn0, TIn1, TIn2, TIn3, TIn4, T> func)
        {
            return in0 => in1 => in2 => in3 => in4 => func(in0, in1, in2, in3, in4);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, TIn4, T> Uncurry<T, TIn0, TIn1, TIn2, TIn3, TIn4>(Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, Func<TIn4, T>>>>> func)
        {
            return (in0, in1, in2, in3, in4) => func(in0)(in1)(in2)(in3)(in4);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, TIn4, Unit> ToFunc<TIn0, TIn1, TIn2, TIn3, TIn4>(this Action<TIn0, TIn1, TIn2, TIn3, TIn4> action)
        {
            return (in0, in1, in2, in3, in4) => { action(in0, in1, in2, in3, in4); return Unit.Default; };
        }

        public static Action<TIn0, TIn1, TIn2, TIn3, TIn4> ToAction<TIn0, TIn1, TIn2, TIn3, TIn4>(this Func<TIn0, TIn1, TIn2, TIn3, TIn4, Unit> func)
        {
            return (in0, in1, in2, in3, in4) => { func(in0, in1, in2, in3, in4); };
        }

        public static Either<T, TException> TryCatch<T, TIn0, TIn1, TIn2, TIn3, TIn4, TException>(this Func<TIn0, TIn1, TIn2, TIn3, TIn4, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4) where TException : Exception
        {
            try
            {
                return func(in0, in1, in2, in3, in4);
            }
            catch (TException e)
            {
                return e;
            }
        }

        public static Maybe<TException> TryCatch<TIn0, TIn1, TIn2, TIn3, TIn4, TException>(this Action<TIn0, TIn1, TIn2, TIn3, TIn4> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4) where TException : Exception
        {
            try
            {
                func(in0, in1, in2, in3, in4);
                return Maybe<TException>.Nothing;
            }
            catch (TException e)
            {
                return Maybe<TException>.Just(e);
            }
        }
        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this (TIn0, TIn1, TIn2, TIn3, TIn4, TIn5) tuple, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> tuple, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Func<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5), T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5)
        {
            return func((in0, in1, in2, in3, in4, in5));
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Func<Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5)
        {
            return func(Tuple.Create(in0, in1, in2, in3, in4, in5));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this (TIn0, TIn1, TIn2, TIn3, TIn4, TIn5) tuple, Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> tuple, Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Action<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5)> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5)
        {
            action((in0, in1, in2, in3, in4, in5));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Action<Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5)
        {
            action(Tuple.Create(in0, in1, in2, in3, in4, in5));
        }

        public static Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, Func<TIn4, Func<TIn5, T>>>>>> Curry<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, T> func)
        {
            return in0 => in1 => in2 => in3 => in4 => in5 => func(in0, in1, in2, in3, in4, in5);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, T> Uncurry<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, Func<TIn4, Func<TIn5, T>>>>>> func)
        {
            return (in0, in1, in2, in3, in4, in5) => func(in0)(in1)(in2)(in3)(in4)(in5);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, Unit> ToFunc<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> action)
        {
            return (in0, in1, in2, in3, in4, in5) => { action(in0, in1, in2, in3, in4, in5); return Unit.Default; };
        }

        public static Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> ToAction<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, Unit> func)
        {
            return (in0, in1, in2, in3, in4, in5) => { func(in0, in1, in2, in3, in4, in5); };
        }

        public static Either<T, TException> TryCatch<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TException>(this Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5) where TException : Exception
        {
            try
            {
                return func(in0, in1, in2, in3, in4, in5);
            }
            catch (TException e)
            {
                return e;
            }
        }

        public static Maybe<TException> TryCatch<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TException>(this Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5) where TException : Exception
        {
            try
            {
                func(in0, in1, in2, in3, in4, in5);
                return Maybe<TException>.Nothing;
            }
            catch (TException e)
            {
                return Maybe<TException>.Just(e);
            }
        }
        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this (TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6) tuple, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> tuple, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> func)
        {
            return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Func<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6), T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5, TIn6 in6)
        {
            return func((in0, in1, in2, in3, in4, in5, in6));
        }

        public static T Apply<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Func<Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5, TIn6 in6)
        {
            return func(Tuple.Create(in0, in1, in2, in3, in4, in5, in6));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this (TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6) tuple, Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> tuple, Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> action)
        {
            action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Action<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6)> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5, TIn6 in6)
        {
            action((in0, in1, in2, in3, in4, in5, in6));
        }

        public static void Apply<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Action<Tuple<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>> action, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5, TIn6 in6)
        {
            action(Tuple.Create(in0, in1, in2, in3, in4, in5, in6));
        }

        public static Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, Func<TIn4, Func<TIn5, Func<TIn6, T>>>>>>> Curry<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> func)
        {
            return in0 => in1 => in2 => in3 => in4 => in5 => in6 => func(in0, in1, in2, in3, in4, in5, in6);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> Uncurry<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(Func<TIn0, Func<TIn1, Func<TIn2, Func<TIn3, Func<TIn4, Func<TIn5, Func<TIn6, T>>>>>>> func)
        {
            return (in0, in1, in2, in3, in4, in5, in6) => func(in0)(in1)(in2)(in3)(in4)(in5)(in6);
        }

        public static Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, Unit> ToFunc<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> action)
        {
            return (in0, in1, in2, in3, in4, in5, in6) => { action(in0, in1, in2, in3, in4, in5, in6); return Unit.Default; };
        }

        public static Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> ToAction<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, Unit> func)
        {
            return (in0, in1, in2, in3, in4, in5, in6) => { func(in0, in1, in2, in3, in4, in5, in6); };
        }

        public static Either<T, TException> TryCatch<T, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TException>(this Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5, TIn6 in6) where TException : Exception
        {
            try
            {
                return func(in0, in1, in2, in3, in4, in5, in6);
            }
            catch (TException e)
            {
                return e;
            }
        }

        public static Maybe<TException> TryCatch<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TException>(this Action<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> func, TIn0 in0, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5, TIn6 in6) where TException : Exception
        {
            try
            {
                func(in0, in1, in2, in3, in4, in5, in6);
                return Maybe<TException>.Nothing;
            }
            catch (TException e)
            {
                return Maybe<TException>.Just(e);
            }
        }
    }
}