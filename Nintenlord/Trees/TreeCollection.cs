using Nintenlord.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Nintenlord.Trees
{
    public sealed class TreeCollection<T1, T2> : ITree<Either<T1, T2, Unit>>
    {
        readonly ITree<T1> tree1;
        readonly ITree<T2> tree2;
        readonly Either<T1, T2, Unit>[] treeRoots;

        public Either<T1, T2, Unit> Root => Unit.Default;

        public TreeCollection(ITree<T1> tree1, ITree<T2> tree2)
        {
            this.tree1 = tree1;
            this.tree2 = tree2;
            treeRoots = new Either<T1, T2, Unit>[] { tree1.Root, tree2.Root };
        }

        public IEnumerable<Either<T1, T2, Unit>> GetChildren(Either<T1, T2, Unit> node)
        {
            return node.Apply(
                node1 => tree1.GetChildren(node1).Select(Either<T1, T2, Unit>.From0),
                node2 => tree2.GetChildren(node2).Select(Either<T1, T2, Unit>.From1),
                root => treeRoots);
        }
    }
}
