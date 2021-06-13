using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class IndexBinaryTree : ITree<int>
    {
        private readonly static Lazy<IndexBinaryTree> instance = new Lazy<IndexBinaryTree>(
            () => new IndexBinaryTree(), isThreadSafe: true);

        public static IndexBinaryTree Instance => instance.Value;

        public int Root => 1;

        private IndexBinaryTree() { }

        public IEnumerable<int> GetChildren(int node)
        {
            var firstChild = node << 1;
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
    }
}
