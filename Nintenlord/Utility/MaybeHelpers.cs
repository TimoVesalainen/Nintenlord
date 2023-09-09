using Nintenlord.Collections;
using Nintenlord.Collections.Comparers;
using Nintenlord.Collections.Lists;
using System;
using System.Collections;
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

        public static bool TryGetValue<T>(this Maybe<T> maybeValue, out T value)
        {
            if (maybeValue.HasValue)
            {
                value = maybeValue.Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
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

        public static T GetValueOrThrow<T>(this Maybe<T> maybe, Exception exception = null)
        {
            if (maybe.HasValue)
            {
                return maybe.Value;
            }
            else
            {
                throw exception ?? throw new InvalidOperationException("No value in maybe");
            }
        }

        public static T GetValueOrThrow<T>(this Maybe<T> maybe, Func<Exception> throwFunc)
        {
            if (maybe.HasValue)
            {
                return maybe.Value;
            }
            else
            {
                throw throwFunc();
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
            return maybe.ToEither(Unit.Default);
        }

        public static Either<T, TOTher> ToEither<T, TOTher>(this Maybe<T> maybe, TOTher other)
        {
            if (maybe.HasValue)
            {
                return maybe.Value;
            }
            else
            {
                return other;
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
        public static Maybe<TOut> Zip<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Maybe<TIn3> maybe3, Maybe<TIn4> maybe4, Maybe<TIn5> maybe5,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue && maybe3.HasValue && maybe4.HasValue && maybe5.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value, maybe3.Value, maybe4.Value, maybe5.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }
        public static Maybe<TOut> Zip<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Maybe<TIn3> maybe3, Maybe<TIn4> maybe4, Maybe<TIn5> maybe5, Maybe<TIn6> maybe6,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue && maybe3.HasValue && maybe4.HasValue && maybe5.HasValue && maybe6.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value, maybe3.Value, maybe4.Value, maybe5.Value, maybe6.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }
        public static Maybe<TOut> Zip<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Maybe<TIn3> maybe3, Maybe<TIn4> maybe4, Maybe<TIn5> maybe5, Maybe<TIn6> maybe6, Maybe<TIn7> maybe7,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue && maybe3.HasValue && maybe4.HasValue && maybe5.HasValue && maybe6.HasValue && maybe7.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value, maybe3.Value, maybe4.Value, maybe5.Value, maybe6.Value, maybe7.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }

        public static Maybe<TOut> Zip<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(this Maybe<TIn1> maybe1, Maybe<TIn2> maybe2, Maybe<TIn3> maybe3, Maybe<TIn4> maybe4, Maybe<TIn5> maybe5, Maybe<TIn6> maybe6, Maybe<TIn7> maybe7, Maybe<TIn8> maybe8,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut> zipper)
        {
            if (maybe1.HasValue && maybe2.HasValue && maybe3.HasValue && maybe4.HasValue && maybe5.HasValue && maybe6.HasValue && maybe7.HasValue && maybe8.HasValue)
            {
                return zipper(maybe1.Value, maybe2.Value, maybe3.Value, maybe4.Value, maybe5.Value, maybe6.Value, maybe7.Value, maybe8.Value);
            }
            else
            {
                return Maybe<TOut>.Nothing;
            }
        }
        //TODO: More tuple helpers

        public static Maybe<TException> TryCatch<TException>(this Action function) where TException : Exception
        {
            try
            {
                function();
                return Maybe<TException>.Nothing;
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

        private static Maybe<TValue> TryGetFromSpanValueHelper<TKey, TValue>(TryGetFromSpanDelegate<TKey, TValue> tryGetValue, ReadOnlySpan<char> span, TKey key)
        {
            if (tryGetValue(span, key, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }
        private static Maybe<TValue> TryGetFromSpanValueHelper<TKey1, TKey2, TValue>(TryGetFromSpanDelegate<TKey1, TKey2, TValue> tryGetValue, ReadOnlySpan<char> span, TKey1 key1, TKey2 key2)
        {
            if (tryGetValue(span, key1, key2, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }
        private static Maybe<TValue> TryGetFromSpanValueHelper<TKey1, TKey2, TKey3, TValue>(TryGetFromSpanDelegate<TKey1, TKey2, TKey3, TValue> tryGetValue, ReadOnlySpan<char> span, TKey1 key1, TKey2 key2, TKey3 key3)
        {
            if (tryGetValue(span, key1, key2, key3, out var value))
            {
                return value;
            }
            else
            {
                return Maybe<TValue>.Nothing;
            }
        }

        #endregion TryGetHelpers

        public static Maybe<TNumber> TryParseNumber<TNumber>(this string text, NumberStyles style, IFormatProvider provider)
            where TNumber : INumberBase<TNumber>
            => TryGetValueHelper<string, NumberStyles, IFormatProvider, TNumber>(TNumber.TryParse, text, style, provider);

        public static Maybe<TNumber> TryParseNumber<TNumber>(this ReadOnlySpan<char> text, IFormatProvider provider)
            where TNumber : ISpanParsable<TNumber> => TryGetFromSpanValueHelper<IFormatProvider, TNumber>(TNumber.TryParse, text, provider);

        public static Maybe<TNumber> TryParseNumber<TNumber>(this ReadOnlySpan<char> text, NumberStyles style, IFormatProvider provider)
            where TNumber : INumberBase<TNumber> => TryGetFromSpanValueHelper<NumberStyles, IFormatProvider, TNumber>(TNumber.TryParse, text, style, provider);

        public static Maybe<int> TryParseInt(this string text) => TryGetValueHelper<string, int>(int.TryParse, text);
        public static Maybe<int> TryParseInt(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<int>(text, style, provider);
        public static Maybe<uint> TryParseUInt(this string text) => TryGetValueHelper<string, uint>(uint.TryParse, text);
        public static Maybe<uint> TryParseUInt(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<uint>(text, style, provider);
        public static Maybe<short> TryParseShort(this string text) => TryGetValueHelper<string, short>(short.TryParse, text);
        public static Maybe<short> TryParseShort(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<short>(text, style, provider);
        public static Maybe<ushort> TryParseUShort(this string text) => TryGetValueHelper<string, ushort>(ushort.TryParse, text);
        public static Maybe<ushort> TryParseUShort(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<ushort>(text, style, provider);
        public static Maybe<byte> TryParseByte(this string text) => TryGetValueHelper<string, byte>(byte.TryParse, text);
        public static Maybe<byte> TryParseByte(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<byte>(text, style, provider);
        public static Maybe<sbyte> TryParseSByte(this string text) => TryGetValueHelper<string, sbyte>(sbyte.TryParse, text);
        public static Maybe<sbyte> TryParseSByte(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<sbyte>(text, style, provider);
        public static Maybe<long> TryParseLong(this string text) => TryGetValueHelper<string, long>(long.TryParse, text);
        public static Maybe<long> TryParseLong(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<long>(text, style, provider);
        public static Maybe<ulong> TryParseULong(this string text) => TryGetValueHelper<string, ulong>(ulong.TryParse, text);
        public static Maybe<ulong> TryParseULong(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<ulong>(text, style, provider);
        public static Maybe<BigInteger> TryParseBigInteger(this string text) => TryGetValueHelper<string, BigInteger>(BigInteger.TryParse, text);
        public static Maybe<BigInteger> TryParseBigInteger(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<BigInteger>(text, style, provider);

        public static Maybe<double> TryParseDouble(this string text) => TryGetValueHelper<string, double>(double.TryParse, text);
        public static Maybe<double> TryParseDouble(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<double>(text, style, provider);
        public static Maybe<float> TryParseFloat(this string text) => TryGetValueHelper<string, float>(float.TryParse, text);
        public static Maybe<float> TryParseFloat(this string text, NumberStyles style, IFormatProvider provider) => TryParseNumber<float>(text, style, provider);

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

        public static Maybe<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            return TryGetValueHelper<TKey, TValue>(dictionary.TryGetValue, key);
        }

        public static Maybe<T> TryGetValue<T>(this IReadOnlyList<T> list, int index)
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

        public static Maybe<object> TryGetValue(this IList list, int index)
        {
            if (index < 0 || index >= list.Count)
            {
                return Maybe<object>.Nothing;
            }
            else
            {
                return list[index];
            }
        }

        public static Maybe<T> TryGetValue<T>(this IList list, int index)
        {
            if (index < 0 || index >= list.Count)
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                var value = list[index];
                if (value is T actualValue)
                {
                    return actualValue;
                }
                return Maybe<T>.Nothing;
            }
        }

        public static Maybe<T> FirstSafe<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.Select(Maybe<T>.Just).FirstOrDefault() ?? Maybe<T>.Nothing;
        }

        public static Maybe<T> FirstSafe<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return enumerable.Where(predicate).Select(Maybe<T>.Just).FirstOrDefault() ?? Maybe<T>.Nothing;
        }

        public static Maybe<T> LastSafe<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (enumerable is IReadOnlyList<T> list)
            {
                if (list.Count > 0)
                {
                    return list[list.Count - 1];
                }
                else
                {
                    return Maybe<T>.Nothing;
                }
            }

            if (enumerable is IList<T> list2)
            {
                if (list2.Count > 0)
                {
                    return list2[list2.Count - 1];
                }
                else
                {
                    return Maybe<T>.Nothing;
                }
            }

            //Is done like this so not to create Maybe.Just from every intermediate value
            bool hasValue = false;
            T latest = default;
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    hasValue = true;
                    latest = enumerator.Current;
                }
            }

            if (hasValue)
            {
                return latest;
            }
            else
            {
                return Maybe<T>.Nothing;
            }
        }

        public static Maybe<T> LastSafe<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (enumerable is IReadOnlyList<T> list)
            {
                if (list.Count > 0)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list[i]))
                        {
                            return list[i];
                        }
                    }
                }

                return Maybe<T>.Nothing;
            }

            if (enumerable is IList<T> list2)
            {
                if (list2.Count > 0)
                {
                    for (int i = list2.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list2[i]))
                        {
                            return list2[i];
                        }
                    }
                }

                return Maybe<T>.Nothing;
            }

            //Is done like this so not to create Maybe.Just from every intermediate value
            bool hasValue = false;
            T latest = default;
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        hasValue = true;
                        latest = enumerator.Current;
                    }
                }
            }

            if (hasValue)
            {
                return latest;
            }
            else
            {
                return Maybe<T>.Nothing;
            }
        }

        public static Maybe<IEnumerable<T>> NonEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.Any())
            {
                return Maybe<IEnumerable<T>>.Just(enumerable);
            }
            else
            {
                return Maybe<IEnumerable<T>>.Nothing;
            }
        }

        public static Maybe<T> MinSafe<T>(this IEnumerable<T> collection, IComparer<T> comp = null)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comp ??= Comparer<T>.Default;

            return collection.MinScan(comp).LastSafe();
        }

        public static Maybe<T> MinBySafe<T>(this IEnumerable<T> collection, Func<T, float> comp)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comp is null)
            {
                throw new ArgumentNullException(nameof(comp));
            }

            return collection.MinSafe(new SelectComparer<T, float>(comp));
        }

        public static Maybe<T> MaxSafe<T>(this IEnumerable<T> collection, IComparer<T> comp = null)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comp ??= Comparer<T>.Default;

            return collection.MaxScan(comp).LastSafe();
        }

        public static Maybe<T> MaxBySafe<T>(this IEnumerable<T> collection, Func<T, float> comp)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comp is null)
            {
                throw new ArgumentNullException(nameof(comp));
            }

            return collection.MaxSafe(new SelectComparer<T, float>(comp));
        }
    }
}
