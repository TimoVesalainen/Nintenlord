using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class RoseTree<T> : ITree<RoseTreeNode<T>>
    {
        public RoseTreeNode<T> Root { get; }

        public RoseTree(RoseTreeNode<T> root)
        {
            Root = root;
        }

        public IEnumerable<RoseTreeNode<T>> GetChildren(RoseTreeNode<T> node)
        {
            return node.GetChildren();
        }
    }
}
