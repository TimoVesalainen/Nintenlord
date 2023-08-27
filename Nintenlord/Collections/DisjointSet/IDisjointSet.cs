using System.Collections.Generic;

namespace Nintenlord.Collections.DisjointSet
{
    /// <summary>
    /// Set divided to subsets with fast unions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDisjointSet<T>
    {
        IEnumerable<T> Items { get; }
        bool AreSameSet(T item1, T item2);
        T FindRepresentative(T item);

        /// <summary>
        /// Joins sets represented by items
        /// </summary>
        /// <returns>True if collection was changed, false otherwise</returns>
        bool Union(T item1, T item2);

        int ElementCount { get; }
    }
}