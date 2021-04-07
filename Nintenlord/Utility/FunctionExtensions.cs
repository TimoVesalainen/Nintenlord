using System;
using System.Reactive;

namespace Nintenlord.Utility
{
    public static partial class FunctionExtensions
    {
        public static Tuple<T1, T2, T3> Flatten<T1, T2, T3>(this Tuple<T1, Tuple<T2, T3>> tuple)
        {
            return Tuple.Create(tuple.Item1, tuple.Item2.Item1, tuple.Item2.Item2);
        }

        public static Tuple<T1, T2, T3> Flatten<T1, T2, T3>(this Tuple<Tuple<T1, T2>, T3> tuple)
        {
            return Tuple.Create(tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2);
        }

        public static Func<Unit> ToFunc(this Action action)
        {
            return () => { action(); return Unit.Default; };
        }

        public static Action ToAction(this Func<Unit> func)
        {
            return () => { func(); };
        }

        public static Action ApplyResult<T>(this Func<T> calculation, Action<T> application)
        {
            return () => application(calculation());
        }

        public static Action<T> Concatenate<T>(params Action<T>[] actions)
        {
            return x =>
                {
                    foreach (var action in actions)
                    {
                        action(x);
                    }
                };
        }
    }
}
