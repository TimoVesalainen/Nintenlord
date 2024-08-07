﻿using System;
using System.Runtime.Serialization;

namespace Nintenlord.Utility
{
    [DataContract]
    public sealed class Either<T0, T1> : IEquatable<Either<T0, T1>>
    {
        [DataMember]
        private readonly Enum2 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        public T0 Option0 => mode == Enum2.Item0 ? option0 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum2.Item0}");
        public T1 Option1 => mode == Enum2.Item1 ? option1 : throw new InvalidOperationException($"Either is {this.mode}, not {Enum2.Item1}");

        private Either(Enum2 mode, T0 option0 = default, T1 option1 = default)
        {
            this.mode = mode;
            this.option0 = option0;
            this.option1 = option1;
        }

        public static Either<T0, T1> From0(T0 item)
        {
            return new Either<T0, T1>(Enum2.Item0, option0: item);
        }

        public static Either<T0, T1> From1(T1 item)
        {
            return new Either<T0, T1>(Enum2.Item1, option1: item);
        }

        public static implicit operator Either<T0, T1>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1>(T1 item)
        {
            return From1(item);
        }

        public static explicit operator T0(Either<T0, T1> either)
        {
            return either.Option0;
        }

        public static explicit operator T1(Either<T0, T1> either)
        {
            return either.Option1;
        }

        private object ToObject()
        {
            return this.mode switch
            {
                Enum2.Item0 => this.option0,
                Enum2.Item1 => this.option1,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum2.Item0 => func0(this.option0),
                Enum2.Item1 => func1(this.option1),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum2.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum2.Item1;
        }
    }
    [DataContract]
    public sealed class Either<T0, T1, T2> : IEquatable<Either<T0, T1, T2>>
    {
        [DataMember]
        private readonly Enum3 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        [DataMember]
        private readonly T2 option2;
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

        public static Either<T0, T1, T2> From0(T0 item)
        {
            return new Either<T0, T1, T2>(Enum3.Item0, option0: item);
        }

        public static Either<T0, T1, T2> From1(T1 item)
        {
            return new Either<T0, T1, T2>(Enum3.Item1, option1: item);
        }

        public static Either<T0, T1, T2> From2(T2 item)
        {
            return new Either<T0, T1, T2>(Enum3.Item2, option2: item);
        }

        public static implicit operator Either<T0, T1, T2>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1, T2>(T1 item)
        {
            return From1(item);
        }

        public static implicit operator Either<T0, T1, T2>(T2 item)
        {
            return From2(item);
        }

        public static implicit operator Either<T0, T1, T2>(Either<T0, T1> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2>(Either<T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2>>(x => x, x => x);
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

        private object ToObject()
        {
            return this.mode switch
            {
                Enum3.Item0 => this.option0,
                Enum3.Item1 => this.option1,
                Enum3.Item2 => this.option2,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1, T2>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1, T2> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1, T2> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum3.Item0 => func0(this.option0),
                Enum3.Item1 => func1(this.option1),
                Enum3.Item2 => func2(this.option2),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum3.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum3.Item1;
        }

        public bool TryGetValue2(out T2 item)
        {
            item = option2;
            return this.mode == Enum3.Item2;
        }
    }
    [DataContract]
    public sealed class Either<T0, T1, T2, T3> : IEquatable<Either<T0, T1, T2, T3>>
    {
        [DataMember]
        private readonly Enum4 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        [DataMember]
        private readonly T2 option2;
        [DataMember]
        private readonly T3 option3;
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

        public static Either<T0, T1, T2, T3> From0(T0 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item0, option0: item);
        }

        public static Either<T0, T1, T2, T3> From1(T1 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item1, option1: item);
        }

        public static Either<T0, T1, T2, T3> From2(T2 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item2, option2: item);
        }

