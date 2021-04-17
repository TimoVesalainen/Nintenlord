using System;

namespace Nintenlord.Trees
{
    public class SelectTree<TNode, TInner> : SelectForest<TNode, TInner>, ITree<TNode>
    {
        public TNode Root { get; }

        public SelectTree(Func<TNode, TInner> toInner, Func<TInner, TNode> fromInner, ITree<TInner> tree)
            : base(toInner, fromInner, tree)
        {
            Root = fromInner(tree.Root);
        }
    }
}
