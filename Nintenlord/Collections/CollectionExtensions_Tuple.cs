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

        public static (T, T) GetFirst2<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
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

        public static (T, T, T) GetFirst3<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
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

        public static (T, T, T, T) GetFirst4<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
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

        public static (T, T, T, T, T) GetFirst5<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
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

        public static (T, T, T, T, T, T) GetFirst6<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
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

        public static (T, T, T, T, T, T, T) GetFirst7<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
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

        public static (T, T, T, T, T, T, T, T) GetFirst8<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            using (var enumerator = enumerable.GetEnumerator())
            {
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
            }
        }
	}
}