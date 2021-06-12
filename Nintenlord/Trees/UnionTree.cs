using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class UnionTree<TNode> : ITree<Maybe<TNode>>
    {
        private readonly ITree<TNode>[] trees;
        private readonly Maybe<TNode>[] roots;

        public UnionTree(IEnumerable<ITree<TNode>> trees)
        {
            this.trees = trees?.ToArray() ?? throw new ArgumentNullException(nameof(trees));
            this.roots = trees.Select(tree => Maybe<TNode>.Just(tree.Root)).ToArray();
        }

        public Maybe<TNode> Root => Maybe<TNode>.Nothing;

        public IEnumerable<Maybe<TNode>> GetChildren(Maybe<TNode> node)
        {
            return node.Select(realNode => trees.SelectMany(tree => tree.GetChildren(realNode).Select(Maybe<TNode>.Just)))
                       .GetValueOrDefault(roots);
        }
    }
}
