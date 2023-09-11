using System.Collections.Generic;

namespace Nintenlord.Trees.Nodes
{
    public sealed class BinaryTreeNode<T> : IValuedTreeNode<T, BinaryTreeNode<T>>
    {
        public T Value { get; }

        public BinaryTreeNode<T> Parent { get; private set; }
        public BinaryTreeNode<T> Left { get; }
        public BinaryTreeNode<T> Right { get; }
        public bool IsLeaf { get; }
        public bool HasValue { get; }

        public BinaryTreeNode()
        {

        }
        public BinaryTreeNode(T value)
        {
            Value = value;
            IsLeaf = true;
            HasValue = true;
        }
        public BinaryTreeNode(BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            Left = left;
            Right = right;
            left.Parent = this;
            right.Parent = this;
            IsLeaf = false;
            HasValue = false;
        }
        public BinaryTreeNode(BinaryTreeNode<T> left, BinaryTreeNode<T> right, T value)
        {
            Left = left;
            Right = right;
            left.Parent = this;
            right.Parent = this;
            Value = value;
            IsLeaf = false;
            HasValue = true;
        }

        public IEnumerable<BinaryTreeNode<T>> GetChildren()
        {
            if (Left != null)
            {
                yield return Left;
            }
            if (Right != null)
            {
                yield return Right;
            }
        }
    }
}
