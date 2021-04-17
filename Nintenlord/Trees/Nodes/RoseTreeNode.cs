using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees.Nodes
{
    public sealed class RoseTreeNode<T> : IValuedTreeNode<T, RoseTreeNode<T>>
    {
        private readonly RoseTreeNode<T>[] children;
        private readonly T value;
        private readonly bool hasValue;

        public RoseTreeNode<T> this[int index] => children[index];

        public int BranchCount => children.Length;


        public RoseTreeNode(IEnumerable<RoseTreeNode<T>> children)
        {
            hasValue = false;
            value = default;
            this.children = children.ToArray();
        }

        public RoseTreeNode(IEnumerable<RoseTreeNode<T>> children, T value)
        {
            hasValue = true;
            this.value = value;
            this.children = children.ToArray();
        }

        #region IValuedTree<T> Members

        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException();
                }
                return value;
            }
        }

        public bool HasValue => hasValue;

        #endregion

        #region ITree<IValuedTree<T>> Members

        public IEnumerable<RoseTreeNode<T>> GetChildren()
        {
            return children;
        }

        #endregion

        public static RoseTreeNode<T> GetRoseTree(Func<T, IEnumerable<T>> getChildren, T top)
        {
            return new RoseTreeNode<T>(getChildren(top).Select(x => GetRoseTree(getChildren, x)), top);
        }
    }
}
