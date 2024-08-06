using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections
{
    public static partial class EnumerableExtensions
    {
        public static (T0, T1) Aggregate<T0, T1, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }

            var seed = (seed0, seed1);

            (T0, T1) Accumalator((T0, T1) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T)> GetSequential2s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;

                yield return (n0, n1);
                while (enumerator.MoveNext())
                {
                    (n0, n1) = (n1, enumerator.Current);
                    yield return (n0, n1);
                }
            }

            return Inner();
        }

        public static (T, T) GetFirst2<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            return (item0, item1);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential2s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T)> GetPartitions2s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential2s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 2) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, Func<T0, T1, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, Func<Maybe<T0>, Maybe<T1>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1)
            {
                return zipper(item0, item1);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t);
            }

            return enum0.ZipLong(enum1, ZipBoth, ZipT0, ZipT1);
        }

        public static (T0, T1, T2) Aggregate<T0, T1, T2, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1, T2 seed2,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1, Func<T2, TSource, T2> func2)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }
            if (func2 is null)
            {
                throw new ArgumentNullException(nameof(func2));
            }

            var seed = (seed0, seed1, seed2);

            (T0, T1, T2) Accumalator((T0, T1, T2) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item), func2(accum.Item3, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T, T)> GetSequential3s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n2 = enumerator.Current;

                yield return (n0, n1, n2);
                while (enumerator.MoveNext())
                {
                    (n0, n1, n2) = (n1, n2, enumerator.Current);
                    yield return (n0, n1, n2);
                }
            }

            return Inner();
        }

        public static (T, T, T) GetFirst3<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item2 = enumerator.Current;
            return (item0, item1, item2);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential3s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T)> GetPartitions3s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential3s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 3) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
        }

        public static IEnumerable<TOut> Zip<T0, T1, T2, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, Func<T0, T1, T2, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }

            IEnumerable<TOut> ZipInner()
            {
                using var enume0 = enum0.GetEnumerator();
                using var enume1 = enum1.GetEnumerator();
                using var enume2 = enum2.GetEnumerator();
                while (enume0.MoveNext() && enume1.MoveNext() && enume2.MoveNext())
                {
                    yield return zipper(enume0.Current, enume1.Current, enume2.Current);
                }
            }

            return ZipInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, Func<T0, T1, T2, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1, Func<T2, TOut> zipper2)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }
            if (zipper2 is null)
            {
                throw new ArgumentNullException(nameof(zipper2));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;
                using var enum2ator = enum2.GetEnumerator();
                bool hasItemInenum2;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()) & (hasItemInenum2 = enum2ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current, enum2ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else

                if (hasItemInenum2)
                {
                    do
                    {
                        yield return zipper2(enum2ator.Current);
                    }
                    while (enum2ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, Func<Maybe<T0>, Maybe<T1>, Maybe<T2>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1, T2 item2)
            {
                return zipper(item0, item1, item2);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing, Maybe<T2>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t, Maybe<T2>.Nothing);
            }

            TOut ZipT2(T2 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, t);
            }

            return enum0.ZipLong(enum1, enum2, ZipBoth, ZipT0, ZipT1, ZipT2);
        }

        public static (T0, T1, T2, T3) Aggregate<T0, T1, T2, T3, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1, T2 seed2, T3 seed3,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1, Func<T2, TSource, T2> func2, Func<T3, TSource, T3> func3)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }
            if (func2 is null)
            {
                throw new ArgumentNullException(nameof(func2));
            }
            if (func3 is null)
            {
                throw new ArgumentNullException(nameof(func3));
            }

            var seed = (seed0, seed1, seed2, seed3);

            (T0, T1, T2, T3) Accumalator((T0, T1, T2, T3) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item), func2(accum.Item3, item), func3(accum.Item4, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T, T, T)> GetSequential4s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T, T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n2 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n3 = enumerator.Current;

                yield return (n0, n1, n2, n3);
                while (enumerator.MoveNext())
                {
                    (n0, n1, n2, n3) = (n1, n2, n3, enumerator.Current);
                    yield return (n0, n1, n2, n3);
                }
            }

            return Inner();
        }

        public static (T, T, T, T) GetFirst4<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item3 = enumerator.Current;
            return (item0, item1, item2, item3);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential4s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T)> GetPartitions4s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential4s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 4) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
        }

        public static IEnumerable<TOut> Zip<T0, T1, T2, T3, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, Func<T0, T1, T2, T3, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }

            IEnumerable<TOut> ZipInner()
            {
                using var enume0 = enum0.GetEnumerator();
                using var enume1 = enum1.GetEnumerator();
                using var enume2 = enum2.GetEnumerator();
                using var enume3 = enum3.GetEnumerator();
                while (enume0.MoveNext() && enume1.MoveNext() && enume2.MoveNext() && enume3.MoveNext())
                {
                    yield return zipper(enume0.Current, enume1.Current, enume2.Current, enume3.Current);
                }
            }

            return ZipInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, Func<T0, T1, T2, T3, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1, Func<T2, TOut> zipper2, Func<T3, TOut> zipper3)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }
            if (zipper2 is null)
            {
                throw new ArgumentNullException(nameof(zipper2));
            }
            if (zipper3 is null)
            {
                throw new ArgumentNullException(nameof(zipper3));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;
                using var enum2ator = enum2.GetEnumerator();
                bool hasItemInenum2;
                using var enum3ator = enum3.GetEnumerator();
                bool hasItemInenum3;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()) & (hasItemInenum2 = enum2ator.MoveNext()) & (hasItemInenum3 = enum3ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current, enum2ator.Current, enum3ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else

                if (hasItemInenum2)
                {
                    do
                    {
                        yield return zipper2(enum2ator.Current);
                    }
                    while (enum2ator.MoveNext());
                }
                else

                if (hasItemInenum3)
                {
                    do
                    {
                        yield return zipper3(enum3ator.Current);
                    }
                    while (enum3ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, Func<Maybe<T0>, Maybe<T1>, Maybe<T2>, Maybe<T3>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1, T2 item2, T3 item3)
            {
                return zipper(item0, item1, item2, item3);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t, Maybe<T2>.Nothing, Maybe<T3>.Nothing);
            }

            TOut ZipT2(T2 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, t, Maybe<T3>.Nothing);
            }

            TOut ZipT3(T3 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, t);
            }

            return enum0.ZipLong(enum1, enum2, enum3, ZipBoth, ZipT0, ZipT1, ZipT2, ZipT3);
        }

        public static (T0, T1, T2, T3, T4) Aggregate<T0, T1, T2, T3, T4, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1, T2 seed2, T3 seed3, T4 seed4,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1, Func<T2, TSource, T2> func2, Func<T3, TSource, T3> func3, Func<T4, TSource, T4> func4)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }
            if (func2 is null)
            {
                throw new ArgumentNullException(nameof(func2));
            }
            if (func3 is null)
            {
                throw new ArgumentNullException(nameof(func3));
            }
            if (func4 is null)
            {
                throw new ArgumentNullException(nameof(func4));
            }

            var seed = (seed0, seed1, seed2, seed3, seed4);

            (T0, T1, T2, T3, T4) Accumalator((T0, T1, T2, T3, T4) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item), func2(accum.Item3, item), func3(accum.Item4, item), func4(accum.Item5, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T, T, T, T)> GetSequential5s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T, T, T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n2 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n3 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n4 = enumerator.Current;

                yield return (n0, n1, n2, n3, n4);
                while (enumerator.MoveNext())
                {
                    (n0, n1, n2, n3, n4) = (n1, n2, n3, n4, enumerator.Current);
                    yield return (n0, n1, n2, n3, n4);
                }
            }

            return Inner();
        }

        public static (T, T, T, T, T) GetFirst5<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item3 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item4 = enumerator.Current;
            return (item0, item1, item2, item3, item4);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential5s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T)> GetPartitions5s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential5s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 5) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T, T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
        }

        public static IEnumerable<TOut> Zip<T0, T1, T2, T3, T4, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, Func<T0, T1, T2, T3, T4, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }

            IEnumerable<TOut> ZipInner()
            {
                using var enume0 = enum0.GetEnumerator();
                using var enume1 = enum1.GetEnumerator();
                using var enume2 = enum2.GetEnumerator();
                using var enume3 = enum3.GetEnumerator();
                using var enume4 = enum4.GetEnumerator();
                while (enume0.MoveNext() && enume1.MoveNext() && enume2.MoveNext() && enume3.MoveNext() && enume4.MoveNext())
                {
                    yield return zipper(enume0.Current, enume1.Current, enume2.Current, enume3.Current, enume4.Current);
                }
            }

            return ZipInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, Func<T0, T1, T2, T3, T4, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1, Func<T2, TOut> zipper2, Func<T3, TOut> zipper3, Func<T4, TOut> zipper4)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }
            if (zipper2 is null)
            {
                throw new ArgumentNullException(nameof(zipper2));
            }
            if (zipper3 is null)
            {
                throw new ArgumentNullException(nameof(zipper3));
            }
            if (zipper4 is null)
            {
                throw new ArgumentNullException(nameof(zipper4));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;
                using var enum2ator = enum2.GetEnumerator();
                bool hasItemInenum2;
                using var enum3ator = enum3.GetEnumerator();
                bool hasItemInenum3;
                using var enum4ator = enum4.GetEnumerator();
                bool hasItemInenum4;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()) & (hasItemInenum2 = enum2ator.MoveNext()) & (hasItemInenum3 = enum3ator.MoveNext()) & (hasItemInenum4 = enum4ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current, enum2ator.Current, enum3ator.Current, enum4ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else

                if (hasItemInenum2)
                {
                    do
                    {
                        yield return zipper2(enum2ator.Current);
                    }
                    while (enum2ator.MoveNext());
                }
                else

                if (hasItemInenum3)
                {
                    do
                    {
                        yield return zipper3(enum3ator.Current);
                    }
                    while (enum3ator.MoveNext());
                }
                else

                if (hasItemInenum4)
                {
                    do
                    {
                        yield return zipper4(enum4ator.Current);
                    }
                    while (enum4ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, Func<Maybe<T0>, Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1, T2 item2, T3 item3, T4 item4)
            {
                return zipper(item0, item1, item2, item3, item4);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing);
            }

            TOut ZipT2(T2 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, t, Maybe<T3>.Nothing, Maybe<T4>.Nothing);
            }

            TOut ZipT3(T3 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, t, Maybe<T4>.Nothing);
            }

            TOut ZipT4(T4 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, t);
            }

            return enum0.ZipLong(enum1, enum2, enum3, enum4, ZipBoth, ZipT0, ZipT1, ZipT2, ZipT3, ZipT4);
        }

        public static (T0, T1, T2, T3, T4, T5) Aggregate<T0, T1, T2, T3, T4, T5, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1, T2 seed2, T3 seed3, T4 seed4, T5 seed5,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1, Func<T2, TSource, T2> func2, Func<T3, TSource, T3> func3, Func<T4, TSource, T4> func4, Func<T5, TSource, T5> func5)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }
            if (func2 is null)
            {
                throw new ArgumentNullException(nameof(func2));
            }
            if (func3 is null)
            {
                throw new ArgumentNullException(nameof(func3));
            }
            if (func4 is null)
            {
                throw new ArgumentNullException(nameof(func4));
            }
            if (func5 is null)
            {
                throw new ArgumentNullException(nameof(func5));
            }

            var seed = (seed0, seed1, seed2, seed3, seed4, seed5);

            (T0, T1, T2, T3, T4, T5) Accumalator((T0, T1, T2, T3, T4, T5) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item), func2(accum.Item3, item), func3(accum.Item4, item), func4(accum.Item5, item), func5(accum.Item6, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T, T, T, T, T)> GetSequential6s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T, T, T, T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n2 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n3 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n4 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n5 = enumerator.Current;

                yield return (n0, n1, n2, n3, n4, n5);
                while (enumerator.MoveNext())
                {
                    (n0, n1, n2, n3, n4, n5) = (n1, n2, n3, n4, n5, enumerator.Current);
                    yield return (n0, n1, n2, n3, n4, n5);
                }
            }

            return Inner();
        }

        public static (T, T, T, T, T, T) GetFirst6<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item3 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item4 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item5 = enumerator.Current;
            return (item0, item1, item2, item3, item4, item5);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential6s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T, T)> GetPartitions6s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential6s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 6) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T, T, T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
        }

        public static IEnumerable<TOut> Zip<T0, T1, T2, T3, T4, T5, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, Func<T0, T1, T2, T3, T4, T5, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }

            IEnumerable<TOut> ZipInner()
            {
                using var enume0 = enum0.GetEnumerator();
                using var enume1 = enum1.GetEnumerator();
                using var enume2 = enum2.GetEnumerator();
                using var enume3 = enum3.GetEnumerator();
                using var enume4 = enum4.GetEnumerator();
                using var enume5 = enum5.GetEnumerator();
                while (enume0.MoveNext() && enume1.MoveNext() && enume2.MoveNext() && enume3.MoveNext() && enume4.MoveNext() && enume5.MoveNext())
                {
                    yield return zipper(enume0.Current, enume1.Current, enume2.Current, enume3.Current, enume4.Current, enume5.Current);
                }
            }

            return ZipInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, T5, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, Func<T0, T1, T2, T3, T4, T5, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1, Func<T2, TOut> zipper2, Func<T3, TOut> zipper3, Func<T4, TOut> zipper4, Func<T5, TOut> zipper5)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }
            if (zipper2 is null)
            {
                throw new ArgumentNullException(nameof(zipper2));
            }
            if (zipper3 is null)
            {
                throw new ArgumentNullException(nameof(zipper3));
            }
            if (zipper4 is null)
            {
                throw new ArgumentNullException(nameof(zipper4));
            }
            if (zipper5 is null)
            {
                throw new ArgumentNullException(nameof(zipper5));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;
                using var enum2ator = enum2.GetEnumerator();
                bool hasItemInenum2;
                using var enum3ator = enum3.GetEnumerator();
                bool hasItemInenum3;
                using var enum4ator = enum4.GetEnumerator();
                bool hasItemInenum4;
                using var enum5ator = enum5.GetEnumerator();
                bool hasItemInenum5;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()) & (hasItemInenum2 = enum2ator.MoveNext()) & (hasItemInenum3 = enum3ator.MoveNext()) & (hasItemInenum4 = enum4ator.MoveNext()) & (hasItemInenum5 = enum5ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current, enum2ator.Current, enum3ator.Current, enum4ator.Current, enum5ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else

                if (hasItemInenum2)
                {
                    do
                    {
                        yield return zipper2(enum2ator.Current);
                    }
                    while (enum2ator.MoveNext());
                }
                else

                if (hasItemInenum3)
                {
                    do
                    {
                        yield return zipper3(enum3ator.Current);
                    }
                    while (enum3ator.MoveNext());
                }
                else

                if (hasItemInenum4)
                {
                    do
                    {
                        yield return zipper4(enum4ator.Current);
                    }
                    while (enum4ator.MoveNext());
                }
                else

                if (hasItemInenum5)
                {
                    do
                    {
                        yield return zipper5(enum5ator.Current);
                    }
                    while (enum5ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, T5, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, Func<Maybe<T0>, Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
            {
                return zipper(item0, item1, item2, item3, item4, item5);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing);
            }

            TOut ZipT2(T2 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, t, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing);
            }

            TOut ZipT3(T3 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, t, Maybe<T4>.Nothing, Maybe<T5>.Nothing);
            }

            TOut ZipT4(T4 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, t, Maybe<T5>.Nothing);
            }

            TOut ZipT5(T5 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, t);
            }

            return enum0.ZipLong(enum1, enum2, enum3, enum4, enum5, ZipBoth, ZipT0, ZipT1, ZipT2, ZipT3, ZipT4, ZipT5);
        }

        public static (T0, T1, T2, T3, T4, T5, T6) Aggregate<T0, T1, T2, T3, T4, T5, T6, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1, T2 seed2, T3 seed3, T4 seed4, T5 seed5, T6 seed6,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1, Func<T2, TSource, T2> func2, Func<T3, TSource, T3> func3, Func<T4, TSource, T4> func4, Func<T5, TSource, T5> func5, Func<T6, TSource, T6> func6)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }
            if (func2 is null)
            {
                throw new ArgumentNullException(nameof(func2));
            }
            if (func3 is null)
            {
                throw new ArgumentNullException(nameof(func3));
            }
            if (func4 is null)
            {
                throw new ArgumentNullException(nameof(func4));
            }
            if (func5 is null)
            {
                throw new ArgumentNullException(nameof(func5));
            }
            if (func6 is null)
            {
                throw new ArgumentNullException(nameof(func6));
            }

            var seed = (seed0, seed1, seed2, seed3, seed4, seed5, seed6);

            (T0, T1, T2, T3, T4, T5, T6) Accumalator((T0, T1, T2, T3, T4, T5, T6) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item), func2(accum.Item3, item), func3(accum.Item4, item), func4(accum.Item5, item), func5(accum.Item6, item), func6(accum.Item7, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T, T, T, T, T, T)> GetSequential7s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T, T, T, T, T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n2 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n3 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n4 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n5 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n6 = enumerator.Current;

                yield return (n0, n1, n2, n3, n4, n5, n6);
                while (enumerator.MoveNext())
                {
                    (n0, n1, n2, n3, n4, n5, n6) = (n1, n2, n3, n4, n5, n6, enumerator.Current);
                    yield return (n0, n1, n2, n3, n4, n5, n6);
                }
            }

            return Inner();
        }

        public static (T, T, T, T, T, T, T) GetFirst7<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item3 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item4 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item5 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item6 = enumerator.Current;
            return (item0, item1, item2, item3, item4, item5, item6);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential7s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T, T, T)> GetPartitions7s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential7s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 7) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T, T, T, T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
            yield return tuple.Item7;
        }

        public static IEnumerable<TOut> Zip<T0, T1, T2, T3, T4, T5, T6, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, IEnumerable<T6> enum6, Func<T0, T1, T2, T3, T4, T5, T6, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }
            if (enum6 is null)
            {
                throw new ArgumentNullException(nameof(enum6));
            }

            IEnumerable<TOut> ZipInner()
            {
                using var enume0 = enum0.GetEnumerator();
                using var enume1 = enum1.GetEnumerator();
                using var enume2 = enum2.GetEnumerator();
                using var enume3 = enum3.GetEnumerator();
                using var enume4 = enum4.GetEnumerator();
                using var enume5 = enum5.GetEnumerator();
                using var enume6 = enum6.GetEnumerator();
                while (enume0.MoveNext() && enume1.MoveNext() && enume2.MoveNext() && enume3.MoveNext() && enume4.MoveNext() && enume5.MoveNext() && enume6.MoveNext())
                {
                    yield return zipper(enume0.Current, enume1.Current, enume2.Current, enume3.Current, enume4.Current, enume5.Current, enume6.Current);
                }
            }

            return ZipInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, T5, T6, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, IEnumerable<T6> enum6, Func<T0, T1, T2, T3, T4, T5, T6, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1, Func<T2, TOut> zipper2, Func<T3, TOut> zipper3, Func<T4, TOut> zipper4, Func<T5, TOut> zipper5, Func<T6, TOut> zipper6)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }
            if (enum6 is null)
            {
                throw new ArgumentNullException(nameof(enum6));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }
            if (zipper2 is null)
            {
                throw new ArgumentNullException(nameof(zipper2));
            }
            if (zipper3 is null)
            {
                throw new ArgumentNullException(nameof(zipper3));
            }
            if (zipper4 is null)
            {
                throw new ArgumentNullException(nameof(zipper4));
            }
            if (zipper5 is null)
            {
                throw new ArgumentNullException(nameof(zipper5));
            }
            if (zipper6 is null)
            {
                throw new ArgumentNullException(nameof(zipper6));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;
                using var enum2ator = enum2.GetEnumerator();
                bool hasItemInenum2;
                using var enum3ator = enum3.GetEnumerator();
                bool hasItemInenum3;
                using var enum4ator = enum4.GetEnumerator();
                bool hasItemInenum4;
                using var enum5ator = enum5.GetEnumerator();
                bool hasItemInenum5;
                using var enum6ator = enum6.GetEnumerator();
                bool hasItemInenum6;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()) & (hasItemInenum2 = enum2ator.MoveNext()) & (hasItemInenum3 = enum3ator.MoveNext()) & (hasItemInenum4 = enum4ator.MoveNext()) & (hasItemInenum5 = enum5ator.MoveNext()) & (hasItemInenum6 = enum6ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current, enum2ator.Current, enum3ator.Current, enum4ator.Current, enum5ator.Current, enum6ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else

                if (hasItemInenum2)
                {
                    do
                    {
                        yield return zipper2(enum2ator.Current);
                    }
                    while (enum2ator.MoveNext());
                }
                else

                if (hasItemInenum3)
                {
                    do
                    {
                        yield return zipper3(enum3ator.Current);
                    }
                    while (enum3ator.MoveNext());
                }
                else

                if (hasItemInenum4)
                {
                    do
                    {
                        yield return zipper4(enum4ator.Current);
                    }
                    while (enum4ator.MoveNext());
                }
                else

                if (hasItemInenum5)
                {
                    do
                    {
                        yield return zipper5(enum5ator.Current);
                    }
                    while (enum5ator.MoveNext());
                }
                else

                if (hasItemInenum6)
                {
                    do
                    {
                        yield return zipper6(enum6ator.Current);
                    }
                    while (enum6ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, T5, T6, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, IEnumerable<T6> enum6, Func<Maybe<T0>, Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>, Maybe<T6>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }
            if (enum6 is null)
            {
                throw new ArgumentNullException(nameof(enum6));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
            {
                return zipper(item0, item1, item2, item3, item4, item5, item6);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing);
            }

            TOut ZipT2(T2 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, t, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing);
            }

            TOut ZipT3(T3 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, t, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing);
            }

            TOut ZipT4(T4 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, t, Maybe<T5>.Nothing, Maybe<T6>.Nothing);
            }

            TOut ZipT5(T5 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, t, Maybe<T6>.Nothing);
            }

            TOut ZipT6(T6 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, t);
            }

            return enum0.ZipLong(enum1, enum2, enum3, enum4, enum5, enum6, ZipBoth, ZipT0, ZipT1, ZipT2, ZipT3, ZipT4, ZipT5, ZipT6);
        }

        public static (T0, T1, T2, T3, T4, T5, T6, T7) Aggregate<T0, T1, T2, T3, T4, T5, T6, T7, TSource>(
            this IEnumerable<TSource> source, T0 seed0, T1 seed1, T2 seed2, T3 seed3, T4 seed4, T5 seed5, T6 seed6, T7 seed7,
            Func<T0, TSource, T0> func0, Func<T1, TSource, T1> func1, Func<T2, TSource, T2> func2, Func<T3, TSource, T3> func3, Func<T4, TSource, T4> func4, Func<T5, TSource, T5> func5, Func<T6, TSource, T6> func6, Func<T7, TSource, T7> func7)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func0 is null)
            {
                throw new ArgumentNullException(nameof(func0));
            }
            if (func1 is null)
            {
                throw new ArgumentNullException(nameof(func1));
            }
            if (func2 is null)
            {
                throw new ArgumentNullException(nameof(func2));
            }
            if (func3 is null)
            {
                throw new ArgumentNullException(nameof(func3));
            }
            if (func4 is null)
            {
                throw new ArgumentNullException(nameof(func4));
            }
            if (func5 is null)
            {
                throw new ArgumentNullException(nameof(func5));
            }
            if (func6 is null)
            {
                throw new ArgumentNullException(nameof(func6));
            }
            if (func7 is null)
            {
                throw new ArgumentNullException(nameof(func7));
            }

            var seed = (seed0, seed1, seed2, seed3, seed4, seed5, seed6, seed7);

            (T0, T1, T2, T3, T4, T5, T6, T7) Accumalator((T0, T1, T2, T3, T4, T5, T6, T7) accum, TSource item)
            {
                return (func0(accum.Item1, item), func1(accum.Item2, item), func2(accum.Item3, item), func3(accum.Item4, item), func4(accum.Item5, item), func5(accum.Item6, item), func6(accum.Item7, item), func7(accum.Item8, item));
            }

            return source.Aggregate(seed, Accumalator);
        }

        public static IEnumerable<(T, T, T, T, T, T, T, T)> GetSequential8s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, T, T, T, T, T, T, T)> Inner()
            {
                using var enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n0 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n1 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n2 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n3 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n4 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n5 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n6 = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
                var n7 = enumerator.Current;

                yield return (n0, n1, n2, n3, n4, n5, n6, n7);
                while (enumerator.MoveNext())
                {
                    (n0, n1, n2, n3, n4, n5, n6, n7) = (n1, n2, n3, n4, n5, n6, n7, enumerator.Current);
                    yield return (n0, n1, n2, n3, n4, n5, n6, n7);
                }
            }

            return Inner();
        }

        public static (T, T, T, T, T, T, T, T) GetFirst8<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item0 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item2 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item3 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item4 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item5 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item6 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable));
            }
            var item7 = enumerator.Current;
            return (item0, item1, item2, item3, item4, item5, item6, item7);
            //Mostly equivalent to (current handles nulls better):
            //return enumerable.GetSequential8s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T, T, T, T)> GetPartitions8s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetSequential8s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 8) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T, T, T, T, T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
            yield return tuple.Item7;
            yield return tuple.Item8;
        }

        public static IEnumerable<TOut> Zip<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, IEnumerable<T6> enum6, IEnumerable<T7> enum7, Func<T0, T1, T2, T3, T4, T5, T6, T7, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }
            if (enum6 is null)
            {
                throw new ArgumentNullException(nameof(enum6));
            }
            if (enum7 is null)
            {
                throw new ArgumentNullException(nameof(enum7));
            }

            IEnumerable<TOut> ZipInner()
            {
                using var enume0 = enum0.GetEnumerator();
                using var enume1 = enum1.GetEnumerator();
                using var enume2 = enum2.GetEnumerator();
                using var enume3 = enum3.GetEnumerator();
                using var enume4 = enum4.GetEnumerator();
                using var enume5 = enum5.GetEnumerator();
                using var enume6 = enum6.GetEnumerator();
                using var enume7 = enum7.GetEnumerator();
                while (enume0.MoveNext() && enume1.MoveNext() && enume2.MoveNext() && enume3.MoveNext() && enume4.MoveNext() && enume5.MoveNext() && enume6.MoveNext() && enume7.MoveNext())
                {
                    yield return zipper(enume0.Current, enume1.Current, enume2.Current, enume3.Current, enume4.Current, enume5.Current, enume6.Current, enume7.Current);
                }
            }

            return ZipInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, IEnumerable<T6> enum6, IEnumerable<T7> enum7, Func<T0, T1, T2, T3, T4, T5, T6, T7, TOut> zipper, Func<T0, TOut> zipper0, Func<T1, TOut> zipper1, Func<T2, TOut> zipper2, Func<T3, TOut> zipper3, Func<T4, TOut> zipper4, Func<T5, TOut> zipper5, Func<T6, TOut> zipper6, Func<T7, TOut> zipper7)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }
            if (enum6 is null)
            {
                throw new ArgumentNullException(nameof(enum6));
            }
            if (enum7 is null)
            {
                throw new ArgumentNullException(nameof(enum7));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipper0 is null)
            {
                throw new ArgumentNullException(nameof(zipper0));
            }
            if (zipper1 is null)
            {
                throw new ArgumentNullException(nameof(zipper1));
            }
            if (zipper2 is null)
            {
                throw new ArgumentNullException(nameof(zipper2));
            }
            if (zipper3 is null)
            {
                throw new ArgumentNullException(nameof(zipper3));
            }
            if (zipper4 is null)
            {
                throw new ArgumentNullException(nameof(zipper4));
            }
            if (zipper5 is null)
            {
                throw new ArgumentNullException(nameof(zipper5));
            }
            if (zipper6 is null)
            {
                throw new ArgumentNullException(nameof(zipper6));
            }
            if (zipper7 is null)
            {
                throw new ArgumentNullException(nameof(zipper7));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                using var enum0ator = enum0.GetEnumerator();
                bool hasItemInenum0;
                using var enum1ator = enum1.GetEnumerator();
                bool hasItemInenum1;
                using var enum2ator = enum2.GetEnumerator();
                bool hasItemInenum2;
                using var enum3ator = enum3.GetEnumerator();
                bool hasItemInenum3;
                using var enum4ator = enum4.GetEnumerator();
                bool hasItemInenum4;
                using var enum5ator = enum5.GetEnumerator();
                bool hasItemInenum5;
                using var enum6ator = enum6.GetEnumerator();
                bool hasItemInenum6;
                using var enum7ator = enum7.GetEnumerator();
                bool hasItemInenum7;

                while ((hasItemInenum0 = enum0ator.MoveNext()) & (hasItemInenum1 = enum1ator.MoveNext()) & (hasItemInenum2 = enum2ator.MoveNext()) & (hasItemInenum3 = enum3ator.MoveNext()) & (hasItemInenum4 = enum4ator.MoveNext()) & (hasItemInenum5 = enum5ator.MoveNext()) & (hasItemInenum6 = enum6ator.MoveNext()) & (hasItemInenum7 = enum7ator.MoveNext()))
                {
                    yield return zipper(enum0ator.Current, enum1ator.Current, enum2ator.Current, enum3ator.Current, enum4ator.Current, enum5ator.Current, enum6ator.Current, enum7ator.Current);
                }

                if (hasItemInenum0)
                {
                    do
                    {
                        yield return zipper0(enum0ator.Current);
                    }
                    while (enum0ator.MoveNext());
                }
                else

                if (hasItemInenum1)
                {
                    do
                    {
                        yield return zipper1(enum1ator.Current);
                    }
                    while (enum1ator.MoveNext());
                }
                else

                if (hasItemInenum2)
                {
                    do
                    {
                        yield return zipper2(enum2ator.Current);
                    }
                    while (enum2ator.MoveNext());
                }
                else

                if (hasItemInenum3)
                {
                    do
                    {
                        yield return zipper3(enum3ator.Current);
                    }
                    while (enum3ator.MoveNext());
                }
                else

                if (hasItemInenum4)
                {
                    do
                    {
                        yield return zipper4(enum4ator.Current);
                    }
                    while (enum4ator.MoveNext());
                }
                else

                if (hasItemInenum5)
                {
                    do
                    {
                        yield return zipper5(enum5ator.Current);
                    }
                    while (enum5ator.MoveNext());
                }
                else

                if (hasItemInenum6)
                {
                    do
                    {
                        yield return zipper6(enum6ator.Current);
                    }
                    while (enum6ator.MoveNext());
                }
                else

                if (hasItemInenum7)
                {
                    do
                    {
                        yield return zipper7(enum7ator.Current);
                    }
                    while (enum7ator.MoveNext());
                }
                else
                {
                    yield break;
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TOut> ZipLong<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this IEnumerable<T0> enum0, IEnumerable<T1> enum1, IEnumerable<T2> enum2, IEnumerable<T3> enum3, IEnumerable<T4> enum4, IEnumerable<T5> enum5, IEnumerable<T6> enum6, IEnumerable<T7> enum7, Func<Maybe<T0>, Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>, Maybe<T6>, Maybe<T7>, TOut> zipper)
        {
            if (enum0 is null)
            {
                throw new ArgumentNullException(nameof(enum0));
            }
            if (enum1 is null)
            {
                throw new ArgumentNullException(nameof(enum1));
            }
            if (enum2 is null)
            {
                throw new ArgumentNullException(nameof(enum2));
            }
            if (enum3 is null)
            {
                throw new ArgumentNullException(nameof(enum3));
            }
            if (enum4 is null)
            {
                throw new ArgumentNullException(nameof(enum4));
            }
            if (enum5 is null)
            {
                throw new ArgumentNullException(nameof(enum5));
            }
            if (enum6 is null)
            {
                throw new ArgumentNullException(nameof(enum6));
            }
            if (enum7 is null)
            {
                throw new ArgumentNullException(nameof(enum7));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            TOut ZipBoth(T0 item0, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
            {
                return zipper(item0, item1, item2, item3, item4, item5, item6, item7);
            }

            TOut ZipT0(T0 t)
            {
                return zipper(t, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing, Maybe<T7>.Nothing);
            }

            TOut ZipT1(T1 t)
            {
                return zipper(Maybe<T0>.Nothing, t, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing, Maybe<T7>.Nothing);
            }

            TOut ZipT2(T2 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, t, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing, Maybe<T7>.Nothing);
            }

            TOut ZipT3(T3 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, t, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing, Maybe<T7>.Nothing);
            }

            TOut ZipT4(T4 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, t, Maybe<T5>.Nothing, Maybe<T6>.Nothing, Maybe<T7>.Nothing);
            }

            TOut ZipT5(T5 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, t, Maybe<T6>.Nothing, Maybe<T7>.Nothing);
            }

            TOut ZipT6(T6 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, t, Maybe<T7>.Nothing);
            }

            TOut ZipT7(T7 t)
            {
                return zipper(Maybe<T0>.Nothing, Maybe<T1>.Nothing, Maybe<T2>.Nothing, Maybe<T3>.Nothing, Maybe<T4>.Nothing, Maybe<T5>.Nothing, Maybe<T6>.Nothing, t);
            }

            return enum0.ZipLong(enum1, enum2, enum3, enum4, enum5, enum6, enum7, ZipBoth, ZipT0, ZipT1, ZipT2, ZipT3, ZipT4, ZipT5, ZipT6, ZipT7);
        }

    }
}