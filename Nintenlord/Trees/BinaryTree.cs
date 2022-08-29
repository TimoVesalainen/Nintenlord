using Nintenlord.Trees.Nodes;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public class BinaryTree<T> : ITree<BinaryTreeNode<T>>
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
    }
}
