using System;

namespace Nintenlord.Utility
{
    public sealed class Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly bool hasValue;
        private readonly T value;


        readonly static Maybe<T> nothing = new Maybe<T>(false, default);
        public static Maybe<T> Nothing => nothing;

        public bool HasValue => hasValue;
        public T Value
        {
            get 
            {
                if (hasValue)
                {
                    return value;
                }
                else
                {
                    throw new InvalidOperationException("Can't get value out of nothing");
                }
            }
        }

        private Maybe(bool hasValue, T value)
        {
            this.hasValue = hasValue;
            this.value = value;
        }

        public static Maybe<T> Just(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Maybe<T>(true, value);
        }

        public static implicit operator Maybe<T>(T value)
        {
            if (value == null)
            {
                return Nothing;
            }
            else
            {
                return Just(value);
            }
        }

        public static explicit operator T(Maybe<T> value)
        {
            if (value.hasValue)
            {
                return value.value;
            }
            else
            {
                throw new InvalidCastException("Cannot cast nothing to valid value.");
            }
        }

        public bool Equals(Maybe<T> other)
        {
            if (this.hasValue && other.hasValue)
            {
                return Equals(this.value, other.value);
            }
            else
            {
                return this.hasValue == other.hasValue;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Maybe<T> maybe)
            {
                return Equals(maybe);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value, hasValue);
        }

        public override string ToString()
        {
            if (this.HasValue)
            {
                return $"{{Just {this.Value}}}";
            }
            else
            {
                return "{Nothing}";
            }
        }
    }
}
