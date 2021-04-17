using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees.Nodes
{
    public class BinaryTreeNode<T> : IValuedTreeNode<T, BinaryTreeNode<T>>
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

        public void AddLeafValues(ICollection<T> collection)
        {
            if (HasValue)
            {
                collection.Add(Value);
            }
            if (Left != null)
            {
                Left.AddLeafValues(collection);
            }
            if (Right != null)
            {
                Right.AddLeafValues(collection);
            }
        }

        public void AddLeafValues(IDictionary<T, bool[]> values, IList<bool> branches)
        {
            if (HasValue)
            {
                values[Value] = branches.ToArray();
            }
            if (Left != null)
            {
                branches.Add(false);
                Left.AddLeafValues(values, branches);
                branches.RemoveAt(branches.Count - 1);

            }
            if (Right != null)
            {
                branches.Add(true);
                Right.AddLeafValues(values, branches);
                branches.RemoveAt(branches.Count - 1);
            }
        }

        public IEnumerable<BinaryTreeNode<T>> GetChildren()
        {
            yield return Left;
            yield return Right;
        }
    }
}
