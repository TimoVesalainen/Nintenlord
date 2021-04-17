using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class UnionForest<TNode> : IForest<TNode>
    {
        private readonly IForest<TNode>[] forests;

        public UnionForest(IEnumerable<IForest<TNode>> forests)
        {
            this.forests = forests?.ToArray() ?? throw new ArgumentNullException(nameof(forests));
        }

        public IEnumerable<TNode> GetChildren(TNode node)
        {
            return forests.SelectMany(forest => forest.GetChildren(node));
        }
    }
}
