using System.Collections.Generic;

namespace Nintenlord.Trees.Nodes
{
    public sealed class VoidNode<T> : IValuedTreeNode<T, VoidNode<T>>
    {
        private VoidNode()
        {

        }

        public T Value => throw new System.NotImplementedException();

        public bool HasValue => throw new System.NotImplementedException();

        public IEnumerable<VoidNode<T>> GetChildren()
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed class Leaf<T> : IValuedTreeNode<T, VoidNode<T>>
    {
        private readonly T value;

        public Leaf(T value)
        {
            this.value = value;
        }

        #region ITree<T> Members

        public IEnumerable<VoidNode<T>> GetChildren()
        {
            yield break;
        }

        public T Value => value;

        public bool HasValue => true;

        #endregion
    }
}
