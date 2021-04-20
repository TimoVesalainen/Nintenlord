using Nintenlord.Collections.Comparers;
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
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Any(x => x);
        }

        public static bool And(this IEnumerable<bool> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.All(x => x);
        }

        public static T Max<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            T MaxBy(T item1, T item2)
            {
                if (item1.CompareTo(item2) >= 0)
                {
                    return item1;
                }
                else
                {
                    return item2;
                }
            }

            return collection.Aggregate(MaxBy);
        }

        public static T Max<T>(this IEnumerable<T> collection, IComparer<T> comp = null)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comp = comp ?? Comparer<T>.Default;

            return collection.Aggregate(comp.Max);
        }

        public static T Min<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            T MinBy(T item1, T item2)
            {
                if (item1.CompareTo(item2) <= 0)
                {
                    return item1;
                }
                else
                {
                    return item2;
                }
            }

            return collection.Aggregate(MinBy);
        }

        public static T Min<T>(this IEnumerable<T> collection, IComparer<T> comp)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comp = comp ?? Comparer<T>.Default;

            return collection.Aggregate(comp.Min);
        }

        public static T MinBy<T>(this IEnumerable<T> collection, Func<T, float> comp)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comp is null)
            {
                throw new ArgumentNullException(nameof(comp));
            }

            return collection.Min(new SelectComparer<T, float>(comp));
        }

        public static T MaxBy<T>(this IEnumerable<T> collection, Func<T, float> comp)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (comp is null)
            {
                throw new ArgumentNullException(nameof(comp));
            }

            return collection.Max(new SelectComparer<T, float>(comp));
        }

        public static string ToElementWiseString<T>(this IEnumerable<T> collection, string separator = ", ", string beginning = "{", string end = "}")
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return $"{beginning}{string.Join(separator, collection)}{end}";
        }

        public static TValue GetValue<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            if (scopes is null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

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
            if (scopes is null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

            return scopes.Any(item => item.ContainsKey(kay));
        }

        public static bool TryGetKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay, out TValue value)
        {
            if (scopes is null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

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
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (test is null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            return array.Any(item2 => test(item2));
        }

        public static int AmountOf<T>(this IEnumerable<T> array, T item)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.Count(item2 => item.Equals(item2));
        }

        public static string GetString(this IEnumerable<char> enume)
        {
            if (enume is null)
            {
                throw new ArgumentNullException(nameof(enume));
            }

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
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

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
        public static IEnumerable<T> OrderedUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2, IComparer<T> comp = null)
        {
            if (list1 is null)
            {
                throw new ArgumentNullException(nameof(list1));
            }

            if (list2 is null)
            {
                throw new ArgumentNullException(nameof(list2));
            }

            comp = comp ?? Comparer<T>.Default;

            return OrderedUnion(list1, list2, comp.Compare);
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
            if (list1 is null)
            {
                throw new ArgumentNullException(nameof(list1));
            }

            if (list2 is null)
            {
                throw new ArgumentNullException(nameof(list2));
            }

            if (comp is null)
            {
                throw new ArgumentNullException(nameof(comp));
            }

            IEnumerable<T> OrderedUnionInner()
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

            return OrderedUnionInner();
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
            if (toRepeat is null)
            {
                throw new ArgumentNullException(nameof(toRepeat));
            }

            IEnumerable<T> CycleInner()
            {
                while (true)
                {
                    foreach (var item in toRepeat)
                    {
                        yield return item;
                    }
                }
            }

            return CycleInner();
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
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IEnumerable<(T, bool isLast)> GetIsLastInner()
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

            return GetIsLastInner();
        }

        public static IEnumerable<TOut> ZipLong<T1, T2, TOut>(this IEnumerable<T1> items1, IEnumerable<T2> items2, Func<Maybe<T1>, Maybe<T2>, TOut> zipper)
        {
            if (items1 is null)
            {
                throw new ArgumentNullException(nameof(items1));
            }

            if (items2 is null)
            {
                throw new ArgumentNullException(nameof(items2));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            IEnumerable<TOut> ZipLongInner()
            {
                foreach (var (item1, item2) in
                    items1.Select(Maybe<T1>.Just).Concat(Repeat(Maybe<T1>.Nothing))
                    .Zip(items2.Select(Maybe<T2>.Just).Concat(Repeat(Maybe<T2>.Nothing)), (x, y) => (x, y)))
                {
                    if (!item1.HasValue && !item2.HasValue)
                    {
                        //Both have run out
                        yield break;
                    }
                    yield return zipper(item1, item2);
                }
            }

            return ZipLongInner();
        }

        public static IEnumerable<TScan> Scan<T, TScan>(this IEnumerable<T> items, TScan start, Func<TScan, T, TScan> scanner)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (scanner is null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            IEnumerable<TScan> ScanInner()
            {
                yield return start;
                TScan current = start;
                foreach (var item in items)
                {
                    current = scanner(current, item);
                    yield return current;
                }
            }

            return ScanInner();
        }

        public static IEnumerable<(T current, T next)> GetSequentialPairs<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

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

        public static IEnumerable<T> Cons<T>(this IEnumerable<T> enumerable, T newHead)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            IEnumerable<T> ConsInner()
            {
                yield return newHead;
                foreach (var item in enumerable)
                {
                    yield return item;
                }
            }

            return ConsInner();
        }

        public static IEnumerable<T> Return<T>(this T item)
        {
            yield return item;
        }
    }
}