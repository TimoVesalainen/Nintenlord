using Nintenlord.Trees.Nodes;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class BinaryTree<T> : ITree<BinaryTreeNode<T>>, IParentForest<BinaryTreeNode<T>>
    {
        public BinaryTreeNode<T> Root { get; }

        public BinaryTree(BinaryTreeNode<T> root)
        {
            Root = root;
        }

        public IEnumerable<BinaryTreeNode<T>> GetChildren(BinaryTreeNode<T> node)
        {
            return node.GetChildren();
        }

        public bool TryGetParent(BinaryTreeNode<T> child, out BinaryTreeNode<T> parent)
        {
            parent = child.Parent;
            return parent != null;
        }
    }
}
