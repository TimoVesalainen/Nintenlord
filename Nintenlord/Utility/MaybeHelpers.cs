using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reactive;

namespace Nintenlord.Utility
{
    public static class MaybeHelpers
    {
        public static Maybe<TOut> Select<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, TOut> selector)
        {
            if (maybe.HasValue)
            {
                return selector(maybe.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }

        public static Maybe<TOut> SelectMany<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, Maybe<TOut>> selector)
        {
            if (maybe.HasValue)
            {
                return selector(maybe.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }

        public static Maybe<TOut> SelectMany<TIn, TIntermediate, TOut>(this Maybe<TIn> maybe, Func<TIn, Maybe<TIntermediate>> selector, Func<TIn, TIntermediate, TOut> resultSelector)
        {
            if (maybe.HasValue)
            {
                var intermediate = selector(maybe.Value);
                if (intermediate.HasValue)
                {
                    return resultSelector(maybe.Value, intermediate.Value);
                }
            }
            return Maybe<TOut>.Nothing;
        }

        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
        {
            if (maybe.HasValue && predicate(maybe.Value))
            {
                return maybe;
            }
            else
            {
                return Maybe<T>.Nothing;
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
            {
                yield return maybe.Value;
            }
        }

        public static IEnumerable<T> GetValues<T>(this IEnumerable<Maybe<T>> maybes)
        {
            return maybes.SelectMany(ToEnumerable);
        }

        public static T GetValueOrDefault<T>(this Maybe<T> maybe, T defaultValue = default)
        {
            if (maybe.HasValue)
            {
                return maybe.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        
        public static Maybe<T> Concat<T>(this Maybe<T> maybe, Maybe<T> maybeOther)
        {
            if (maybe.HasValue)
            {
                return maybe;
            }
            else
            {
                return maybeOther;
            }
        }

        public static Maybe<T> Concat<T>(this IEnumerable<Maybe<T>> maybes)
        {
            return maybes.Aggregate(Maybe<T>.Nothing, Concat);
        }

        public static Maybe<T> Concat<T>(params Maybe<T>[] maybes)
        {
            return Concat((IEnumerable<Maybe<T>>)maybes);
        }

        public static Either<T, Unit> ToEither<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
            {
                return maybe.Value;
            }
            else
            {
                return Unit.Default;
            }
        }

        public static Maybe<T> ToMaybe<T>(this Either<T, Unit> either)
        {
            return either.Apply(x => x, _ => Maybe<T>.Nothing);
        }

        public static Maybe<TOut> Zip<TIn1, TIn2, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Func<TIn1, TIn2, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }

        public static Maybe<TOut> Zip<TIn1, TIn2, TIn3, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Maybe<TIn3> maybe3, Func<TIn1, TIn2, TIn3, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue && maybe3.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value, maybe3.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }

        public static Maybe<TOut> Zip<TIn1, TIn2, TIn3, TIn4, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Maybe<TIn3> maybe3, Maybe<TIn4> maybe4, Func<TIn1, TIn2, TIn3, TIn4, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue && maybe3.HasValue && maybe4.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value, maybe3.Value, maybe4.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }
        //TODO: More tuple helpers

        #region TryGetHelpers

        private delegate bool TryGetDelegate<TValue>(out TValue value);
        private delegate bool TryGetDelegate<TKey, TValue>(TKey key, out TValue value);
        private delegate bool TryGetDelegate<TKey1, TKey2, TValue>(TKey1 key1, TKey2 key2, out TValue value);
        private delegate bool TryGetDelegate<TKey1, TKey2, TKey3, TValue>(TKey1 key1, TKey2 key2, TKey3 key3, out TValue value);
        private delegate bool TryGetDelegate<TKey1, TKey2, TKey3, TKey4, TValue>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, out TValue value);

        private static Maybe<TValue> TryGetValueHelper<TValue>(TryGetDelegate<TValue> tryGetValue)
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
        private static Maybe<TValue> TryGetValueHelper<TKey, TValue>(TryGetDelegate<TKey, TValue> tryGetValue, TKey key)
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
        private static Maybe<TValue> TryGetValueHelper<TKey1, TKey2, TValue>(TryGetDelegate<TKey1, TKey2, TValue> tryGetValue, TKey1 key1, TKey2 key2)
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
        private static Maybe<TValue> TryGetValueHelper<TKey1, TKey2, TKey3, TValue>(TryGetDelegate<TKey1, TKey2, TKey3, TValue> tryGetValue, TKey1 key1, TKey2 key2, TKey3 key3)
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
        private static Maybe<TValue> TryGetValueHelper<TKey1, TKey2, TKey3, TKey4, TValue>(
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
        #endregion TryGetHelpers

        public static Maybe<int> TryParseInt(this string text) => TryGetValueHelper<string, int>(int.TryParse, text);
        public static Maybe<int> TryParseInt(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, int>(int.TryParse, text, style, provider);
        public static Maybe<uint> TryParseUInt(this string text) => TryGetValueHelper<string, uint>(uint.TryParse, text);
        public static Maybe<uint> TryParseUInt(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, uint>(uint.TryParse, text, style, provider);
        public static Maybe<short> TryParseShort(this string text) => TryGetValueHelper<string, short>(short.TryParse, text);
        public static Maybe<short> TryParseShort(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, short>(short.TryParse, text, style, provider);
        public static Maybe<ushort> TryParseUShort(this string text) => TryGetValueHelper<string, ushort>(ushort.TryParse, text);
        public static Maybe<ushort> TryParseUShort(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, ushort>(ushort.TryParse, text, style, provider);
        public static Maybe<byte> TryParseByte(this string text) => TryGetValueHelper<string, byte>(byte.TryParse, text);
        public static Maybe<byte> TryParseByte(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, byte>(byte.TryParse, text, style, provider);
        public static Maybe<sbyte> TryParseSByte(this string text) => TryGetValueHelper<string, sbyte>(sbyte.TryParse, text);
        public static Maybe<sbyte> TryParseSByte(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, sbyte>(sbyte.TryParse, text, style, provider);
        public static Maybe<long> TryParseLong(this string text) => TryGetValueHelper<string, long>(long.TryParse, text);
        public static Maybe<long> TryParseLong(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, long>(long.TryParse, text, style, provider);
        public static Maybe<ulong> TryParseULong(this string text) => TryGetValueHelper<string, ulong>(ulong.TryParse, text);
        public static Maybe<ulong> TryParseULong(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, ulong>(ulong.TryParse, text, style, provider);
        public static Maybe<BigInteger> TryParseBigInteger(this string text) => TryGetValueHelper<string, BigInteger>(BigInteger.TryParse, text);
        public static Maybe<BigInteger> TryParseBigInteger(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, BigInteger>(BigInteger.TryParse, text, style, provider);

        public static Maybe<double> TryParseDouble(this string text) => TryGetValueHelper<string, double>(double.TryParse, text);
        public static Maybe<double> TryParseDouble(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, double>(double.TryParse, text, style, provider);
        public static Maybe<float> TryParseFloat(this string text) => TryGetValueHelper<string, float>(float.TryParse, text);
        public static Maybe<float> TryParseFloat(this string text, NumberStyles style, IFormatProvider provider) => TryGetValueHelper<string, NumberStyles, IFormatProvider, float>(float.TryParse, text, style, provider);

        public static Maybe<DateTime> TryParseDateTime(this string text) => TryGetValueHelper<string, DateTime>(DateTime.TryParse, text);
        public static Maybe<DateTime> TryParseDateTime(this string text, IFormatProvider provider, DateTimeStyles styles) => TryGetValueHelper<string, IFormatProvider, DateTimeStyles, DateTime>(DateTime.TryParse, text, provider, styles);
        public static Maybe<DateTime> TryParseDateTimeExact(this string text, string format, IFormatProvider provider, DateTimeStyles style) => TryGetValueHelper<string, string, IFormatProvider, DateTimeStyles, DateTime>(DateTime.TryParseExact, text, format, provider, style);
        public static Maybe<DateTime> TryParseDateTimeExact(this string text, string[] formats, IFormatProvider provider, DateTimeStyles style) => TryGetValueHelper<string, string[], IFormatProvider, DateTimeStyles, DateTime>(DateTime.TryParseExact, text, formats, provider, style);

        public static Maybe<TimeSpan> TryParseTimeSpan(this string text) => TryGetValueHelper<string, TimeSpan>(TimeSpan.TryParse, text);
        public static Maybe<TimeSpan> TryParseTimeSpan(this string text, IFormatProvider provider) => TryGetValueHelper<string, IFormatProvider, TimeSpan>(TimeSpan.TryParse, text, provider);
        public static Maybe<TimeSpan> TryParseTimeSpanExact(this string text, string format, IFormatProvider provider) => TryGetValueHelper<string, string, IFormatProvider, TimeSpan>(TimeSpan.TryParseExact, text, format, provider);
        public static Maybe<TimeSpan> TryParseTimeSpanExact(this string text, string[] formats, IFormatProvider provider) => TryGetValueHelper<string, string[], IFormatProvider, TimeSpan>(TimeSpan.TryParseExact, text, formats, provider);
        public static Maybe<TimeSpan> TryParseTimeSpanExact(this string text, string format, IFormatProvider provider, TimeSpanStyles style) => TryGetValueHelper<string, string, IFormatProvider, TimeSpanStyles, TimeSpan>(TimeSpan.TryParseExact, text, format, provider, style);
        public static Maybe<TimeSpan> TryParseTimeSpanExact(this string text, string[] formats, IFormatProvider provider, TimeSpanStyles style) => TryGetValueHelper<string, string[], IFormatProvider, TimeSpanStyles, TimeSpan>(TimeSpan.TryParseExact, text, formats, provider, style);

        public static Maybe<DateTimeOffset> TryParseDateTimeOffset(this string text) => TryGetValueHelper<string, DateTimeOffset>(DateTimeOffset.TryParse, text);
        public static Maybe<DateTimeOffset> TryParseDateTimeOffset(this string text, IFormatProvider provider, DateTimeStyles styles) => TryGetValueHelper<string, IFormatProvider, DateTimeStyles, DateTimeOffset>(DateTimeOffset.TryParse, text, provider, styles);
        public static Maybe<DateTimeOffset> TryParseDateTimeExactOffset(this string text, string format, IFormatProvider provider, DateTimeStyles style) => TryGetValueHelper<string, string, IFormatProvider, DateTimeStyles, DateTimeOffset>(DateTimeOffset.TryParseExact, text, format, provider, style);
        public static Maybe<DateTimeOffset> TryParseDateTimeExactOffset(this string text, string[] formats, IFormatProvider provider, DateTimeStyles style) => TryGetValueHelper<string, string[], IFormatProvider, DateTimeStyles, DateTimeOffset>(DateTimeOffset.TryParseExact, text, formats, provider, style);

        public static Maybe<Guid> TryParseGuid(this string text) => TryGetValueHelper<string, Guid>(Guid.TryParse, text);
        public static Maybe<Guid> TryParseGuidExact(this string text, string format) => TryGetValueHelper<string, string, Guid>(Guid.TryParseExact, text, format);

        public static Maybe<TEnum> TryParseEnum<TEnum>(this string text) where TEnum : struct
        {
            if (Enum.TryParse<TEnum>(text, out var enumValue))
            {
                return enumValue;
            }
            else
            {
                return Maybe<TEnum>.Nothing;
            }
        }

        public static Maybe<TEnum> TryParseEnum<TEnum>(this string text, bool ignoreCase) where TEnum : struct
        {
            if (Enum.TryParse<TEnum>(text, ignoreCase, out var enumValue))
            {
                return enumValue;
            }
            else
            {
                return Maybe<TEnum>.Nothing;
            }
        }

        public static Maybe<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return TryGetValueHelper<TKey, TValue>(dictionary.TryGetValue, key);
        }

        public static Maybe<T> TryGetValue<T>(this IList<T> list, int index)
        {
            if (index < 0 || index >= list.Count)
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return list[index];
            }
        }

        public static Maybe<object> TryGetValue(this Array list, int index)
        {
            if (index < 0 || index >= list.Length)
            {
                return Maybe<object>.Nothing;
            }
            else
            {
                return list.GetValue(index);
            }
        }

        public static Maybe<T> TryGetValue<T>(this Array list, int index)
        {
            if (index < 0 || index >= list.Length)
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                var value = list.GetValue(index);
                if (value is T actualValue)
                {
                    return actualValue;
                }
                return Maybe<T>.Nothing;
            }
        }
    }
}
