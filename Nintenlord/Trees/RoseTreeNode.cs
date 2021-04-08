using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class RoseTreeNode<T>
    {
        private readonly RoseTreeNode<T>[] children;

        public T Value { get; }
        public int ChildCount => children.Length;
        public RoseTreeNode<T> this[int index] => children[index];

        public RoseTreeNode(IEnumerable<RoseTreeNode<T>> children, T value)
        {
            this.children = children.ToArray();
            Value = value;
        }

        public RoseTreeNode(T value)
        {
            this.children = new RoseTreeNode<T>[0];
            Value = value;
        }

        public IEnumerable<RoseTreeNode<T>> GetChildren()
        {
            return children;
        }
        public override string ToString()
        {
            return $"{{Node of {Value}}}";
        }
    }
}
