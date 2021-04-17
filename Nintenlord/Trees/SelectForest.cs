using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class SelectForest<TNode, TInner> : IForest<TNode>
    {
        private readonly Func<TNode, TInner> toInner;
        private readonly Func<TInner, TNode> fromInner;
        private readonly IForest<TInner> forest;

        public SelectForest(Func<TNode, TInner> toInner, Func<TInner, TNode> fromInner, IForest<TInner> forest)
        {
            this.toInner = toInner ?? throw new ArgumentNullException(nameof(toInner));
            this.fromInner = fromInner ?? throw new ArgumentNullException(nameof(fromInner));
            this.forest = forest ?? throw new ArgumentNullException(nameof(forest));
        }

        public IEnumerable<TNode> GetChildren(TNode node)
        {
            return forest.GetChildren(toInner(node)).Select(fromInner);
        }
    }
}
