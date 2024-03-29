﻿using System.Collections.Generic;

namespace Nintenlord.Collections.Lists.Change
{
    public interface IListChange<T>
    {
        IReadOnlyList<T> Original { get; }
        IReadOnlyList<T> Next { get; }
        int OriginalIndex { get; }
        int OriginalLength { get; }
        int NextIndex { get; }
        int NextLength { get; }

        public static IListChange<T> Moved(int from, int to, int amount, IReadOnlyList<T> original, IReadOnlyList<T> next)
        {
            return new Moved<T>(amount, original, next, from, to);
        }

        public static IListChange<T> Removed(int from, int amount, IReadOnlyList<T> original, IReadOnlyList<T> next)
        {
            return new Removed<T>(from, original, next, amount);
        }

        public static IListChange<T> Added(int to, int amount, IReadOnlyList<T> original, IReadOnlyList<T> next)
        {
            return new Added<T>(to, original, next, amount);
        }
    }
}
