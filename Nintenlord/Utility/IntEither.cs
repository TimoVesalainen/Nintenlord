using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nintenlord.Utility
{
    [DataContract]
    [DebuggerDisplay("[{value}: {index}]")]
    public readonly struct IntEither
    {
        [DataMember]
        private readonly int value;
        [DataMember]
        private readonly int index;

        public IntEither(int value, int index)
        {
            this.value = value;
            this.index = index;
        }

        public void Apply(Action<int>[] actions)
        {
            actions[index](value);
        }

        public T Apply<T>(Func<int, T>[] funcs)
        {
            return funcs[index](value);
        }

        public override bool Equals(object obj)
        {
            return obj is IntEither either &&
                   value == either.value &&
                   index == either.index;
        }

        public override int GetHashCode()
        {
            int hashCode = -1432443131;
            hashCode = hashCode * -1521134295 + value.GetHashCode();
            hashCode = hashCode * -1521134295 + index.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{{Option{index}: {value}}}";
        }
    }
}
