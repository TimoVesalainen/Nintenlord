using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Collections.Lists.Change
{
    public interface IListChange<T>
    {
        IList<T> Original { get; }
        IList<T> Next { get; }
        int OriginalIndex { get; }
        int OriginalLength { get; }
        int NextIndex { get; }
        int NextLength { get; }

        public static IListChange<T> Moved(int from, int to, int amount, IList<T> original, IList<T> next)
        {
            return new Moved<T>(amount, original, next, from, to);
        }

        public static IListChange<T> Removed(int from, int amount, IList<T> original, IList<T> next)
        {
            return new Removed<T>(from, original, next, amount);
        }

        public static IListChange<T> Added(int to, int amount, IList<T> original, IList<T> next)
        {
            return new Added<T>(to, original, next, amount);
        }
    }
}
