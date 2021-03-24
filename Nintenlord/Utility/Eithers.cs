﻿using System;

namespace Nintenlord.Utility
{
	public sealed class Either<T0, T1>
	{
        readonly Enum2 mode;
        readonly T0 option0;
        readonly T1 option1;
        public T0 Option0 => mode == Enum2.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum2.Item0}");
        public T1 Option1 => mode == Enum2.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum2.Item1}");

        private Either(Enum2 mode, T0 option0 = default, T1 option1 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
        }

        public static implicit operator Either<T0, T1>(T0 item)
        {
            return new Either<T0, T1>(Enum2.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1>(T1 item)
        {
            return new Either<T0, T1>(Enum2.Item1, option1: item);
        }

        public static explicit operator T0(Either<T0, T1> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1> either)
        {
            return either.Option1;
        }

        public void Apply(Action<T0> action0, Action<T1> action1)
        {
            switch (this.mode)
            {
                case Enum2.Item0:
                    action0(this.option0);
                    break;
                case Enum2.Item1:
                    action1(this.option1);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1)
        {
            switch (this.mode)
            {
                case Enum2.Item0:
                    return func0(this.option0);
                case Enum2.Item1:
                    return func1(this.option1);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
	public sealed class Either<T0, T1, T2>
	{
        readonly Enum3 mode;
        readonly T0 option0;
        readonly T1 option1;
        readonly T2 option2;
        public T0 Option0 => mode == Enum3.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum3.Item0}");
        public T1 Option1 => mode == Enum3.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum3.Item1}");
        public T2 Option2 => mode == Enum3.Item2 ? option2 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum3.Item2}");

        private Either(Enum3 mode, T0 option0 = default, T1 option1 = default, T2 option2 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
            this.option2 = option2;
        }

        public static implicit operator Either<T0, T1, T2>(T0 item)
        {
            return new Either<T0, T1, T2>(Enum3.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1, T2>(T1 item)
        {
            return new Either<T0, T1, T2>(Enum3.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1, T2>(T2 item)
        {
            return new Either<T0, T1, T2>(Enum3.Item2, option2: item);
        }

        public static explicit operator T0(Either<T0, T1, T2> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1, T2> either)
        {
            return either.Option1;
        }

        public static explicit operator T2(Either<T0, T1, T2> either)
        {
            return either.Option2;
        }

        public void Apply(Action<T0> action0, Action<T1> action1, Action<T2> action2)
        {
            switch (this.mode)
            {
                case Enum3.Item0:
                    action0(this.option0);
                    break;
                case Enum3.Item1:
                    action1(this.option1);
                    break;
                case Enum3.Item2:
                    action2(this.option2);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1, Func<T2, T> func2)
        {
            switch (this.mode)
            {
                case Enum3.Item0:
                    return func0(this.option0);
                case Enum3.Item1:
                    return func1(this.option1);
                case Enum3.Item2:
                    return func2(this.option2);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
	public sealed class Either<T0, T1, T2, T3>
	{
        readonly Enum4 mode;
        readonly T0 option0;
        readonly T1 option1;
        readonly T2 option2;
        readonly T3 option3;
        public T0 Option0 => mode == Enum4.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum4.Item0}");
        public T1 Option1 => mode == Enum4.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum4.Item1}");
        public T2 Option2 => mode == Enum4.Item2 ? option2 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum4.Item2}");
        public T3 Option3 => mode == Enum4.Item3 ? option3 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum4.Item3}");

        private Either(Enum4 mode, T0 option0 = default, T1 option1 = default, T2 option2 = default, T3 option3 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
        }

        public static implicit operator Either<T0, T1, T2, T3>(T0 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T1 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T2 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item2, option2: item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T3 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item3, option3: item);
        }

        public static explicit operator T0(Either<T0, T1, T2, T3> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1, T2, T3> either)
        {
            return either.Option1;
        }

        public static explicit operator T2(Either<T0, T1, T2, T3> either)
        {
            return either.Option2;
        }

        public static explicit operator T3(Either<T0, T1, T2, T3> either)
        {
            return either.Option3;
        }

        public void Apply(Action<T0> action0, Action<T1> action1, Action<T2> action2, Action<T3> action3)
        {
            switch (this.mode)
            {
                case Enum4.Item0:
                    action0(this.option0);
                    break;
                case Enum4.Item1:
                    action1(this.option1);
                    break;
                case Enum4.Item2:
                    action2(this.option2);
                    break;
                case Enum4.Item3:
                    action3(this.option3);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1, Func<T2, T> func2, Func<T3, T> func3)
        {
            switch (this.mode)
            {
                case Enum4.Item0:
                    return func0(this.option0);
                case Enum4.Item1:
                    return func1(this.option1);
                case Enum4.Item2:
                    return func2(this.option2);
                case Enum4.Item3:
                    return func3(this.option3);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
	public sealed class Either<T0, T1, T2, T3, T4>
	{
        readonly Enum5 mode;
        readonly T0 option0;
        readonly T1 option1;
        readonly T2 option2;
        readonly T3 option3;
        readonly T4 option4;
        public T0 Option0 => mode == Enum5.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum5.Item0}");
        public T1 Option1 => mode == Enum5.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum5.Item1}");
        public T2 Option2 => mode == Enum5.Item2 ? option2 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum5.Item2}");
        public T3 Option3 => mode == Enum5.Item3 ? option3 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum5.Item3}");
        public T4 Option4 => mode == Enum5.Item4 ? option4 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum5.Item4}");

        private Either(Enum5 mode, T0 option0 = default, T1 option1 = default, T2 option2 = default, T3 option3 = default, T4 option4 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
            this.option4 = option4;
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item2, option2: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item3, option3: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item4, option4: item);
        }

        public static explicit operator T0(Either<T0, T1, T2, T3, T4> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1, T2, T3, T4> either)
        {
            return either.Option1;
        }

        public static explicit operator T2(Either<T0, T1, T2, T3, T4> either)
        {
            return either.Option2;
        }

        public static explicit operator T3(Either<T0, T1, T2, T3, T4> either)
        {
            return either.Option3;
        }

        public static explicit operator T4(Either<T0, T1, T2, T3, T4> either)
        {
            return either.Option4;
        }

        public void Apply(Action<T0> action0, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4)
        {
            switch (this.mode)
            {
                case Enum5.Item0:
                    action0(this.option0);
                    break;
                case Enum5.Item1:
                    action1(this.option1);
                    break;
                case Enum5.Item2:
                    action2(this.option2);
                    break;
                case Enum5.Item3:
                    action3(this.option3);
                    break;
                case Enum5.Item4:
                    action4(this.option4);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1, Func<T2, T> func2, Func<T3, T> func3, Func<T4, T> func4)
        {
            switch (this.mode)
            {
                case Enum5.Item0:
                    return func0(this.option0);
                case Enum5.Item1:
                    return func1(this.option1);
                case Enum5.Item2:
                    return func2(this.option2);
                case Enum5.Item3:
                    return func3(this.option3);
                case Enum5.Item4:
                    return func4(this.option4);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
	public sealed class Either<T0, T1, T2, T3, T4, T5>
	{
        readonly Enum6 mode;
        readonly T0 option0;
        readonly T1 option1;
        readonly T2 option2;
        readonly T3 option3;
        readonly T4 option4;
        readonly T5 option5;
        public T0 Option0 => mode == Enum6.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum6.Item0}");
        public T1 Option1 => mode == Enum6.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum6.Item1}");
        public T2 Option2 => mode == Enum6.Item2 ? option2 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum6.Item2}");
        public T3 Option3 => mode == Enum6.Item3 ? option3 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum6.Item3}");
        public T4 Option4 => mode == Enum6.Item4 ? option4 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum6.Item4}");
        public T5 Option5 => mode == Enum6.Item5 ? option5 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum6.Item5}");

        private Either(Enum6 mode, T0 option0 = default, T1 option1 = default, T2 option2 = default, T3 option3 = default, T4 option4 = default, T5 option5 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
            this.option4 = option4;
            this.option5 = option5;
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item2, option2: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item3, option3: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item4, option4: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T5 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item5, option5: item);
        }

        public static explicit operator T0(Either<T0, T1, T2, T3, T4, T5> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1, T2, T3, T4, T5> either)
        {
            return either.Option1;
        }

        public static explicit operator T2(Either<T0, T1, T2, T3, T4, T5> either)
        {
            return either.Option2;
        }

        public static explicit operator T3(Either<T0, T1, T2, T3, T4, T5> either)
        {
            return either.Option3;
        }

        public static explicit operator T4(Either<T0, T1, T2, T3, T4, T5> either)
        {
            return either.Option4;
        }

        public static explicit operator T5(Either<T0, T1, T2, T3, T4, T5> either)
        {
            return either.Option5;
        }

        public void Apply(Action<T0> action0, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5)
        {
            switch (this.mode)
            {
                case Enum6.Item0:
                    action0(this.option0);
                    break;
                case Enum6.Item1:
                    action1(this.option1);
                    break;
                case Enum6.Item2:
                    action2(this.option2);
                    break;
                case Enum6.Item3:
                    action3(this.option3);
                    break;
                case Enum6.Item4:
                    action4(this.option4);
                    break;
                case Enum6.Item5:
                    action5(this.option5);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1, Func<T2, T> func2, Func<T3, T> func3, Func<T4, T> func4, Func<T5, T> func5)
        {
            switch (this.mode)
            {
                case Enum6.Item0:
                    return func0(this.option0);
                case Enum6.Item1:
                    return func1(this.option1);
                case Enum6.Item2:
                    return func2(this.option2);
                case Enum6.Item3:
                    return func3(this.option3);
                case Enum6.Item4:
                    return func4(this.option4);
                case Enum6.Item5:
                    return func5(this.option5);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
	public sealed class Either<T0, T1, T2, T3, T4, T5, T6>
	{
        readonly Enum7 mode;
        readonly T0 option0;
        readonly T1 option1;
        readonly T2 option2;
        readonly T3 option3;
        readonly T4 option4;
        readonly T5 option5;
        readonly T6 option6;
        public T0 Option0 => mode == Enum7.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item0}");
        public T1 Option1 => mode == Enum7.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item1}");
        public T2 Option2 => mode == Enum7.Item2 ? option2 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item2}");
        public T3 Option3 => mode == Enum7.Item3 ? option3 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item3}");
        public T4 Option4 => mode == Enum7.Item4 ? option4 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item4}");
        public T5 Option5 => mode == Enum7.Item5 ? option5 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item5}");
        public T6 Option6 => mode == Enum7.Item6 ? option6 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum7.Item6}");

        private Either(Enum7 mode, T0 option0 = default, T1 option1 = default, T2 option2 = default, T3 option3 = default, T4 option4 = default, T5 option5 = default, T6 option6 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
            this.option4 = option4;
            this.option5 = option5;
            this.option6 = option6;
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item2, option2: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item3, option3: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item4, option4: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T5 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item5, option5: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T6 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item6, option6: item);
        }

        public static explicit operator T0(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option1;
        }

        public static explicit operator T2(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option2;
        }

        public static explicit operator T3(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option3;
        }

        public static explicit operator T4(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option4;
        }

        public static explicit operator T5(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option5;
        }

        public static explicit operator T6(Either<T0, T1, T2, T3, T4, T5, T6> either)
        {
            return either.Option6;
        }

        public void Apply(Action<T0> action0, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6)
        {
            switch (this.mode)
            {
                case Enum7.Item0:
                    action0(this.option0);
                    break;
                case Enum7.Item1:
                    action1(this.option1);
                    break;
                case Enum7.Item2:
                    action2(this.option2);
                    break;
                case Enum7.Item3:
                    action3(this.option3);
                    break;
                case Enum7.Item4:
                    action4(this.option4);
                    break;
                case Enum7.Item5:
                    action5(this.option5);
                    break;
                case Enum7.Item6:
                    action6(this.option6);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1, Func<T2, T> func2, Func<T3, T> func3, Func<T4, T> func4, Func<T5, T> func5, Func<T6, T> func6)
        {
            switch (this.mode)
            {
                case Enum7.Item0:
                    return func0(this.option0);
                case Enum7.Item1:
                    return func1(this.option1);
                case Enum7.Item2:
                    return func2(this.option2);
                case Enum7.Item3:
                    return func3(this.option3);
                case Enum7.Item4:
                    return func4(this.option4);
                case Enum7.Item5:
                    return func5(this.option5);
                case Enum7.Item6:
                    return func6(this.option6);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
	public sealed class Either<T0, T1, T2, T3, T4, T5, T6, T7>
	{
        readonly Enum8 mode;
        readonly T0 option0;
        readonly T1 option1;
        readonly T2 option2;
        readonly T3 option3;
        readonly T4 option4;
        readonly T5 option5;
        readonly T6 option6;
        readonly T7 option7;
        public T0 Option0 => mode == Enum8.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item0}");
        public T1 Option1 => mode == Enum8.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item1}");
        public T2 Option2 => mode == Enum8.Item2 ? option2 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item2}");
        public T3 Option3 => mode == Enum8.Item3 ? option3 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item3}");
        public T4 Option4 => mode == Enum8.Item4 ? option4 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item4}");
        public T5 Option5 => mode == Enum8.Item5 ? option5 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item5}");
        public T6 Option6 => mode == Enum8.Item6 ? option6 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item6}");
        public T7 Option7 => mode == Enum8.Item7 ? option7 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum8.Item7}");

        private Either(Enum8 mode, T0 option0 = default, T1 option1 = default, T2 option2 = default, T3 option3 = default, T4 option4 = default, T5 option5 = default, T6 option6 = default, T7 option7 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
            this.option4 = option4;
            this.option5 = option5;
            this.option6 = option6;
            this.option7 = option7;
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item0, option0: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item2, option2: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item3, option3: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item4, option4: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T5 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item5, option5: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T6 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item6, option6: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T7 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item7, option7: item);
        }

        public static explicit operator T0(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option1;
        }

        public static explicit operator T2(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option2;
        }

        public static explicit operator T3(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option3;
        }

        public static explicit operator T4(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option4;
        }

        public static explicit operator T5(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option5;
        }

        public static explicit operator T6(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option6;
        }

        public static explicit operator T7(Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
        {
            return either.Option7;
        }

        public void Apply(Action<T0> action0, Action<T1> action1, Action<T2> action2, Action<T3> action3, Action<T4> action4, Action<T5> action5, Action<T6> action6, Action<T7> action7)
        {
            switch (this.mode)
            {
                case Enum8.Item0:
                    action0(this.option0);
                    break;
                case Enum8.Item1:
                    action1(this.option1);
                    break;
                case Enum8.Item2:
                    action2(this.option2);
                    break;
                case Enum8.Item3:
                    action3(this.option3);
                    break;
                case Enum8.Item4:
                    action4(this.option4);
                    break;
                case Enum8.Item5:
                    action5(this.option5);
                    break;
                case Enum8.Item6:
                    action6(this.option6);
                    break;
                case Enum8.Item7:
                    action7(this.option7);
                    break;
            }
        }

        public T Apply<T>(Func<T0, T> func0, Func<T1, T> func1, Func<T2, T> func2, Func<T3, T> func3, Func<T4, T> func4, Func<T5, T> func5, Func<T6, T> func6, Func<T7, T> func7)
        {
            switch (this.mode)
            {
                case Enum8.Item0:
                    return func0(this.option0);
                case Enum8.Item1:
                    return func1(this.option1);
                case Enum8.Item2:
                    return func2(this.option2);
                case Enum8.Item3:
                    return func3(this.option3);
                case Enum8.Item4:
                    return func4(this.option4);
                case Enum8.Item5:
                    return func5(this.option5);
                case Enum8.Item6:
                    return func6(this.option6);
                case Enum8.Item7:
                    return func7(this.option7);
                default:
                    throw new InvalidProgramException();
            }
        }
	}
}