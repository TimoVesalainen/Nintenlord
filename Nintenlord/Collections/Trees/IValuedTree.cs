// -----------------------------------------------------------------------
// <copyright file="IValuedTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Collections.Trees
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IValuedTree<out T> : ITree<IValuedTree<T>>
    {
        T Value { get; }
        bool HasValue { get; }
    }
}
