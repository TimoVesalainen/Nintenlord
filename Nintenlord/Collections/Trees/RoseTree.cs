// -----------------------------------------------------------------------
// <copyright file="RoseTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Collections.Trees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class RoseTree<T> : IValuedTreeNode<T>
    {
        private readonly RoseTree<T>[] children;
        private readonly T value;
        private readonly bool hasValue;

        public RoseTree<T> this[int index] => children[index];

        public int BranchCount => children.Length;


        public RoseTree(IEnumerable<RoseTree<T>> children)
        {
            this.hasValue = false;
            this.value = default(T);
            this.children = children.ToArray();
        }

        public RoseTree(IEnumerable<RoseTree<T>> children, T value)
        {
            this.hasValue = true;
            this.value = value;
            this.children = children.ToArray();
        }

        #region IValuedTree<T> Members

        public T Value
        {
            get
            {
                if (!this.HasValue)
                {
                    throw new InvalidOperationException();
                }
                return value;
            }
        }

        public bool HasValue => hasValue;

        #endregion

        #region ITree<IValuedTree<T>> Members

        public IEnumerable<IValuedTreeNode<T>> GetChildren()
        {
            return children;
        }

        #endregion

        public static RoseTree<T> GetRoseTree(Func<T, IEnumerable<T>> getChildren, T top)
        {
            return new RoseTree<T>(getChildren(top).Select(x => GetRoseTree(getChildren, x)), top);
        }
    }
}