        public static Either<T0, T1, T2, T3> From3(T3 item)
        {
            return new Either<T0, T1, T2, T3>(Enum4.Item3, option3: item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T1 item)
        {
            return From1(item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T2 item)
        {
            return From2(item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(T3 item)
        {
            return From3(item);
        }

        public static implicit operator Either<T0, T1, T2, T3>(Either<T0, T1> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3>(Either<T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3>(Either<T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3>(Either<T0, T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3>(Either<T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3>>(x => x, x => x, x => x);
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

        private object ToObject()
        {
            return this.mode switch
            {
                Enum4.Item0 => this.option0,
                Enum4.Item1 => this.option1,
                Enum4.Item2 => this.option2,
                Enum4.Item3 => this.option3,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1, T2, T3>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1, T2, T3> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1, T2, T3> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum4.Item0 => func0(this.option0),
                Enum4.Item1 => func1(this.option1),
                Enum4.Item2 => func2(this.option2),
                Enum4.Item3 => func3(this.option3),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum4.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum4.Item1;
        }

        public bool TryGetValue2(out T2 item)
        {
            item = option2;
            return this.mode == Enum4.Item2;
        }

        public bool TryGetValue3(out T3 item)
        {
            item = option3;
            return this.mode == Enum4.Item3;
        }
    }
    [DataContract]
    public sealed class Either<T0, T1, T2, T3, T4> : IEquatable<Either<T0, T1, T2, T3, T4>>
    {
        [DataMember]
        private readonly Enum5 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        [DataMember]
        private readonly T2 option2;
        [DataMember]
        private readonly T3 option3;
        [DataMember]
        private readonly T4 option4;
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

        public static Either<T0, T1, T2, T3, T4> From0(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item0, option0: item);
        }

        public static Either<T0, T1, T2, T3, T4> From1(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item1, option1: item);
        }

        public static Either<T0, T1, T2, T3, T4> From2(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item2, option2: item);
        }

        public static Either<T0, T1, T2, T3, T4> From3(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item3, option3: item);
        }

        public static Either<T0, T1, T2, T3, T4> From4(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4>(Enum5.Item4, option4: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T1 item)
        {
            return From1(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T2 item)
        {
            return From2(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T3 item)
        {
            return From3(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(T4 item)
        {
            return From4(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T0, T1> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T0, T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T0, T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4>(Either<T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x, x => x);
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

        private object ToObject()
        {
            return this.mode switch
            {
                Enum5.Item0 => this.option0,
                Enum5.Item1 => this.option1,
                Enum5.Item2 => this.option2,
                Enum5.Item3 => this.option3,
                Enum5.Item4 => this.option4,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1, T2, T3, T4>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1, T2, T3, T4> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1, T2, T3, T4> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum5.Item0 => func0(this.option0),
                Enum5.Item1 => func1(this.option1),
                Enum5.Item2 => func2(this.option2),
                Enum5.Item3 => func3(this.option3),
                Enum5.Item4 => func4(this.option4),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum5.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum5.Item1;
        }

        public bool TryGetValue2(out T2 item)
        {
            item = option2;
            return this.mode == Enum5.Item2;
        }

        public bool TryGetValue3(out T3 item)
        {
            item = option3;
            return this.mode == Enum5.Item3;
        }

        public bool TryGetValue4(out T4 item)
        {
            item = option4;
            return this.mode == Enum5.Item4;
        }
    }
    [DataContract]
    public sealed class Either<T0, T1, T2, T3, T4, T5> : IEquatable<Either<T0, T1, T2, T3, T4, T5>>
    {
        [DataMember]
        private readonly Enum6 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        [DataMember]
        private readonly T2 option2;
        [DataMember]
        private readonly T3 option3;
        [DataMember]
        private readonly T4 option4;
        [DataMember]
        private readonly T5 option5;
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

        public static Either<T0, T1, T2, T3, T4, T5> From0(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item0, option0: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5> From1(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item1, option1: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5> From2(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item2, option2: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5> From3(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item3, option3: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5> From4(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item4, option4: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5> From5(T5 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5>(Enum6.Item5, option5: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T1 item)
        {
            return From1(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T2 item)
        {
            return From2(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T3 item)
        {
            return From3(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T4 item)
        {
            return From4(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(T5 item)
        {
            return From5(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T0, T1> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T0, T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T0, T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T0, T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5>(Either<T1, T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
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

        private object ToObject()
        {
            return this.mode switch
            {
                Enum6.Item0 => this.option0,
                Enum6.Item1 => this.option1,
                Enum6.Item2 => this.option2,
                Enum6.Item3 => this.option3,
                Enum6.Item4 => this.option4,
                Enum6.Item5 => this.option5,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1, T2, T3, T4, T5>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1, T2, T3, T4, T5> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1, T2, T3, T4, T5> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum6.Item0 => func0(this.option0),
                Enum6.Item1 => func1(this.option1),
                Enum6.Item2 => func2(this.option2),
                Enum6.Item3 => func3(this.option3),
                Enum6.Item4 => func4(this.option4),
                Enum6.Item5 => func5(this.option5),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum6.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum6.Item1;
        }

        public bool TryGetValue2(out T2 item)
        {
            item = option2;
            return this.mode == Enum6.Item2;
        }

        public bool TryGetValue3(out T3 item)
        {
            item = option3;
            return this.mode == Enum6.Item3;
        }

        public bool TryGetValue4(out T4 item)
        {
            item = option4;
            return this.mode == Enum6.Item4;
        }

        public bool TryGetValue5(out T5 item)
        {
            item = option5;
            return this.mode == Enum6.Item5;
        }
    }
    [DataContract]
    public sealed class Either<T0, T1, T2, T3, T4, T5, T6> : IEquatable<Either<T0, T1, T2, T3, T4, T5, T6>>
    {
        [DataMember]
        private readonly Enum7 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        [DataMember]
        private readonly T2 option2;
        [DataMember]
        private readonly T3 option3;
        [DataMember]
        private readonly T4 option4;
        [DataMember]
        private readonly T5 option5;
        [DataMember]
        private readonly T6 option6;
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

        public static Either<T0, T1, T2, T3, T4, T5, T6> From0(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item0, option0: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6> From1(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item1, option1: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6> From2(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item2, option2: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6> From3(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item3, option3: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6> From4(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item4, option4: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6> From5(T5 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item5, option5: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6> From6(T6 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6>(Enum7.Item6, option6: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T1 item)
        {
            return From1(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T2 item)
        {
            return From2(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T3 item)
        {
            return From3(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T4 item)
        {
            return From4(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T5 item)
        {
            return From5(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(T6 item)
        {
            return From6(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T0, T1> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T0, T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T0, T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T0, T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T1, T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T2, T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T0, T1, T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6>(Either<T1, T2, T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
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

        private object ToObject()
        {
            return this.mode switch
            {
                Enum7.Item0 => this.option0,
                Enum7.Item1 => this.option1,
                Enum7.Item2 => this.option2,
                Enum7.Item3 => this.option3,
                Enum7.Item4 => this.option4,
                Enum7.Item5 => this.option5,
                Enum7.Item6 => this.option6,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1, T2, T3, T4, T5, T6>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1, T2, T3, T4, T5, T6> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1, T2, T3, T4, T5, T6> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum7.Item0 => func0(this.option0),
                Enum7.Item1 => func1(this.option1),
                Enum7.Item2 => func2(this.option2),
                Enum7.Item3 => func3(this.option3),
                Enum7.Item4 => func4(this.option4),
                Enum7.Item5 => func5(this.option5),
                Enum7.Item6 => func6(this.option6),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum7.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum7.Item1;
        }

        public bool TryGetValue2(out T2 item)
        {
            item = option2;
            return this.mode == Enum7.Item2;
        }

        public bool TryGetValue3(out T3 item)
        {
            item = option3;
            return this.mode == Enum7.Item3;
        }

        public bool TryGetValue4(out T4 item)
        {
            item = option4;
            return this.mode == Enum7.Item4;
        }

        public bool TryGetValue5(out T5 item)
        {
            item = option5;
            return this.mode == Enum7.Item5;
        }

        public bool TryGetValue6(out T6 item)
        {
            item = option6;
            return this.mode == Enum7.Item6;
        }
    }
    [DataContract]
    public sealed class Either<T0, T1, T2, T3, T4, T5, T6, T7> : IEquatable<Either<T0, T1, T2, T3, T4, T5, T6, T7>>
    {
        [DataMember]
        private readonly Enum8 mode;
        [DataMember]
        private readonly T0 option0;
        [DataMember]
        private readonly T1 option1;
        [DataMember]
        private readonly T2 option2;
        [DataMember]
        private readonly T3 option3;
        [DataMember]
        private readonly T4 option4;
        [DataMember]
        private readonly T5 option5;
        [DataMember]
        private readonly T6 option6;
        [DataMember]
        private readonly T7 option7;
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

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From0(T0 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item0, option0: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From1(T1 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item1, option1: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From2(T2 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item2, option2: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From3(T3 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item3, option3: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From4(T4 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item4, option4: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From5(T5 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item5, option5: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From6(T6 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item6, option6: item);
        }

        public static Either<T0, T1, T2, T3, T4, T5, T6, T7> From7(T7 item)
        {
            return new Either<T0, T1, T2, T3, T4, T5, T6, T7>(Enum8.Item7, option7: item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T0 item)
        {
            return From0(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T1 item)
        {
            return From1(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T2 item)
        {
            return From2(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T3 item)
        {
            return From3(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T4 item)
        {
            return From4(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T5 item)
        {
            return From5(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T6 item)
        {
            return From6(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(T7 item)
        {
            return From7(item);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T0, T1> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T6, T7> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T0, T1, T2> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T5, T6, T7> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T0, T1, T2, T3> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T4, T5, T6, T7> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T0, T1, T2, T3, T4> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T1, T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T2, T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T3, T4, T5, T6, T7> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T0, T1, T2, T3, T4, T5> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T1, T2, T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T2, T3, T4, T5, T6, T7> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T0, T1, T2, T3, T4, T5, T6> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
        }

        public static implicit operator Either<T0, T1, T2, T3, T4, T5, T6, T7>(Either<T1, T2, T3, T4, T5, T6, T7> smaller)
        {
            return smaller.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
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

        private object ToObject()
        {
            return this.mode switch
            {
                Enum8.Item0 => this.option0,
                Enum8.Item1 => this.option1,
                Enum8.Item2 => this.option2,
                Enum8.Item3 => this.option3,
                Enum8.Item4 => this.option4,
                Enum8.Item5 => this.option5,
                Enum8.Item6 => this.option6,
                Enum8.Item7 => this.option7,
                _ => throw new InvalidProgramException(),
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Either<T0, T1, T2, T3, T4, T5, T6, T7>)} {this.mode} {this.ToObject()}}}";
        }

        public bool Equals(Either<T0, T1, T2, T3, T4, T5, T6, T7> other)
        {
            if (this.mode == other?.mode)
            {
                return Equals(this.ToObject(), other.ToObject());
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Either<T0, T1, T2, T3, T4, T5, T6, T7> either)
            {
                return Equals(either);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ToObject().GetHashCode();
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
            return this.mode switch
            {
                Enum8.Item0 => func0(this.option0),
                Enum8.Item1 => func1(this.option1),
                Enum8.Item2 => func2(this.option2),
                Enum8.Item3 => func3(this.option3),
                Enum8.Item4 => func4(this.option4),
                Enum8.Item5 => func5(this.option5),
                Enum8.Item6 => func6(this.option6),
                Enum8.Item7 => func7(this.option7),
                _ => throw new InvalidProgramException(),
            };
        }

        public bool TryGetValue0(out T0 item)
        {
            item = option0;
            return this.mode == Enum8.Item0;
        }

        public bool TryGetValue1(out T1 item)
        {
            item = option1;
            return this.mode == Enum8.Item1;
        }

        public bool TryGetValue2(out T2 item)
        {
            item = option2;
            return this.mode == Enum8.Item2;
        }

        public bool TryGetValue3(out T3 item)
        {
            item = option3;
            return this.mode == Enum8.Item3;
        }

        public bool TryGetValue4(out T4 item)
        {
            item = option4;
            return this.mode == Enum8.Item4;
        }

        public bool TryGetValue5(out T5 item)
        {
            item = option5;
            return this.mode == Enum8.Item5;
        }

        public bool TryGetValue6(out T6 item)
        {
            item = option6;
            return this.mode == Enum8.Item6;
        }

        public bool TryGetValue7(out T7 item)
        {
            item = option7;
            return this.mode == Enum8.Item7;
        }
    }
}