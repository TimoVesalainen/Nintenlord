using Nintenlord.Trees.Nodes;
using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Contains values in leafs only.
    /// </summary>
    /// <typeparam name="T">Type of the values to hold.</typeparam>
    public class LeafValuedBinaryTree<T> : ITree<BinaryTreeNode<T>>, IParentForest<BinaryTreeNode<T>>
    {
        public int Count { get; }
        public int MaxDepth { get; }
        public BinaryTreeNode<T> Root { get; }

        public LeafValuedBinaryTree(LeafValuedBinaryTree<T> left, LeafValuedBinaryTree<T> right)
        {
            Count = left.Count + right.Count;
            MaxDepth = Math.Max(left.MaxDepth, right.MaxDepth) + 1;
            Root = new BinaryTreeNode<T>(left.Root, right.Root);
        }

        public LeafValuedBinaryTree(T value)
        {
            Root = new BinaryTreeNode<T>(value);
            Count = 1;
            MaxDepth = 1;
        }

        public IEnumerable<BinaryTreeNode<T>> GetChildren(BinaryTreeNode<T> node)
        {
            return node.GetChildren();
        }

        public bool TryGetParent(BinaryTreeNode<T> child, out BinaryTreeNode<T> parent)
        {
            parent = child.Parent;
            return child.Parent != null;
        }
    }
}
