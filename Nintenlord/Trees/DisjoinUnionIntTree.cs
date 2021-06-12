using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Version of <c>DisjoinUnionTree</c> that uses <c>IntEither</c>
    /// </summary>
    public sealed class DisjoinUnionIntTree : ITree<IntEither>
    {
        readonly ITree<int>[] trees;
        readonly Func<int, IEnumerable<IntEither>>[] getChildren;
        readonly IntEither[] treeRoots;
        readonly IntEither root;

        public IntEither Root => root;

        public DisjoinUnionIntTree(IEnumerable<ITree<int>> trees)
        {
            this.trees = trees.ToArray();
            this.treeRoots = this.trees.Select((t, i) => new IntEither(t.Root, i)).ToArray();

            this.getChildren = this.trees.Select((t, i) => (Func<int, IEnumerable<IntEither>>)(n => t.GetChildren(n).Select(c => new IntEither(i, c))))
                .Append(_ => treeRoots)
                .ToArray();

            root = new IntEither(0, this.trees.Length);
        }

        public IEnumerable<IntEither> GetChildren(IntEither node)
        {
            return node.Apply(getChildren);
        }
    }
}
