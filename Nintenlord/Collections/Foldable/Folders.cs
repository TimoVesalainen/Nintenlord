﻿using Nintenlord.Utility;
using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Foldable
{
    public static class Folders
    {
        public static readonly AllFolder<bool> And = new(x => x);
        public static AllFolder<T> All<T>(Predicate<T> predicate) => new(predicate);
        public static readonly AnyFolder<bool> Or = new(x => x);
        public static AnyFolder<T> Any<T>(Predicate<T> predicate) => new(predicate);

        public static readonly IFolder<int, int, int> SumI = new FunctionFolder<int, int, int>(0, (x, y) => x + y, x => x);
        public static readonly IFolder<long, long, long> SumL = new FunctionFolder<long, long, long>(0, (x, y) => x + y, x => x);
        public static readonly IFolder<float, float, float> SumF = new FunctionFolder<float, float, float>(0, (x, y) => x + y, x => x);
        public static readonly IFolder<double, double, double> SumD = new FunctionFolder<double, double, double>(0, (x, y) => x + y, x => x);

        public static readonly IFolder<int, int, int> ProductI = new FunctionFolder<int, int, int>(0, (x, y) => x * y, x => x);
        public static readonly IFolder<long, long, long> ProductL = new FunctionFolder<long, long, long>(0, (x, y) => x * y, x => x);
        public static readonly IFolder<float, float, float> ProductF = new FunctionFolder<float, float, float>(0, (x, y) => x * y, x => x);
        public static readonly IFolder<double, double, double> ProductD = new FunctionFolder<double, double, double>(0, (x, y) => x * y, x => x);

        public static EmptyFolder<T> Empty<T>() => EmptyFolder<T>.Instance;
        public static CountIntFolder<T> CountI<T>() => CountIntFolder<T>.Value;
        public static CountLongFolder<T> CountL<T>() => CountLongFolder<T>.Value;

        public static readonly MeanIntFolder AverageInteger = MeanIntFolder.Instance;
        public static readonly MeanFloatFolder AverageFloat = MeanFloatFolder.Instance;

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

        public static FirstFolder<T> First<T>() => FirstFolder<T>.Instance;
        public static FirstFolder<T> First<T>(Predicate<T> predicate) => new FirstFolder<T>(predicate);
        public static LastFolder<T> Last<T>() => LastFolder<T>.Instance;
        public static LastFolder<T> Last<T>(Predicate<T> predicate) => new LastFolder<T>(predicate);

        public static AnyFolder<T> Contains<T>(T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            return new(otherItem => comparer.Equals(otherItem, item));
        }
        public static FindFolder<T> Find<T>(Predicate<T> predicate) => new(predicate);
        public static FirstIndexOfFolder<T> FirstIndexOf<T>(Predicate<T> predicate) => new(predicate);
        public static LastIndexOfFolder<T> LastIndexOf<T>(Predicate<T> predicate) => new(predicate);
        public static IFolder<(TKey, TValue), Maybe<(TKey, TValue)>, Maybe<TValue>> LookUp<TKey, TValue>(TKey key, IEqualityComparer<TKey> comparer = null)
        {
            comparer ??= EqualityComparer<TKey>.Default;
            return new FindFolder<(TKey, TValue)>(pair => comparer.Equals(key, pair.Item1)).Select(pairMaybe => pairMaybe.Select(pair => pair.Item2));
        }

        public static ReturnFolder<T, TResult> Return<T, TResult>(TResult result) => ReturnFolder<T, TResult>.Create(result);
    }
}
