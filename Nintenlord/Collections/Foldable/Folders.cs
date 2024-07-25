using Nintenlord.Collections.EqualityComparer;
using Nintenlord.Collections.Foldable.Collections;
using Nintenlord.Collections.Foldable.Comparers;
using Nintenlord.Collections.Foldable.EqualityComparers;
using Nintenlord.Collections.Foldable.Numerics;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Collections.Foldable
{
    public static class Folders
    {
        public static readonly AllFolder<bool> And = new(x => x);
        public static AllFolder<T> All<T>(Predicate<T> predicate) => new(predicate);
        public static readonly AnyFolder<bool> Or = new(x => x);
        public static AnyFolder<T> Any<T>(Predicate<T> predicate) => new(predicate);

        public static SumFolder<TNumber> Sum<TNumber>() where TNumber : IAdditiveIdentity<TNumber, TNumber>, IAdditionOperators<TNumber, TNumber, TNumber> => SumFolder<TNumber>.Instance;
        public static readonly SumFolder<int> SumI = Sum<int>();
        public static readonly SumFolder<long> SumL = Sum<long>();
        public static readonly SumFolder<BigInteger> SumBig = Sum<BigInteger>();
        public static readonly SumFolder<float> SumF = Sum<float>();
        public static readonly SumFolder<double> SumD = Sum<double>();

        public static ProductFolder<TNumber> Product<TNumber>() where TNumber : IMultiplicativeIdentity<TNumber, TNumber>, IMultiplyOperators<TNumber, TNumber, TNumber> => ProductFolder<TNumber>.Instance;
        public static readonly ProductFolder<int> ProductI = Product<int>();
        public static readonly ProductFolder<long> ProductL = Product<long>();
        public static readonly ProductFolder<float> ProductF = Product<float>();
        public static readonly ProductFolder<double> ProductD = Product<double>();

        public static EmptyFolder<T> Empty<T>() => EmptyFolder<T>.Instance;
        public static CountFolder<T, int> CountI<T>() => CountFolder<T, int>.Instance;
        public static AmountOfFolder<T, int> CountI<T>(Predicate<T> predicate) => new AmountOfFolder<T, int>(predicate);
        public static CountFolder<T, long> CountL<T>() => CountFolder<T, long>.Instance;
        public static AmountOfFolder<T, long> CountL<T>(Predicate<T> predicate) => new AmountOfFolder<T, long>(predicate);
        public static CountFolder<T, BigInteger> CountBig<T>() => CountFolder<T, BigInteger>.Instance;
        public static AmountOfFolder<T, BigInteger> CountBig<T>(Predicate<T> predicate) => new AmountOfFolder<T, BigInteger>(predicate);

        public static readonly MeanIntFolder AverageInteger = MeanIntFolder.Instance;
        public static readonly MeanFloatFolder AverageFloat = MeanFloatFolder.Instance;
        public static readonly VarianceIntFolder VarianceInteger = VarianceIntFolder.Instance;
        public static readonly VarianceFloatFolder VarianceFloat = VarianceFloatFolder.Instance;

        public static MinFolder<T> Min<T>() => MinFolder<T>.Default;
        public static MinFolder<T> MinBy<T>(IComparer<T> comparer) => MinFolder<T>.Create(comparer);
        public static MaxFolder<T> Max<T>() => MaxFolder<T>.Default;
        public static MaxFolder<T> MaxBy<T>(IComparer<T> comparer) => MaxFolder<T>.Create(comparer);
        public static IFolder<T, (Maybe<T>, Maybe<T>), Maybe<(T min, T max)>> MinMax<T>()
        {
            return Min<T>().Combine(Max<T>(), (x, y) => MaybeHelpers.Zip(x, y, (a, b) => (a, b)));
        }

        public static ImmutableListFolder<T> ImmutableList<T>() => ImmutableListFolder<T>.Instance;
        public static ImmutableHashSetFolder<T> ImmutableHashSet<T>() => ImmutableHashSetFolder<T>.Instance;
        public static ImmutableDictionaryFolder<TKey, TValue> ImmutableDictionary<TKey, TValue>() => ImmutableDictionaryFolder<TKey, TValue>.Instance;

        public static FirstFolder<T> First<T>() => FirstFolder<T>.Instance;
        public static FirstFolder<T> First<T>(Predicate<T> predicate) => new FirstFolder<T>(predicate);
        public static LastFolder<T> Last<T>() => LastFolder<T>.Instance;
        public static LastFolder<T> Last<T>(Predicate<T> predicate) => new LastFolder<T>(predicate);

        public static AnyFolder<T> Contains<T>(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            return new(comparer.GetPredicate(item));
        }
        public static FirstFolder<T> Find<T>(Predicate<T> predicate) => new(predicate);
        public static FirstIndexOfFolder<T> FirstIndexOf<T>(Predicate<T> predicate) => new(predicate);
        public static LastIndexOfFolder<T> LastIndexOf<T>(Predicate<T> predicate) => new(predicate);
        public static IFolder<(TKey, TValue), Maybe<(TKey, TValue)>, Maybe<TValue>> LookUp<TKey, TValue>(TKey key, IEqualityComparer<TKey> comparer = null)
        {
            comparer ??= EqualityComparer<TKey>.Default;
            return new FirstFolder<(TKey, TValue)>(pair => comparer.Equals(key, pair.Item1)).Select(pairMaybe => pairMaybe.Select(pair => pair.Item2));
        }

        public static ReturnFolder<T, TResult> Return<T, TResult>(TResult result) => ReturnFolder<T, TResult>.Create(result);
    }
}
