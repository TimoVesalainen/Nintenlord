using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Utility
{
    public delegate bool TryGetDelegate<TOut>(out TOut value);
    public delegate bool TryGetDelegate<in TIn, TOut>(TIn input, out TOut result);
    public delegate bool TryGetDelegate<in TIn1, in TIn2, TValue>(TIn1 input1, TIn2 input2, out TValue value);
    public delegate bool TryGetDelegate<in TIn1, in TIn2, in TIn3, TValue>(TIn1 input1, TIn2 input2, TIn3 input3, out TValue value);
    public delegate bool TryGetDelegate<in TIn1, in TIn2, in TIn3, in TIn4, TValue>(TIn1 input1, TIn2 input2, TIn3 input3, TIn4 input4, out TValue value);

    public static class TryGetHelpers
    {
        public static Maybe<TValue> TryGetMaybe<TValue>(TryGetDelegate<TValue> tryGetValue)
        {
            if (tryGetValue(out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }

        public static Maybe<TValue> TryGetMaybe<TKey, TValue>(TryGetDelegate<TKey, TValue> tryGetValue, TKey key)
        {
            if (tryGetValue(key, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }

        public static Maybe<TValue> TryGetMaybe<TKey1, TKey2, TValue>(TryGetDelegate<TKey1, TKey2, TValue> tryGetValue, TKey1 key1, TKey2 key2)
        {
            if (tryGetValue(key1, key2, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }

        public static Maybe<TValue> TryGetMaybe<TKey1, TKey2, TKey3, TValue>(TryGetDelegate<TKey1, TKey2, TKey3, TValue> tryGetValue, TKey1 key1, TKey2 key2, TKey3 key3)
        {
            if (tryGetValue(key1, key2, key3, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }

        public static Maybe<TValue> TryGetMaybe<TKey1, TKey2, TKey3, TKey4, TValue>(
            TryGetDelegate<TKey1, TKey2, TKey3, TKey4, TValue> tryGetValue, TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
        {
            if (tryGetValue(key1, key2, key3, key4, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }
        public static Either<TValue, TError> TryGetEither<TValue, TError>(TryGetDelegate<TValue> tryGetValue, TError error)
        {
            if (tryGetValue(out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
        public static Either<TValue, TError> TryGetEither<TKey, TValue, TError>(TryGetDelegate<TKey, TValue> tryGetValue, TKey key, TError error)
        {
            if (tryGetValue(key, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
        public static Either<TValue, TError> TryGetEither<TKey1, TKey2, TValue, TError>(TryGetDelegate<TKey1, TKey2, TValue> tryGetValue, TKey1 key1, TKey2 key2, TError error)
        {
            if (tryGetValue(key1, key2, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
        public static Either<TValue, TError> TryGetEither<TKey1, TKey2, TKey3, TValue, TError>(TryGetDelegate<TKey1, TKey2, TKey3, TValue> tryGetValue, TKey1 key1, TKey2 key2, TKey3 key3, TError error)
        {
            if (tryGetValue(key1, key2, key3, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
        public static Either<TValue, TError> TryGetEither<TKey1, TKey2, TKey3, TKey4, TValue, TError>(
            TryGetDelegate<TKey1, TKey2, TKey3, TKey4, TValue> tryGetValue, TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TError error)
        {
            if (tryGetValue(key1, key2, key3, key4, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
    }
}
