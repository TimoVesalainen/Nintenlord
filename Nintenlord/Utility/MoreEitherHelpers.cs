using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Nintenlord.Utility
{
    public static class MoreEitherHelpers
    {
        public static Either<T, TException> TryCatch<T, TException>(this Func<T> function) where TException : Exception
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
        private delegate bool TryGetFromSpanDelegate<TValue>(ReadOnlySpan<char> text, out TValue value);
        private delegate bool TryGetFromSpanDelegate<TKey1, TValue>(ReadOnlySpan<char> text, TKey1 key1, out TValue value);
        private delegate bool TryGetFromSpanDelegate<TKey1, TKey2, TValue>(ReadOnlySpan<char> text, TKey1 key1, TKey2 key2, out TValue value);
        private delegate bool TryGetFromSpanDelegate<TKey1, TKey2, TKey3, TValue>(ReadOnlySpan<char> text, TKey1 key1, TKey2 key2, TKey3 key3, out TValue value);

        private static Either<TValue, TError> TryGetFromSpanValueHelper<TKey, TValue, TError>(TryGetFromSpanDelegate<TKey, TValue> tryGetValue, ReadOnlySpan<char> span, TKey key, TError error)
        {
            if (tryGetValue(span, key, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
        private static Either<TValue, TError> TryGetFromSpanValueHelper<TKey1, TKey2, TValue, TError>(TryGetFromSpanDelegate<TKey1, TKey2, TValue> tryGetValue, ReadOnlySpan<char> span, TKey1 key1, TKey2 key2, TError error)
        {
            if (tryGetValue(span, key1, key2, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }
        private static Either<TValue, TError> TryGetFromSpanValueHelper<TKey1, TKey2, TKey3, TValue, TError>(TryGetFromSpanDelegate<TKey1, TKey2, TKey3, TValue> tryGetValue, ReadOnlySpan<char> span, TKey1 key1, TKey2 key2, TKey3 key3, TError error)
        {
            if (tryGetValue(span, key1, key2, key3, out var value))
            {
                return value;
            }
            else
            {
                return error;
            }
        }

        #endregion TryGetHelpers

        public static Either<TNumber, TError> TryParseNumber<TNumber, TError>(this string text, NumberStyles style, IFormatProvider provider, TError error)
            where TNumber : INumberBase<TNumber>
            => TryGetHelpers.TryGetEither<string, NumberStyles, IFormatProvider, TNumber, TError>(TNumber.TryParse, text, style, provider, error);

        public static Either<TNumber, TError> TryParseNumber<TNumber, TError>(this ReadOnlySpan<char> text, IFormatProvider provider, TError error)
            where TNumber : ISpanParsable<TNumber> => TryGetFromSpanValueHelper<IFormatProvider, TNumber, TError>(TNumber.TryParse, text, provider, error);

        public static Either<TNumber, TError> TryParseNumber<TNumber, TError>(this ReadOnlySpan<char> text, NumberStyles style, IFormatProvider provider, TError error)
            where TNumber : INumberBase<TNumber> => TryGetFromSpanValueHelper<NumberStyles, IFormatProvider, TNumber, TError>(TNumber.TryParse, text, style, provider, error);

        public static Either<int, TError> TryParseInt<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, int, TError>(int.TryParse, text, error);
        public static Either<int, TError> TryParseInt<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<int, TError>(text, style, provider, error);
        public static Either<uint, TError> TryParseUInt<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, uint, TError>(uint.TryParse, text, error);
        public static Either<uint, TError> TryParseUInt<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<uint, TError>(text, style, provider, error);
        public static Either<short, TError> TryParseShort<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, short, TError>(short.TryParse, text, error);
        public static Either<short, TError> TryParseShort<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<short, TError>(text, style, provider, error);
        public static Either<ushort, TError> TryParseUShort<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, ushort, TError>(ushort.TryParse, text, error);
        public static Either<ushort, TError> TryParseUShort<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<ushort, TError>(text, style, provider, error);
        public static Either<byte, TError> TryParseByte<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, byte, TError>(byte.TryParse, text, error);
        public static Either<byte, TError> TryParseByte<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<byte, TError>(text, style, provider, error);
        public static Either<sbyte, TError> TryParseSByte<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, sbyte, TError>(sbyte.TryParse, text, error);
        public static Either<sbyte, TError> TryParseSByte<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<sbyte, TError>(text, style, provider, error);
        public static Either<long, TError> TryParseLong<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, long, TError>(long.TryParse, text, error);
        public static Either<long, TError> TryParseLong<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<long, TError>(text, style, provider, error);
        public static Either<ulong, TError> TryParseULong<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, ulong, TError>(ulong.TryParse, text, error);
        public static Either<ulong, TError> TryParseULong<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<ulong, TError>(text, style, provider, error);
        public static Either<BigInteger, TError> TryParseBigInteger<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, BigInteger, TError>(BigInteger.TryParse, text, error);
        public static Either<BigInteger, TError> TryParseBigInteger<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<BigInteger, TError>(text, style, provider, error);

        public static Either<double, TError> TryParseDouble<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, double, TError>(double.TryParse, text, error);
        public static Either<double, TError> TryParseDouble<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<double, TError>(text, style, provider, error);
        public static Either<float, TError> TryParseFloat<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, float, TError>(float.TryParse, text, error);
        public static Either<float, TError> TryParseFloat<TError>(this string text, NumberStyles style, IFormatProvider provider, TError error) => TryParseNumber<float, TError>(text, style, provider, error);

        public static Either<DateTime, TError> TryParseDateTime<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, DateTime, TError>(DateTime.TryParse, text, error);
        public static Either<DateTime, TError> TryParseDateTime<TError>(this string text, IFormatProvider provider, DateTimeStyles styles, TError error) => TryGetHelpers.TryGetEither<string, IFormatProvider, DateTimeStyles, DateTime, TError>(DateTime.TryParse, text, provider, styles, error);
        public static Either<DateTime, TError> TryParseDateTimeExact<TError>(this string text, string format, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetHelpers.TryGetEither<string, string, IFormatProvider, DateTimeStyles, DateTime, TError>(DateTime.TryParseExact, text, format, provider, style, error);
        public static Either<DateTime, TError> TryParseDateTimeExact<TError>(this string text, string[] formats, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetHelpers.TryGetEither<string, string[], IFormatProvider, DateTimeStyles, DateTime, TError>(DateTime.TryParseExact, text, formats, provider, style, error);

        public static Either<TimeSpan, TError> TryParseTimeSpan<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, TimeSpan, TError>(TimeSpan.TryParse, text, error);
        public static Either<TimeSpan, TError> TryParseTimeSpan<TError>(this string text, IFormatProvider provider, TError error) => TryGetHelpers.TryGetEither<string, IFormatProvider, TimeSpan, TError>(TimeSpan.TryParse, text, provider, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string format, IFormatProvider provider, TError error) => TryGetHelpers.TryGetEither<string, string, IFormatProvider, TimeSpan, TError>(TimeSpan.TryParseExact, text, format, provider, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string[] formats, IFormatProvider provider, TError error) => TryGetHelpers.TryGetEither<string, string[], IFormatProvider, TimeSpan, TError>(TimeSpan.TryParseExact, text, formats, provider, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string format, IFormatProvider provider, TimeSpanStyles style, TError error) => TryGetHelpers.TryGetEither<string, string, IFormatProvider, TimeSpanStyles, TimeSpan, TError>(TimeSpan.TryParseExact, text, format, provider, style, error);
        public static Either<TimeSpan, TError> TryParseTimeSpanExact<TError>(this string text, string[] formats, IFormatProvider provider, TimeSpanStyles style, TError error) => TryGetHelpers.TryGetEither<string, string[], IFormatProvider, TimeSpanStyles, TimeSpan, TError>(TimeSpan.TryParseExact, text, formats, provider, style, error);

        public static Either<DateTimeOffset, TError> TryParseDateTimeOffset<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, DateTimeOffset, TError>(DateTimeOffset.TryParse, text, error);
        public static Either<DateTimeOffset, TError> TryParseDateTimeOffset<TError>(this string text, IFormatProvider provider, DateTimeStyles styles, TError error) => TryGetHelpers.TryGetEither<string, IFormatProvider, DateTimeStyles, DateTimeOffset, TError>(DateTimeOffset.TryParse, text, provider, styles, error);
        public static Either<DateTimeOffset, TError> TryParseDateTimeExactOffset<TError>(this string text, string format, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetHelpers.TryGetEither<string, string, IFormatProvider, DateTimeStyles, DateTimeOffset, TError>(DateTimeOffset.TryParseExact, text, format, provider, style, error);
        public static Either<DateTimeOffset, TError> TryParseDateTimeExactOffset<TError>(this string text, string[] formats, IFormatProvider provider, DateTimeStyles style, TError error) => TryGetHelpers.TryGetEither<string, string[], IFormatProvider, DateTimeStyles, DateTimeOffset, TError>(DateTimeOffset.TryParseExact, text, formats, provider, style, error);

        public static Either<Guid, TError> TryParseGuid<TError>(this string text, TError error) => TryGetHelpers.TryGetEither<string, Guid, TError>(Guid.TryParse, text, error);
        public static Either<Guid, TError> TryParseGuidExact<TError>(this string text, string format, TError error) => TryGetHelpers.TryGetEither<string, string, Guid, TError>(Guid.TryParseExact, text, format, error);

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
            return TryGetHelpers.TryGetEither<TKey, TValue, TError>(dictionary.TryGetValue, key, error);
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
