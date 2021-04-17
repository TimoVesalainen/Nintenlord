using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public class LambdaForest<TNode> : IForest<TNode>
    {
        private readonly Func<TNode, IEnumerable<TNode>> getChildren;

        public LambdaForest(Func<TNode, IEnumerable<TNode>> getChildren)
        {
            this.getChildren = getChildren;
        }

        public IEnumerable<TNode> GetChildren(TNode node)
        {
            return getChildren(node);
        }
    }
}
