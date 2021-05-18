using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Trees
{
    public sealed class ArrayBinaryTree<T> : ITree<(int index, T item)>
    {
        //Index 0 is not used
        (bool hasValue, T value)[] nodes;

        public (int index, T item) Root => (1, nodes[1].value);

        public ArrayBinaryTree() : this(4)
        {

        }

        public ArrayBinaryTree(int capacity)
        {
            nodes = new (bool, T)[capacity];
        }

        public IEnumerable<(int index, T item)> GetChildren((int index, T item) node)
        {
            var leftIndex = GetLeftChildIndex(node.index);
            var rightIndex = GetRightChildIndex(node.index);

            if (TryGetValue(leftIndex, out var left))
            {
                yield return (leftIndex, left);
            }
            if (TryGetValue(rightIndex, out var right))
            {
                yield return (rightIndex, right);
            }
        }

        public bool HasValue(int index)
        {
            return index < nodes.Length && nodes[index].hasValue;
        }

        public bool TryGetValue(int index, out T value)
        {
            if (nodes[index].hasValue)
            {
                value = nodes[index].value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public void SetLeftChild(int parentIndex, T item)
        {
            SetItemToIndex(GetLeftChildIndex(parentIndex), item);
        }

        public void SetRightChild(int parentIndex, T item)
        {
            SetItemToIndex(GetRightChildIndex(parentIndex), item);
        }

        private void SetItemToIndex(int index, T item)
        {
            if (index >= nodes.Length)
            {
                var newLength = nodes.Length;

                while (newLength <= index)
                {
                    newLength *= 2;
                }

                var newArray = new (bool, T)[newLength];
                Array.Copy(nodes, newArray, nodes.Length);
                nodes = newArray;
            }

            nodes[index].value = item;
            nodes[index].hasValue = true;
        }

        private IEnumerable<int> GetParentIndicis(int startIndex)
        {
            while (startIndex > 0)
            {
                yield return startIndex;
                startIndex >>= 1;
            }
        }

        public IEnumerable<T> ToRoot(int startIndex)
        {
            return GetParentIndicis(startIndex).Select(index => nodes[index].value);
        }

        private static int GetLeftChildIndex(int index)
        {
            return index * 2;
        }

        private static int GetRightChildIndex(int index)
        {
            return index * 2 + 1;
        }
    }
}
