using System;

namespace Nintenlord.Utility
{
    /// <summary>
    /// Remembers the previous assigned value as well as the current one.
    /// </summary>
    public struct RememberPreviousValue<T>
    {
        public T Current
        {
            get => current;
            set
            {
                previous = current;
                current = value;
            }
        }
        public T Previous => previous;

        private T current;
        private T previous;

        public RememberPreviousValue(T startValue)
        {
            current = startValue;
            previous = default;
        }

        public TOut Apply<TOut>(Func<T, T, TOut> difference)
        {
            return difference(current, previous);
        }

        public void RestorePrevious()
        {
            current = previous;
        }

        public static implicit operator RememberPreviousValue<T>(T value)
        {
            return new RememberPreviousValue<T>(value);
        }

        public static explicit operator T(RememberPreviousValue<T> value)
        {
            return value.current;
        }

        public override string ToString()
        {
            return string.Format("Current: {0}, Previous: {1}", current, previous);
        }
    }
}
