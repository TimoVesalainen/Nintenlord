using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class DisjointUnionForest<TNode1, TNode2> : IForest<Either<TNode1, TNode2>>
    {
        readonly IForest<TNode1> forest1;
        readonly IForest<TNode2> forest2;

        public DisjointUnionForest(IForest<TNode1> forest1, IForest<TNode2> forest2)
        {
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
        }

        public IEnumerable<Either<TNode1, TNode2>> GetChildren(Either<TNode1, TNode2> node)
        {
            return node.Apply(
                nodeInner => forest1.GetChildren(nodeInner).Select(Either<TNode1, TNode2>.From0),
                nodeInner => forest2.GetChildren(nodeInner).Select(Either<TNode1, TNode2>.From1)
                );
        }
    }
    public sealed class DisjointUnionForest<TNode1, TNode2, TNode3> : IForest<Either<TNode1, TNode2, TNode3>>
    {
        readonly IForest<TNode1> forest1;
        readonly IForest<TNode2> forest2;
        readonly IForest<TNode3> forest3;

        public DisjointUnionForest(IForest<TNode1> forest1, IForest<TNode2> forest2, IForest<TNode3> forest3)
        {
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
            this.forest3 = forest3 ?? throw new ArgumentNullException(nameof(forest3));
        }

        public IEnumerable<Either<TNode1, TNode2, TNode3>> GetChildren(Either<TNode1, TNode2, TNode3> node)
        {
            return node.Apply(
                nodeInner => forest1.GetChildren(nodeInner).Select(Either<TNode1, TNode2, TNode3>.From0),
                nodeInner => forest2.GetChildren(nodeInner).Select(Either<TNode1, TNode2, TNode3>.From1),
                nodeInner => forest3.GetChildren(nodeInner).Select(Either<TNode1, TNode2, TNode3>.From2)
                );
        }
    }
}
