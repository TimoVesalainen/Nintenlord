using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Nintenlord.Utility
{
    public static class MoreEitherHelpers
    {
        public static Either<T, TException> TryCatch<T, TException>(this Func<T> function)where TException : Exception
        {
            try
            {
                return function();
            }
            catch (TException e)
            {
                return e;
            }
        }

        #region TryGetHelpers

        private delegate bool TryGetDelegate<TValue>(out TValue value);
        private delegate bool TryGetDelegate<TKey, TValue>(TKey key, out TValue value);
        private delegate bool TryGetDelegate<TKey1, TKey2, TValue>(TKey1 key1, TKey2 key2, out TValue value);
        private delegate bool TryGetDelegate<TKey1, TKey2, TKey3, TValue>(TKey1 key1, TKey2 key2, TKey3 key3, out TValue value);
        private delegate bool TryGetDelegate<TKey1, TKey2, TKey3, TKey4, TValue>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, out TValue value);

        private static Either<TValue, TError> TryGetValueHelper<TValue, TError>(TryGetDelegate<TValue> tryGetValue, TError error)
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
        private static Either<TValue, TError> TryGetValueHelper<TKey, TValue, TError>(TryGetDelegate<TKey, TValue> tryGetValue, TKey key, TError error)
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
        private static Either<TValue, TError> TryGetValueHelper<TKey1, TKey2, TValue, TError>(TryGetDelegate<TKey1, TKey2, TValue> tryGetValue, TKey1 key1, TKey2 key2, TError error)
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
        private static Either<TValue, TError> TryGetValueHelper<TKey1, TKey2, TKey3, TValue, TError>(TryGetDelegate<TKey1, TKey2, TKey3, TValue> tryGetValue, TKey1 key1, TKey2 key2, TKey3 key3, TError error)
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
        private static Either<TValue, TError> TryGetValueHelper<TKey1, TKey2, TKey3, TKey4, TValue, TError>(
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
        #endregion TryGetHelpers

        public static Either<int, TError> TryParseInt<TError>(this string text, TError error) => TryGetValueHelper<string, int, TError>(int.TryParse, text, error);
        public static Either<int, TError> TryParseInt<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, int, TError>(int.TryParse, text, style, provider, error);
        public static Either<uint, TError> TryParseUInt<TError>(this string text, TError error) => TryGetValueHelper<string, uint, TError>(uint.TryParse, text, error);
        public static Either<uint, TError> TryParseUInt<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, uint, TError>(uint.TryParse, text, style, provider, error);
        public static Either<short, TError> TryParseShort<TError>(this string text, TError error) => TryGetValueHelper<string, short, TError>(short.TryParse, text, error);
        public static Either<short, TError> TryParseShort<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, short, TError>(short.TryParse, text, style, provider, error);
        public static Either<ushort, TError> TryParseUShort<TError>(this string text, TError error) => TryGetValueHelper<string, ushort, TError>(ushort.TryParse, text, error);
        public static Either<ushort, TError> TryParseUShort<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, ushort, TError>(ushort.TryParse, text, style, provider, error);
        public static Either<byte, TError> TryParseByte<TError>(this string text, TError error) => TryGetValueHelper<string, byte, TError>(byte.TryParse, text, error);
        public static Either<byte, TError> TryParseByte<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, byte, TError>(byte.TryParse, text, style, provider, error);
        public static Either<sbyte, TError> TryParseSByte<TError>(this string text, TError error) => TryGetValueHelper<string, sbyte, TError>(sbyte.TryParse, text, error);
        public static Either<sbyte, TError> TryParseSByte<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, sbyte, TError>(sbyte.TryParse, text, style, provider, error);
        public static Either<long, TError> TryParseLong<TError>(this string text, TError error) => TryGetValueHelper<string, long, TError>(long.TryParse, text, error);
        public static Either<long, TError> TryParseLong<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, long, TError>(long.TryParse, text, style, provider, error);
        public static Either<ulong, TError> TryParseULong<TError>(this string text, TError error) => TryGetValueHelper<string, ulong, TError>(ulong.TryParse, text, error);
        public static Either<ulong, TError> TryParseULong<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, ulong, TError>(ulong.TryParse, text, style, provider, error);
        public static Either<BigInteger, TError> TryParseBigInteger<TError>(this string text, TError error) => TryGetValueHelper<string, BigInteger, TError>(BigInteger.TryParse, text, error);
        public static Either<BigInteger, TError> TryParseBigInteger<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, BigInteger, TError>(BigInteger.TryParse, text, style, provider, error);

        public static Either<double, TError> TryParseDouble<TError>(this string text, TError error) => TryGetValueHelper<string, double, TError>(double.TryParse, text, error);
        public static Either<double, TError> TryParseDouble<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, double, TError>(double.TryParse, text, style, provider, error);
        public static Either<float, TError> TryParseFloat<TError>(this string text, TError error) => TryGetValueHelper<string, float, TError>(float.TryParse, text, error);
        public static Either<float, TError> TryParseFloat<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryGetValueHelper<string, NumberStyles, IFormatProvider, float, TError>(float.TryParse, text, style, provider, error);

        public static Either<DateTime, TError> TryParseDateTime<TError>(this string text, TError error) => TryGetValueHelper<string, DateTime, TError>(DateTime.TryParse, text, error);
        public static Either<DateTime, TError> TryParseDateTime<TError>(this string text, IFormatProvider provider, DateTimeStyles styles, TError error) => TryGetValueHelper<string, IFormatProvider, DateTimeStyles, DateTime, TError>(DateTime.TryParse, text, provider, styles, error);
        public static Either<DateTime, TError> TryParseDateTimeExact<TError>(this string text, string format, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetValueHelper<string, string, IFormatProvider, DateTimeStyles, DateTime, TError>(DateTime.TryParseExact, text, format, provider, style, error);
        public static Either<DateTime, TError> TryParseDateTimeExact<TError>(this string text, string[] formats, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetValueHelper<string, string[], IFormatProvider, DateTimeStyles, DateTime, TError>(DateTime.TryParseExact, text, formats, provider, style, error);

        public static Either<TimeSpan, TError> TryParseTimeSpan<TError>(this string text, TError error) => TryGetValueHelper<string, TimeSpan, TError>(TimeSpan.TryParse, text, error);
        public static Either<TimeSpan, TError> TryParseTimeSpan<TError>(this string text, IFormatProvider provider, TError error) => TryGetValueHelper<string, IFormatProvider, TimeSpan, TError>(TimeSpan.TryParse, text, provider, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string format, IFormatProvider provider, TError error) => TryGetValueHelper<string, string, IFormatProvider, TimeSpan, TError>(TimeSpan.TryParseExact, text, format, provider, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string[] formats, IFormatProvider provider, TError error) => TryGetValueHelper<string, string[], IFormatProvider, TimeSpan, TError>(TimeSpan.TryParseExact, text, formats, provider, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string format, IFormatProvider provider, TimeSpanStyles style, TError error) => TryGetValueHelper<string, string, IFormatProvider, TimeSpanStyles, TimeSpan, TError>(TimeSpan.TryParseExact, text, format, provider, style, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string[] formats, IFormatProvider provider, TimeSpanStyles style, TError error) => TryGetValueHelper<string, string[], IFormatProvider, TimeSpanStyles, TimeSpan, TError>(TimeSpan.TryParseExact, text, formats, provider, style, error);

        public static Either<DateTimeOffset, TError> TryParseDateTimeOffset<TError>(this string text, TError error) => TryGetValueHelper<string, DateTimeOffset, TError>(DateTimeOffset.TryParse, text, error);
        public static Either<DateTimeOffset, TError> TryParseDateTimeOffset<TError>(this string text, IFormatProvider provider, DateTimeStyles styles, TError error) => TryGetValueHelper<string, IFormatProvider, DateTimeStyles, DateTimeOffset, TError>(DateTimeOffset.TryParse, text, provider, styles, error);
        public static Either<DateTimeOffset, TError> TryParseDateTimeExactOffset<TError>(this string text, string format, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetValueHelper<string, string, IFormatProvider, DateTimeStyles, DateTimeOffset, TError>(DateTimeOffset.TryParseExact, text, format, provider, style, error);
        public static Either<DateTimeOffset, TError> TryParseDateTimeExactOffset<TError>(this string text, string[] formats, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetValueHelper<string, string[], IFormatProvider, DateTimeStyles, DateTimeOffset, TError>(DateTimeOffset.TryParseExact, text, formats, provider, style, error);

        public static Either<Guid, TError> TryParseGuid<TError>(this string text, TError error) => TryGetValueHelper<string, Guid, TError>(Guid.TryParse, text, error);
        public static Either<Guid, TError> TryParseGuidExact<TError>(this string text, string format, TError error) => TryGetValueHelper<string, string, Guid, TError>(Guid.TryParseExact, text, format, error);

        public static Either<TEnum, TError> TryParseEnum<TEnum, TError>(this string text, TError error) where TEnum : struct
        {
            if (Enum.TryParse<TEnum>(text, out var enumValue))
            {
                return enumValue;
            }
            else
            {
                return error;
            }
        }

        public static Either<TEnum, TError> TryParseEnum<TEnum, TError>(this string text, bool ignoreCase, TError error) where TEnum : struct
        {
            if (Enum.TryParse<TEnum>(text, ignoreCase, out var enumValue))
            {
                return enumValue;
            }
            else
            {
                return error;
            }
        }

        public static Either<TValue, TError> TryGetValue<TKey, TValue, TError>(this IDictionary<TKey, TValue> dictionary, TKey key, TError error)
        {
            return TryGetValueHelper<TKey, TValue, TError>(dictionary.TryGetValue, key, error);
        }

        public static Either<T, TError> TryGetValue<T, TError>(this IList<T> list, int index, TError error)
        {
            if (index < 0 || index >= list.Count)
            {
                return error;
            }
            else
            {
                return list[index];
            }
        }

        public static Either<object, TError> TryGetValue<TError>(this Array list, int index, TError error)
        {
            if (index < 0 || index >= list.Length)
            {
                return error;
            }
            else
            {
                return list.GetValue(index);
            }
        }

        public static Either<T, TError> TryGetValue<T, TError>(this Array list, int index, TError error)
        {
            if (index < 0 || index >= list.Length)
            {
                return error;
            }
            else
            {
                var value = list.GetValue(index);
                if (value is T actualValue)
                {
                    return actualValue;
                }
                return error;
            }
        }

    }
}
