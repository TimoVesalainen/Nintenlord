using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class LambdaTree<TNode> : LambdaForest<TNode>, ITree<TNode>
    {
        public TNode Root { get; }

        public LambdaTree(TNode root, Func<TNode, IEnumerable<TNode>> getChildren)
            : base(getChildren)
        {
            Root = root;
        }
    }
}
