using Nintenlord.Collections.DataChange;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Collections
{
    /// <summary>
    /// Extensions and helper methods to .NET collections
    /// </summary>
    public static class CollectionExtensions
    {
        public static bool Or(this IEnumerable<bool> collection)
        {
            return collection.Any(x => x);
        }

        public static bool And(this IEnumerable<bool> collection)
        {
            return collection.All(x => x);
        }

        public static T Max<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Empty IEnumerable", "collection");
            }
            T max = enumerator.Current;
            while (enumerator.MoveNext())
            {
                T current = enumerator.Current;
                if (current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        public static T Max<T>(this IEnumerable<T> collection, IComparer<T> comp)
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Empty IEnumerable", "collection");
            }
            T max = enumerator.Current;
            while (enumerator.MoveNext())
            {
                T current = enumerator.Current;
                if (comp.Compare(current, max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        public static T Min<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Empty IEnumerable", "collection");
            }
            T min = enumerator.Current;
            while (enumerator.MoveNext())
            {
                T current = enumerator.Current;
                if (current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }
            return min;
        }

        public static T Min<T>(this IEnumerable<T> collection, IComparer<T> comp)
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Empty IEnumerable", "collection");
            }
            T min = enumerator.Current;
            while (enumerator.MoveNext())
            {
                T current = enumerator.Current;
                if (comp.Compare(current, min) < 0)
                {
                    min = current;
                }
            }
            return min;
        }

        public static T MinBy<T>(this IEnumerable<T> collection, Func<T, float> comp)
        {
            (T, float) Min((T, float) first, T second)
            {
                var secondValue = comp(second);
                if (first.Item2 <= secondValue)
                {
                    return first;
                }
                else
                {
                    return (second, secondValue);
                }
            }

            return collection.Aggregate((default(T), float.MaxValue), Min, x => x.Item1);
        }

        public static string ToElementWiseString<T>(this IEnumerable<T> collection)
        {
            StringBuilder text = new StringBuilder("{");

            foreach (T item in collection)
            {
                text.AppendFormat("{0}, ", item);
            }

            if (text.Length > 1)
            {
                text.Remove(text.Length - 2, 2);
            }
            text.Append("}");

            return text.ToString();
        }

        public static string ToElementWiseString<T>(this IEnumerable<T> collection, string separator, string beginning, string end)
        {
            StringBuilder text = new StringBuilder(beginning);

            foreach (T item in collection)
            {
                text.Append(item + separator);
            }

            if (text.Length > 1)
            {
                text.Remove(text.Length - separator.Length, separator.Length);
            }
            text.Append(end);

            return text.ToString();
        }

        public static TValue GetValue<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            TValue result = default(TValue);

            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.TryGetValue(kay, out result))
                {
                    break;
                }
            }
            return result;
        }

        public static bool ContainsKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            return scopes.Any(item => item.ContainsKey(kay));
        }

        public static bool TryGetKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay, out TValue value)
        {
            bool result = false;
            value = default(TValue);

            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.TryGetValue(kay, out value))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static bool Contains<T>(this IEnumerable<T> array, Predicate<T> test)
        {
            return array.Any(item2 => test(item2));
        }

        public static int AmountOf<T>(this IEnumerable<T> array, T item)
        {
            return array.Count(item2 => item.Equals(item2));
        }

        public static string GetString(this IEnumerable<char> enume)
        {
            StringBuilder bldr;
            if (enume is ICollection<char>)
            {
                bldr = new StringBuilder((enume as ICollection<char>).Count);
            }
            else
            {
                bldr = new StringBuilder();
            }

            foreach (var item in enume)
            {
                bldr.Append(item);
            }
            return bldr.ToString();
        }

        public static string ToHumanString<T>(this IEnumerable<T> list)
        {
            T[] array = list.ToArray();
            if (array.Length > 1)
            {
                StringBuilder bldr = new StringBuilder();
                for (int i = 0; i < array.Length - 2; i++)
                {
                    bldr.Append(array[i]);
                    bldr.Append(", ");
                }
                bldr.Append(array[array.Length - 2]);
                bldr.Append(" & ");
                bldr.Append(array[array.Length - 1]);
                return bldr.ToString();
            }
            else if (array.Length == 1)
            {
                return array[0].ToString();
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// Merges two ordered enumerables into one ordered.
        /// </summary>
        /// <typeparam name="T">Type of items to order.</typeparam>
        /// <param name="list1">Ordered enumerable.</param>
        /// <param name="list2">Ordered enumerable.</param>
        /// <param name="comp">Comparer of T.</param>
        /// <returns>Ordered enumerable containing all items of passed enumerators.</returns>
        public static IEnumerable<T> OrderedUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2, IComparer<T> comp)
        {
            IEnumerator<T> enume1 = list1.GetEnumerator();
            IEnumerator<T> enume2 = list2.GetEnumerator();

            bool moveFirstToNext = true;
            bool moveSecondToNext = true;

            while (true)
            {
                if (moveFirstToNext)
                {
                    if (enume1.MoveNext())
                    {
                        moveFirstToNext = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (moveSecondToNext)
                {
                    if (enume2.MoveNext())
                    {
                        moveSecondToNext = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (comp.Compare(enume1.Current, enume2.Current) <= 0)
                {
                    yield return enume1.Current;
                    moveFirstToNext = true;
                }
                else
                {
                    yield return enume2.Current;
                    moveSecondToNext = true;
                }
            }

            //One of the enumerators was run completely
            if (moveFirstToNext)
            {
                if (!moveSecondToNext)//Current hasn't been consumed
                {
                    yield return enume2.Current;
                }
                while (enume2.MoveNext())
                {
                    yield return enume2.Current;
                }

            }
            else
            {
                if (!moveFirstToNext)//Current hasn't been consumed
                {
                    yield return enume1.Current;
                }
                while (enume1.MoveNext())
                {
                    yield return enume1.Current;
                }
            }
        }

        /// <summary>
        /// Merges two ordered enumerables into one ordered.
        /// </summary>
        /// <typeparam name="T">Type of items to order.</typeparam>
        /// <param name="list1">Ordered enumerable.</param>
        /// <param name="list2">Ordered enumerable.</param>
        /// <param name="comp">Comparer of T.</param>
        /// <returns>Ordered enumerable containing all items of passed enumerators.</returns>
        public static IEnumerable<T> OrderedUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2, Func<T, T, int> comp)
        {
            IEnumerator<T> enume1 = list1.GetEnumerator();
            IEnumerator<T> enume2 = list2.GetEnumerator();

            bool moveFirstToNext = true;
            bool moveSecondToNext = true;

            while (true)
            {
                if (moveFirstToNext)
                {
                    if (enume1.MoveNext())
                    {
                        moveFirstToNext = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (moveSecondToNext)
                {
                    if (enume2.MoveNext())
                    {
                        moveSecondToNext = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (comp(enume1.Current, enume2.Current) <= 0)
                {
                    yield return enume1.Current;
                    moveFirstToNext = true;
                }
                else
                {
                    yield return enume2.Current;
                    moveSecondToNext = true;
                }
            }

            //One of the enumerators was run completely
            if (moveFirstToNext)
            {
                if (!moveSecondToNext)//Current hasn't been consumed
                {
                    yield return enume2.Current;
                }
                while (enume2.MoveNext())
                {
                    yield return enume2.Current;
                }

            }
            else
            {
                if (!moveFirstToNext)//Current hasn't been consumed
                {
                    yield return enume1.Current;
                }
                while (enume1.MoveNext())
                {
                    yield return enume1.Current;
                }
            }
        }

        /// <summary>
        /// Merges two ordered enumerables into one ordered.
        /// </summary>
        /// <typeparam name="T">Type of comparable items to order.</typeparam>
        /// <param name="list1">Ordered enumerable.</param>
        /// <param name="list2">Ordered enumerable.</param>
        /// <returns>Ordered enumerable containing all items of passed enumerators.</returns>
        public static IEnumerable<T> OrderedUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
            where T : IComparable<T>
        {
            return list1.OrderedUnion(list2, Comparer<T>.Default);
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> toRepeat)
        {
            while (true)
            {
                foreach (var item in toRepeat)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Repeat<T>(this T item)
        {
            while (true)
            {
                yield return item;
            }
        }

        public static void AddAll<TKey, Tvalue>(this IDictionary<TKey, Tvalue> a,
            IEnumerable<KeyValuePair<TKey, Tvalue>> values)
        {
            foreach (var item in values)
            {
                a.Add(item.Key, item.Value);
            }
        }

        public static TValue GetOldOrSetNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (!dict.TryGetValue(key, out TValue value))
            {
                value = new TValue();
                dict[key] = value;
            }
            return value;
        }

        public static IEnumerable<Tuple<int, T>> Index<T>(this IEnumerable<T> items)
        {
            int index = 0;
            foreach (var item in items)
            {
                yield return Tuple.Create(index, item);
                index++;
            }
        }

        public static int GetEqualsInBeginning<T>(this IList<T> a, IList<T> b, IEqualityComparer<T> comp)
        {
            int max = Math.Min(a.Count, b.Count);
            int count;
            for (count = 0; count < max; count++)
            {
                if (!comp.Equals(a[count], b[count]))
                {
                    break;
                }
            }
            return count;
        }
        public static int GetEqualsInBeginning<T>(this IList<T> a, IList<T> b)
        {
            return a.GetEqualsInBeginning(b, EqualityComparer<T>.Default);
        }


        public static IndexOverlay GetOverlay<T>(this IDictionary<int, T> dict, Func<T, int> measurement)
        {
            IndexOverlay result = new IndexOverlay();

            foreach (var item in dict)
            {
                int length = measurement(item.Value);
                result.AddIndexes(item.Key, length);
            }

            return result;
        }

        public static bool CanFit<T>(this IDictionary<int, T> dict, Func<T, int> measurement,
            int index, T item)
        {
            int lastIndex = index + measurement(item);

            for (int i = index; i < lastIndex; i++)
            {
                if (dict.ContainsKey(i))
                {
                    return false;
                }
            }
            for (int i = index - 1; i >= 0; i--)
            {
                if (dict.TryGetValue(i, out T oldItem) && i + measurement(oldItem) > index)
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<T> Flatten<T, TEnumarable>(this IEnumerable<TEnumarable> collection)
            where TEnumarable : IEnumerable<T>
        {
            return collection.SelectMany(x => x);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            return collection.SelectMany(x => x);
        }

        public static IEnumerable<TOut> ConvertAll<TIn, TOut>(this IEnumerable<TIn> enume, Func<TIn, TOut> conversion)
        {
            return enume.Select(conversion);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, params T[] args)
        {
            return collection.Concat((IEnumerable<T>)args);
        }

        public static IEnumerable<Tuple<T, T>> GetPairs<T>(this IEnumerable<T> enume)
        {
            var enumerator = enume.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            T first = enumerator.Current;

            while (enumerator.MoveNext())
            {
                T second = enumerator.Current;
                yield return Tuple.Create(first, second);
                first = second;
            }
        }

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
              emptyProduct,
              (accumulator, sequence) =>
                from accseq in accumulator
                from item in sequence
                select accseq.Concat(new[] { item }));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            TKey key, TValue def = default(TValue))
        {
            if (!dict.TryGetValue(key, out TValue value))
            {
                value = def;
            }
            return value;
        }

        public static TValue GetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            TKey key,
            TValue defaultVal = default(TValue))
        {
            return dict.TryGetValue(key, out TValue val) ? val : defaultVal;
        }

        public static Tuple<TAccumulate1, TAccumulate2> Aggregate<TAccumulate1, TAccumulate2, TSource>(
            this IEnumerable<TSource> source,
            TAccumulate1 seed1,
            TAccumulate2 seed2,
            Func<TAccumulate1, TSource, TAccumulate1> func1,
            Func<TAccumulate2, TSource, TAccumulate2> func2)
        {
            var seed = Tuple.Create(seed1, seed2);

            Func<
                Tuple<TAccumulate1, TAccumulate2>,
                TSource,
                Tuple<TAccumulate1, TAccumulate2>> func =
                (accum, sourceItem) => Tuple.Create(func1(accum.Item1, sourceItem), func2(accum.Item2, sourceItem));

            return source.Aggregate(seed, func);
        }

        public static Tuple<TAccumulate1, TAccumulate2, TAccumulate3>
            Aggregate<TAccumulate1, TAccumulate2, TAccumulate3, TSource>(
                this IEnumerable<TSource> source,
                TAccumulate1 seed1,
                TAccumulate2 seed2,
                TAccumulate3 seed3,
                Func<TAccumulate1, TSource, TAccumulate1> func1,
                Func<TAccumulate2, TSource, TAccumulate2> func2,
                Func<TAccumulate3, TSource, TAccumulate3> func3)
        {
            var seed = Tuple.Create(seed1, seed2, seed3);

            Func<
                Tuple<TAccumulate1, TAccumulate2, TAccumulate3>,
                TSource,
                Tuple<TAccumulate1, TAccumulate2, TAccumulate3>> func =
                (accum, sourceItem) =>
                    Tuple.Create(
                        func1(accum.Item1, sourceItem),
                        func2(accum.Item2, sourceItem),
                        func3(accum.Item3, sourceItem));

            return source.Aggregate(seed, func);
        }

        public static IEnumerable<(T, bool isLast)> GetIsLast<T>(this IEnumerable<T> items)
        {
            bool hasValue = false;
            T previousItem = default;

            foreach (var item in items)
            {
                if (hasValue)
                {
                    yield return (previousItem, false);
                }
                else
                {
                    hasValue = true;
                }
                previousItem = item;
            }
            if (hasValue)
            {
                yield return (previousItem, true);
            }
        }

        public static IEnumerable<TOut> ZipLong<T1, T2, TOut>(this IEnumerable<T1> items1, IEnumerable<T2> items2, Func<Maybe<T1>, Maybe<T2>, TOut> zipper)
        {
            foreach (var (item1, item2) in
                items1.Select(Maybe<T1>.Just).Concat(Repeat(Maybe<T1>.Nothing))
                .Zip(items2.Select(Maybe<T2>.Just).Concat(Repeat(Maybe<T2>.Nothing)), (x,y) => (x,y)))
            {
                if (!item1.HasValue && !item2.HasValue)
                {
                    //Both have run out
                    yield break;
                }
                yield return zipper(item1, item2);
            }
        }

        public static IEnumerable<TScan> Scan<T, TScan>(this IEnumerable<T> items, TScan start, Func<TScan, T, TScan> scanner)
        {
            yield return start;
            TScan current = start;
            foreach (var item in items)
            {
                current = scanner(current, item);
                yield return current;
            }
        }

        public static IEnumerable<(T current, T next)> GetSequentialPairs<T>(this IEnumerable<T> items)
        {
            (Maybe<T>, Maybe<T>) MoveNext((Maybe<T>, Maybe<T>) previous, Maybe<T> next)
            {
                var (previousItem, current) = previous;

                return (current, next);
            }

            return items.Select(Maybe<T>.Just)
                        .Scan((Maybe<T>.Nothing, Maybe<T>.Nothing), MoveNext)
                        .Select(pair => pair.Item1.Zip(pair.Item2, (x,y) => (x, y)))
                        .GetValues();
        }
    }
}