using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class LambdaTree<TNode> : ITree<TNode>
    {
        private readonly Func<TNode, IEnumerable<TNode>> getChildren;

        public LambdaTree(TNode root, Func<TNode, IEnumerable<TNode>> getChildren)
        {
            this.getChildren = getChildren;
            Root = root;
        }

        public TNode Root { get; }

        public IEnumerable<TNode> GetChildren(TNode node)
        {
            return getChildren(node);
        }
    }
}
