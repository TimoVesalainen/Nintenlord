using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Collections
{
    public static partial class CollectionExtensions
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

            (Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1) = previous;

                return (a1, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, (a0, a1) => (a0, a1)))
                        .GetValues();
        }

        public static (T, T) GetFirst2<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential2s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T)> GetParts2s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1) = previous;

                return (a1, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, (a0, a1) => (a0, a1)))
                        .GetValues();
        }

        public static IEnumerable<(T, T)> GetPartitions2s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts2s()
                        .Select((x, i) => (x, i))
                        .Where(t => (t.i % 2) == 0)
                        .Select(t => t.x);
        }

        public static IEnumerable<T> Enumerate<T>(this (T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
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

            (Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2) = previous;

                return (a1, a2, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, (a0, a1, a2) => (a0, a1, a2)))
                        .GetValues();
        }

        public static (T, T, T) GetFirst3<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential3s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T)> GetParts3s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2) = previous;

                return (a1, a2, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, (a0, a1, a2) => (a0, a1, a2)))
                        .GetValues();
        }

        public static IEnumerable<(T, T, T)> GetPartitions3s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts3s()
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

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3) = previous;

                return (a1, a2, a3, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, (a0, a1, a2, a3) => (a0, a1, a2, a3)))
                        .GetValues();
        }

        public static (T, T, T, T) GetFirst4<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential4s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T)> GetParts4s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3) = previous;

                return (a1, a2, a3, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, (a0, a1, a2, a3) => (a0, a1, a2, a3)))
                        .GetValues();
        }

        public static IEnumerable<(T, T, T, T)> GetPartitions4s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts4s()
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

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4) = previous;

                return (a1, a2, a3, a4, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, (a0, a1, a2, a3, a4) => (a0, a1, a2, a3, a4)))
                        .GetValues();
        }

        public static (T, T, T, T, T) GetFirst5<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential5s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T)> GetParts5s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4) = previous;

                return (a1, a2, a3, a4, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, (a0, a1, a2, a3, a4) => (a0, a1, a2, a3, a4)))
                        .GetValues();
        }

        public static IEnumerable<(T, T, T, T, T)> GetPartitions5s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts5s()
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

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4, a5) = previous;

                return (a1, a2, a3, a4, a5, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, (a0, a1, a2, a3, a4, a5) => (a0, a1, a2, a3, a4, a5)))
                        .GetValues();
        }

        public static (T, T, T, T, T, T) GetFirst6<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential6s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T, T)> GetParts6s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4, a5) = previous;

                return (a1, a2, a3, a4, a5, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, (a0, a1, a2, a3, a4, a5) => (a0, a1, a2, a3, a4, a5)))
                        .GetValues();
        }

        public static IEnumerable<(T, T, T, T, T, T)> GetPartitions6s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts6s()
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

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4, a5, a6) = previous;

                return (a1, a2, a3, a4, a5, a6, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, (a0, a1, a2, a3, a4, a5, a6) => (a0, a1, a2, a3, a4, a5, a6)))
                        .GetValues();
        }

        public static (T, T, T, T, T, T, T) GetFirst7<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential7s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T, T, T)> GetParts7s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4, a5, a6) = previous;

                return (a1, a2, a3, a4, a5, a6, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, (a0, a1, a2, a3, a4, a5, a6) => (a0, a1, a2, a3, a4, a5, a6)))
                        .GetValues();
        }

        public static IEnumerable<(T, T, T, T, T, T, T)> GetPartitions7s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts7s()
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

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4, a5, a6, a7) = previous;

                return (a1, a2, a3, a4, a5, a6, a7, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8, (a0, a1, a2, a3, a4, a5, a6, a7) => (a0, a1, a2, a3, a4, a5, a6, a7)))
                        .GetValues();
        }

        public static (T, T, T, T, T, T, T, T) GetFirst8<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return enumerable.GetSequential8s().FirstSafe().GetValueOrThrow(() => new ArgumentException("Enumerable doesn't have enough items", nameof(enumerable)));
        }

        public static IEnumerable<(T, T, T, T, T, T, T, T)> GetParts8s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            (Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (a0, a1, a2, a3, a4, a5, a6, a7) = previous;

                return (a1, a2, a3, a4, a5, a6, a7, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(tuple => tuple.Item1.Zip(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8, (a0, a1, a2, a3, a4, a5, a6, a7) => (a0, a1, a2, a3, a4, a5, a6, a7)))
                        .GetValues();
        }

        public static IEnumerable<(T, T, T, T, T, T, T, T)> GetPartitions8s<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.GetParts8s()
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

	}
}