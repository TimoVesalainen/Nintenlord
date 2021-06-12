using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class ArrayTree<T> : ITree<(int index, T item)>
    {
        (bool hasValue, T value)[] nodes;

        public (int index, T item) Root => (treeStructure.Root, nodes[treeStructure.Root].value);

        readonly ITree<int> treeStructure;

        public int RootIndex => treeStructure.Root;

        public ArrayTree(ITree<int> treeStructure) : this(4, treeStructure)
        {

        }

        public ArrayTree(int capacity, ITree<int> treeStructure)
        {
            nodes = new (bool, T)[capacity];
            this.treeStructure = treeStructure;
        }

        public IEnumerable<(int index, T item)> GetChildren((int index, T item) node)
        {
            foreach (var index in treeStructure.GetChildren(node.index))
            {
                if (TryGetValue(index, out var child))
                {
                    yield return (index, child);
                }
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

        public bool SetChild(int parentIndex, int childIndex, T item)
        {
            var index = treeStructure.GetChildren(parentIndex).Skip(childIndex).DefaultIfEmpty(-1).First();

            if (index >= 0)
            {
                SetItemToIndex(index, item);
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SetItemToIndex(int index, T item)
        {
            if (index >= nodes.Length)
            {
                var newLength = nodes.Length;

                while (newLength <= index)
                {
                    newLength *= 2;
                }

                Array.Resize(ref nodes, newLength);
            }

            nodes[index].value = item;
            nodes[index].hasValue = true;
        }

        /// <remarks>Indecis are no longer valid after this</remarks>
        public static ArrayTree<T> AddNewRoot(ArrayTree<T> old, T newRoot)
        {
            int childMaxCount;
            switch (old.treeStructure)
            {
                case IndexBinaryTree _:
                    childMaxCount = 2;
                    break;
                case IndexNTree nTree:
                    childMaxCount = nTree.ChildCount;
                    break;
                default:
                    throw new ArgumentException("No way to know childCount");
            }

            var newTree = old.AddRoot((-1, newRoot));

            return newTree.ConvertTo(newTree.Root, childMaxCount, tuple => tuple.Item2);
        }
    }
}
