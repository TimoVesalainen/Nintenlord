using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class IndexBinaryTree : ITree<int>, IParentForest<int>
    {
        private readonly static Lazy<IndexBinaryTree> instance = new(
            () => new IndexBinaryTree(), isThreadSafe: true);

        public static IndexBinaryTree Instance => instance.Value;

        public int Root => 1;

        private IndexBinaryTree() { }

        public IEnumerable<int> GetChildren(int node)
        {
            var firstChild = GetFirstChild(node);
            yield return firstChild;
            yield return firstChild + 1;
        }

        public IEnumerable<int> GetParentIndicis(int startIndex)
        {
            while (startIndex > 0)
            {
                yield return startIndex;
                startIndex >>= 1;
            }
        }

        public bool TryGetParent(int child, out int parent)
        {
            parent = child >> 1;
            return child != Root;
        }

        public int GetFirstChild(int node)
        {
            return node << 1;
        }

        public int GetSecondChild(int node)
        {
            return node << 1 + 1;
        }
    }
}
