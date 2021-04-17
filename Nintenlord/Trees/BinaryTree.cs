using Nintenlord.Trees.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Contains values in leafs only.
    /// </summary>
    /// <typeparam name="T">Type of the values to hold.</typeparam>
    public class BinaryTree<T> : ITree<BinaryTreeNode<T>>
    {
        public int Count { get; }
        public int MaxDepth { get; }
        public BinaryTreeNode<T> Root { get; }

        public BinaryTree(BinaryTree<T> left, BinaryTree<T> right)
        {
            Count = left.Count + right.Count;
            MaxDepth = Math.Max(left.MaxDepth, right.MaxDepth) + 1;
            Root = new BinaryTreeNode<T>(left.Root, right.Root);
        }

        public BinaryTree(T value)
        {
            Root = new BinaryTreeNode<T>(value);
            Count = 1;
            MaxDepth = 1;
        }

        public IEnumerable<BinaryTreeNode<T>> GetChildren(BinaryTreeNode<T> node)
        {
            return node.GetChildren();
        }

        public static implicit operator Dictionary<T, bool[]>(BinaryTree<T> tree)
        {
            Dictionary<T, bool[]> dict = new Dictionary<T, bool[]>();
            tree.Root.AddLeafValues(dict, new bool[] { });
            return dict;
        }
    }
}
