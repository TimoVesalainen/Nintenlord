﻿using Nintenlord.Collections.Comparers;
using Nintenlord.Distributions;
using Nintenlord.Distributions.Discrete;
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
    public static partial class EnumerableExtensions
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

        public static T Max<T>(this IEnumerable<T> collection, IComparer<T> comp = null)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comp ??= Comparer<T>.Default;

            return collection.Aggregate(comp.Max);
        }

        public static IEnumerable<T> MaxScan<T>(this IEnumerable<T> items, IComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= Comparer<T>.Default;

            return items.Scan(comparer.Max);
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

        public static T Min<T>(this IEnumerable<T> collection, IComparer<T> comp = null)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comp ??= Comparer<T>.Default;

            return collection.Aggregate(comp.Min);
        }

        public static IEnumerable<T> MinScan<T>(this IEnumerable<T> items, IComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= Comparer<T>.Default;

            return items.Scan(comparer.Min);
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

        public static T GetNth<T>(this IEnumerable<T> collection, int index)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Negative value");
            }

            return collection.Skip(index).First();
        }

        public static T GetNthOrDefault<T>(this IEnumerable<T> collection, int index, T defaultValue = default)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Negative value");
            }

            return collection.Skip(index).FirstOrDefault(defaultValue);
        }

        public static string ToElementWiseString<T>(this IEnumerable<T> collection, string separator = ", ", string beginning = "{", string end = "}")
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return $"{beginning}{string.Join(separator, collection)}{end}";
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
                StringBuilder bldr = new();
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

        public static IEnumerable<(T item, int index, int length)> GroupWithIndex<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= EqualityComparer<T>.Default;

            int prevIndex = -1;
            T previous = default;
            int length = 0;
            int start = 0;
            foreach (var item in items)
            {
                if (prevIndex < 0)
                {
                    previous = item;
                    start = 0;
                    length = 1;
                }
                else if (comparer.Equals(item, previous))
                {
                    length++;
                }
                else
                {
                    yield return (previous, start, length);

                    previous = item;
                    start = prevIndex + 1;
                    length = 1;
                }
                prevIndex++;
            }
            if (prevIndex >= 0)
            {
                yield return (previous, start, length);
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

            comp ??= Comparer<T>.Default;

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

        public static IEnumerable<T> FindLargest<T>(this IEnumerable<T> items, Func<T, int> measure)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (measure is null)
            {
                throw new ArgumentNullException(nameof(measure));
            }

            List<T> bufferForLargestYet = new();
            int currentLargestMeasure = int.MinValue;

            foreach (var item in items)
            {
                var itemMeasure = measure(item);

                if (currentLargestMeasure < itemMeasure)
                {
                    bufferForLargestYet.Clear();
                    bufferForLargestYet.Add(item);
                    currentLargestMeasure = itemMeasure;
                }
                else if (currentLargestMeasure == itemMeasure)
                {
                    bufferForLargestYet.Add(item);
                }
            }

            return bufferForLargestYet;
        }

        public static IEnumerable<T> FindLargest<T>(this IEnumerable<T> items, IComparer<T> comparer)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            List<T> bufferForLargestYet = new();

            foreach (var item in items)
            {
                if (bufferForLargestYet.Count == 0)
                {
                    bufferForLargestYet.Add(item);
                }
                else
                {
                    var comparison = comparer.Compare(bufferForLargestYet[0], item);
                    if (comparison < 0)
                    {
                        bufferForLargestYet.Clear();
                        bufferForLargestYet.Add(item);
                    }
                    else if (comparison == 0)
                    {
                        bufferForLargestYet.Add(item);
                    }
                }
            }

            return bufferForLargestYet;
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

        public static IEnumerable<T> ConcatInfinite<T>(this IEnumerable<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            IEnumerable<T> Result()
            {
                foreach (var itemPrefix in collection)
                {
                    yield return itemPrefix;
                }
                while (true)
                {
                    yield return item;
                }
            }

            return Result();
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
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

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
                select accseq.Concat(item));
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

        public static IEnumerable<T> Scan<T>(this IEnumerable<T> items, Func<T, T, T> scanner)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (scanner is null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            IEnumerable<T> ScanInner()
            {
                using var enumerator = items.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                yield return enumerator.Current;

                var accum = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    accum = scanner(accum, enumerator.Current);
                    yield return accum;
                }
            }

            return ScanInner();
        }

        public static IEnumerable<T> UnAggregate<TState, T>(this TState item, Func<TState, (TState, T)> unScanner)
        {
            ArgumentNullException.ThrowIfNull(unScanner);

            IEnumerable<T> ScanInner()
            {
                var state = item;
                while (true)
                {
                    var (newState, item) = unScanner(state);
                    yield return item;
                    state = newState;
                }
            }

            return ScanInner();
        }
        public static IEnumerable<T> UnAggregate<T>(this T item, Func<T, T> unScanner)
        {
            return Iterate(unScanner, item);
        }

        public static IEnumerable<(T current, T next)> GetSequentialPairs<T>(this IEnumerable<T> items)
        {
            return items.GetSequential2s();
        }

        public static IEnumerable<T> Return<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> Iterate<T>(Func<T, T> toIterate, T start)
        {
            if (toIterate is null)
            {
                throw new ArgumentNullException(nameof(toIterate));
            }

            IEnumerable<T> IterateInner()
            {
                while (true)
                {
                    yield return start;
                    start = toIterate(start);
                }
            }

            return IterateInner();
        }

        public static IEnumerable<T> Intersperse<T>(this IEnumerable<T> items, T separator)
        {
            return items.Zip(Repeat(separator), ValueTuple.Create)
                .SelectMany(Enumerate)
                .GetIsLast()
                .Where(x => !x.isLast)
                .Select(x => x.Item1);
        }

        public static IEnumerable<T[]> GetEquivalenceClasses<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> getConjugates)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (getConjugates is null)
            {
                throw new ArgumentNullException(nameof(getConjugates));
            }

            int conjugateClass = 0;
            Dictionary<T, int> classes = new();

            int count = 0;
            foreach (var item in items)
            {
                if (!classes.ContainsKey(item))
                {
                    foreach (var conjugate in getConjugates(item))
                    {
                        classes[conjugate] = conjugateClass;
                    }
                    conjugateClass++;
                }
                count++;
            }

            List<T> buffer = new(count);
            for (int i = 0; i < conjugateClass; i++)
            {
                buffer.AddRange(from keyValue in classes
                                where keyValue.Value == i
                                select keyValue.Key);
                yield return buffer.ToArray();
                buffer.Clear();
            }
        }

        public static IEnumerable<byte[]> GetEquivalenceClasses(Func<byte, IEnumerable<byte>> getConjugates)
        {
            int nextConjugateClass = 1;
            int[] conjugate = new int[0x100];

            for (byte i = 0; i < conjugate.Length; i++)
            {
                if (conjugate[i] == 0)
                {
                    foreach (var item in getConjugates(i))
                    {
                        conjugate[item] = nextConjugateClass;
                    }
                    nextConjugateClass++;
                }
            }

            List<byte> tempArray = new(0x100);
            for (byte i = 1; i < nextConjugateClass; i++)
            {
                for (int j = 0; j < conjugate.Length; j++)
                {
                    if (conjugate[j] == i)
                    {
                        tempArray.Add((byte)j);
                    }
                }
                yield return tempArray.ToArray();
                tempArray.Clear();
            }
        }

        public static IEnumerable<int[]> GetEquivalenceClasses(int length, Func<int, IEnumerable<int>> getConjugates)
        {
            int nextConjugateClass = 1;
            int[] conjugate = new int[length];

            for (int i = 0; i < conjugate.Length; i++)
            {
                if (conjugate[i] == 0)
                {
                    foreach (var item in getConjugates(i))
                    {
                        conjugate[item] = nextConjugateClass;
                    }
                    nextConjugateClass++;
                }
            }

            List<int> tempArray = new(length);
            for (int i = 1; i < nextConjugateClass; i++)
            {
                for (int j = 0; j < conjugate.Length; j++)
                {
                    if (conjugate[j] == i)
                    {
                        tempArray.Add((byte)j);
                    }
                }
                yield return tempArray.ToArray();
                tempArray.Clear();
            }
        }

        /// <summary>
        /// Counts amount of orbits using Cauchy-Frobenius theorem
        /// </summary>
        public static int GetOrbitsCount<T>(this IEnumerable<T> items, IEnumerable<Func<T, T>> groupActions, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;

            var groupSize = 0;

            var fixPoints = 0;

            foreach (var action in groupActions)
            {
                groupSize++;
                foreach (var item in items)
                {
                    if (comparer.Equals(item, action(item)))
                    {
                        fixPoints++;
                    }
                }
            }

            return fixPoints / groupSize;
        }

        public static T SingletonOrDefault<T>(this IEnumerable<T> items, T defaultItem = default)
        {
            using var enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return defaultItem;
            }

            var first = enumerator.Current;

            if (enumerator.MoveNext())
            {
                return defaultItem;
            }

            return first;
        }

        public static IEnumerable<IEnumerable<int>> GetIndexLists(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            var values = Enumerable.Range(0, length);

            IEnumerable<IEnumerable<int>> Inner(int n)
            {
                if (n == 0)
                {
                    return new[] { Enumerable.Empty<int>() };
                }
                else
                {
                    return from end in values
                           from rest in Inner(n - 1)
                           select rest.Append(end);
                }
            }

            return Inner(length);
        }

        public static IEnumerable<IEnumerable<T>> GetSequencesFrom<T>(this IEnumerable<T> items, int length)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            var array = items.ToArray();

            return GetIndexLists(length).Select(indecis => indecis.Select(index => array[index]));
        }

        public static IEnumerable<T> UntilNotDistinct<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            comparer ??= EqualityComparer<T>.Default;

            IEnumerable<T> Inner()
            {
                T previous = default;
                bool first = true;

                using var enumerator = items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    if (!first && comparer.Equals(previous, enumerator.Current))
                    {
                        yield break;
                    }
                    yield return enumerator.Current;
                    previous = enumerator.Current;
                    first = false;
                }
            }

            return Inner();
        }

        public static IDistribution<T> RandomItem<T>(this IEnumerable<T> items)
        {
            var count = items.Count();
            var indexDistribution = DiscreteUniformDistribution.Create(0, count - 1);
            return DiscreteDistribution<T>.Create(indexDistribution, items);
        }

        public static IEnumerable<IEnumerable<T>> GetPrefixes<T>(this IEnumerable<T> items)
        {
            if (items.TryGetNonEnumeratedCount(out var count))
            {
                for (int i = 0; i <= count; i++)
                {
                    yield return items.Take(i);
                }
            }
            else
            {
                for (int i = 0; i <= int.MaxValue; i++)
                {
                    bool reachedEnd = true;

                    IEnumerable<T> Prefix()
                    {
                        int j = 0;
                        foreach (var item in items)
                        {
                            yield return item;
                            j++;
                            if (j == i)
                            {
                                reachedEnd = false;
                                break;
                            }
                        }
                    }

                    yield return Prefix();

                    if (reachedEnd)
                    {
                        break;
                    }
                }
            }
        }
    }
}