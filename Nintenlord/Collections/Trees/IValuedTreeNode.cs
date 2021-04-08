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
    public interface IValuedTreeNode<out T> : ITreeNode<IValuedTreeNode<T>>
    {
        T Value { get; }
        bool HasValue { get; }
    }
}
