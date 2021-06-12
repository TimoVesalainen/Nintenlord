
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Nintenlord.Trees
{
    public sealed class DisjointUnionForest<T0, T1> : IForest<Either<T0, T1>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
        }

        public IEnumerable<Either<T0, T1>> GetChildren(Either<T0, T1> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1>.From1));
        }
    }

    public sealed class DisjoinUnionTree<T0, T1> : ITree<Either<T0, T1, Unit>>
    {
        readonly ITree<T0> tree0;
        readonly ITree<T1> tree1;
        readonly Either<T0, T1, Unit>[] treeRoots;

        public Either<T0, T1, Unit> Root => Unit.Default;

        public DisjoinUnionTree(ITree<T0> tree0, ITree<T1> tree1)
        {
            this.tree0 = tree0 ?? throw new ArgumentNullException(nameof(tree0));
            this.tree1 = tree1 ?? throw new ArgumentNullException(nameof(tree1));
            treeRoots = new Either<T0, T1, Unit>[] { tree0.Root, tree1.Root };
        }

        public IEnumerable<Either<T0, T1, Unit>> GetChildren(Either<T0, T1, Unit> node)
        {
            return node.Apply(nodeInner => tree0.GetChildren(nodeInner).Select(Either<T0, T1, Unit>.From0), nodeInner => tree1.GetChildren(nodeInner).Select(Either<T0, T1, Unit>.From1),
                        _ => treeRoots);
        }
    }
    public sealed class DisjointUnionForest<T0, T1, T2> : IForest<Either<T0, T1, T2>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;
        readonly IForest<T2> forest2;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1, IForest<T2> forest2)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
        }

        public IEnumerable<Either<T0, T1, T2>> GetChildren(Either<T0, T1, T2> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1, T2>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1, T2>.From1), nodeInner => forest2.GetChildren(nodeInner).Select(Either<T0, T1, T2>.From2));
        }
    }

    public sealed class DisjoinUnionTree<T0, T1, T2> : ITree<Either<T0, T1, T2, Unit>>
    {
        readonly ITree<T0> tree0;
        readonly ITree<T1> tree1;
        readonly ITree<T2> tree2;
        readonly Either<T0, T1, T2, Unit>[] treeRoots;

        public Either<T0, T1, T2, Unit> Root => Unit.Default;

        public DisjoinUnionTree(ITree<T0> tree0, ITree<T1> tree1, ITree<T2> tree2)
        {
            this.tree0 = tree0 ?? throw new ArgumentNullException(nameof(tree0));
            this.tree1 = tree1 ?? throw new ArgumentNullException(nameof(tree1));
            this.tree2 = tree2 ?? throw new ArgumentNullException(nameof(tree2));
            treeRoots = new Either<T0, T1, T2, Unit>[] { tree0.Root, tree1.Root, tree2.Root };
        }

        public IEnumerable<Either<T0, T1, T2, Unit>> GetChildren(Either<T0, T1, T2, Unit> node)
        {
            return node.Apply(nodeInner => tree0.GetChildren(nodeInner).Select(Either<T0, T1, T2, Unit>.From0), nodeInner => tree1.GetChildren(nodeInner).Select(Either<T0, T1, T2, Unit>.From1), nodeInner => tree2.GetChildren(nodeInner).Select(Either<T0, T1, T2, Unit>.From2),
                        _ => treeRoots);
        }
    }
    public sealed class DisjointUnionForest<T0, T1, T2, T3> : IForest<Either<T0, T1, T2, T3>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;
        readonly IForest<T2> forest2;
        readonly IForest<T3> forest3;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1, IForest<T2> forest2, IForest<T3> forest3)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
            this.forest3 = forest3 ?? throw new ArgumentNullException(nameof(forest3));
        }

        public IEnumerable<Either<T0, T1, T2, T3>> GetChildren(Either<T0, T1, T2, T3> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3>.From1), nodeInner => forest2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3>.From2), nodeInner => forest3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3>.From3));
        }
    }

    public sealed class DisjoinUnionTree<T0, T1, T2, T3> : ITree<Either<T0, T1, T2, T3, Unit>>
    {
        readonly ITree<T0> tree0;
        readonly ITree<T1> tree1;
        readonly ITree<T2> tree2;
        readonly ITree<T3> tree3;
        readonly Either<T0, T1, T2, T3, Unit>[] treeRoots;

        public Either<T0, T1, T2, T3, Unit> Root => Unit.Default;

        public DisjoinUnionTree(ITree<T0> tree0, ITree<T1> tree1, ITree<T2> tree2, ITree<T3> tree3)
        {
            this.tree0 = tree0 ?? throw new ArgumentNullException(nameof(tree0));
            this.tree1 = tree1 ?? throw new ArgumentNullException(nameof(tree1));
            this.tree2 = tree2 ?? throw new ArgumentNullException(nameof(tree2));
            this.tree3 = tree3 ?? throw new ArgumentNullException(nameof(tree3));
            treeRoots = new Either<T0, T1, T2, T3, Unit>[] { tree0.Root, tree1.Root, tree2.Root, tree3.Root };
        }

        public IEnumerable<Either<T0, T1, T2, T3, Unit>> GetChildren(Either<T0, T1, T2, T3, Unit> node)
        {
            return node.Apply(nodeInner => tree0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, Unit>.From0), nodeInner => tree1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, Unit>.From1), nodeInner => tree2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, Unit>.From2), nodeInner => tree3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, Unit>.From3),
                        _ => treeRoots);
        }
    }
    public sealed class DisjointUnionForest<T0, T1, T2, T3, T4> : IForest<Either<T0, T1, T2, T3, T4>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;
        readonly IForest<T2> forest2;
        readonly IForest<T3> forest3;
        readonly IForest<T4> forest4;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1, IForest<T2> forest2, IForest<T3> forest3, IForest<T4> forest4)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
            this.forest3 = forest3 ?? throw new ArgumentNullException(nameof(forest3));
            this.forest4 = forest4 ?? throw new ArgumentNullException(nameof(forest4));
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4>> GetChildren(Either<T0, T1, T2, T3, T4> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4>.From1), nodeInner => forest2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4>.From2), nodeInner => forest3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4>.From3), nodeInner => forest4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4>.From4));
        }
    }

    public sealed class DisjoinUnionTree<T0, T1, T2, T3, T4> : ITree<Either<T0, T1, T2, T3, T4, Unit>>
    {
        readonly ITree<T0> tree0;
        readonly ITree<T1> tree1;
        readonly ITree<T2> tree2;
        readonly ITree<T3> tree3;
        readonly ITree<T4> tree4;
        readonly Either<T0, T1, T2, T3, T4, Unit>[] treeRoots;

        public Either<T0, T1, T2, T3, T4, Unit> Root => Unit.Default;

        public DisjoinUnionTree(ITree<T0> tree0, ITree<T1> tree1, ITree<T2> tree2, ITree<T3> tree3, ITree<T4> tree4)
        {
            this.tree0 = tree0 ?? throw new ArgumentNullException(nameof(tree0));
            this.tree1 = tree1 ?? throw new ArgumentNullException(nameof(tree1));
            this.tree2 = tree2 ?? throw new ArgumentNullException(nameof(tree2));
            this.tree3 = tree3 ?? throw new ArgumentNullException(nameof(tree3));
            this.tree4 = tree4 ?? throw new ArgumentNullException(nameof(tree4));
            treeRoots = new Either<T0, T1, T2, T3, T4, Unit>[] { tree0.Root, tree1.Root, tree2.Root, tree3.Root, tree4.Root };
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4, Unit>> GetChildren(Either<T0, T1, T2, T3, T4, Unit> node)
        {
            return node.Apply(nodeInner => tree0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, Unit>.From0), nodeInner => tree1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, Unit>.From1), nodeInner => tree2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, Unit>.From2), nodeInner => tree3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, Unit>.From3), nodeInner => tree4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, Unit>.From4),
                        _ => treeRoots);
        }
    }
    public sealed class DisjointUnionForest<T0, T1, T2, T3, T4, T5> : IForest<Either<T0, T1, T2, T3, T4, T5>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;
        readonly IForest<T2> forest2;
        readonly IForest<T3> forest3;
        readonly IForest<T4> forest4;
        readonly IForest<T5> forest5;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1, IForest<T2> forest2, IForest<T3> forest3, IForest<T4> forest4, IForest<T5> forest5)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
            this.forest3 = forest3 ?? throw new ArgumentNullException(nameof(forest3));
            this.forest4 = forest4 ?? throw new ArgumentNullException(nameof(forest4));
            this.forest5 = forest5 ?? throw new ArgumentNullException(nameof(forest5));
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4, T5>> GetChildren(Either<T0, T1, T2, T3, T4, T5> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5>.From1), nodeInner => forest2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5>.From2), nodeInner => forest3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5>.From3), nodeInner => forest4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5>.From4), nodeInner => forest5.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5>.From5));
        }
    }

    public sealed class DisjoinUnionTree<T0, T1, T2, T3, T4, T5> : ITree<Either<T0, T1, T2, T3, T4, T5, Unit>>
    {
        readonly ITree<T0> tree0;
        readonly ITree<T1> tree1;
        readonly ITree<T2> tree2;
        readonly ITree<T3> tree3;
        readonly ITree<T4> tree4;
        readonly ITree<T5> tree5;
        readonly Either<T0, T1, T2, T3, T4, T5, Unit>[] treeRoots;

        public Either<T0, T1, T2, T3, T4, T5, Unit> Root => Unit.Default;

        public DisjoinUnionTree(ITree<T0> tree0, ITree<T1> tree1, ITree<T2> tree2, ITree<T3> tree3, ITree<T4> tree4, ITree<T5> tree5)
        {
            this.tree0 = tree0 ?? throw new ArgumentNullException(nameof(tree0));
            this.tree1 = tree1 ?? throw new ArgumentNullException(nameof(tree1));
            this.tree2 = tree2 ?? throw new ArgumentNullException(nameof(tree2));
            this.tree3 = tree3 ?? throw new ArgumentNullException(nameof(tree3));
            this.tree4 = tree4 ?? throw new ArgumentNullException(nameof(tree4));
            this.tree5 = tree5 ?? throw new ArgumentNullException(nameof(tree5));
            treeRoots = new Either<T0, T1, T2, T3, T4, T5, Unit>[] { tree0.Root, tree1.Root, tree2.Root, tree3.Root, tree4.Root, tree5.Root };
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4, T5, Unit>> GetChildren(Either<T0, T1, T2, T3, T4, T5, Unit> node)
        {
            return node.Apply(nodeInner => tree0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, Unit>.From0), nodeInner => tree1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, Unit>.From1), nodeInner => tree2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, Unit>.From2), nodeInner => tree3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, Unit>.From3), nodeInner => tree4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, Unit>.From4), nodeInner => tree5.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, Unit>.From5),
                        _ => treeRoots);
        }
    }
    public sealed class DisjointUnionForest<T0, T1, T2, T3, T4, T5, T6> : IForest<Either<T0, T1, T2, T3, T4, T5, T6>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;
        readonly IForest<T2> forest2;
        readonly IForest<T3> forest3;
        readonly IForest<T4> forest4;
        readonly IForest<T5> forest5;
        readonly IForest<T6> forest6;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1, IForest<T2> forest2, IForest<T3> forest3, IForest<T4> forest4, IForest<T5> forest5, IForest<T6> forest6)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
            this.forest3 = forest3 ?? throw new ArgumentNullException(nameof(forest3));
            this.forest4 = forest4 ?? throw new ArgumentNullException(nameof(forest4));
            this.forest5 = forest5 ?? throw new ArgumentNullException(nameof(forest5));
            this.forest6 = forest6 ?? throw new ArgumentNullException(nameof(forest6));
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4, T5, T6>> GetChildren(Either<T0, T1, T2, T3, T4, T5, T6> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From1), nodeInner => forest2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From2), nodeInner => forest3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From3), nodeInner => forest4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From4), nodeInner => forest5.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From5), nodeInner => forest6.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6>.From6));
        }
    }

    public sealed class DisjoinUnionTree<T0, T1, T2, T3, T4, T5, T6> : ITree<Either<T0, T1, T2, T3, T4, T5, T6, Unit>>
    {
        readonly ITree<T0> tree0;
        readonly ITree<T1> tree1;
        readonly ITree<T2> tree2;
        readonly ITree<T3> tree3;
        readonly ITree<T4> tree4;
        readonly ITree<T5> tree5;
        readonly ITree<T6> tree6;
        readonly Either<T0, T1, T2, T3, T4, T5, T6, Unit>[] treeRoots;

        public Either<T0, T1, T2, T3, T4, T5, T6, Unit> Root => Unit.Default;

        public DisjoinUnionTree(ITree<T0> tree0, ITree<T1> tree1, ITree<T2> tree2, ITree<T3> tree3, ITree<T4> tree4, ITree<T5> tree5, ITree<T6> tree6)
        {
            this.tree0 = tree0 ?? throw new ArgumentNullException(nameof(tree0));
            this.tree1 = tree1 ?? throw new ArgumentNullException(nameof(tree1));
            this.tree2 = tree2 ?? throw new ArgumentNullException(nameof(tree2));
            this.tree3 = tree3 ?? throw new ArgumentNullException(nameof(tree3));
            this.tree4 = tree4 ?? throw new ArgumentNullException(nameof(tree4));
            this.tree5 = tree5 ?? throw new ArgumentNullException(nameof(tree5));
            this.tree6 = tree6 ?? throw new ArgumentNullException(nameof(tree6));
            treeRoots = new Either<T0, T1, T2, T3, T4, T5, T6, Unit>[] { tree0.Root, tree1.Root, tree2.Root, tree3.Root, tree4.Root, tree5.Root, tree6.Root };
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4, T5, T6, Unit>> GetChildren(Either<T0, T1, T2, T3, T4, T5, T6, Unit> node)
        {
            return node.Apply(nodeInner => tree0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From0), nodeInner => tree1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From1), nodeInner => tree2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From2), nodeInner => tree3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From3), nodeInner => tree4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From4), nodeInner => tree5.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From5), nodeInner => tree6.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, Unit>.From6),
                        _ => treeRoots);
        }
    }
    public sealed class DisjointUnionForest<T0, T1, T2, T3, T4, T5, T6, T7> : IForest<Either<T0, T1, T2, T3, T4, T5, T6, T7>>
    {
        readonly IForest<T0> forest0;
        readonly IForest<T1> forest1;
        readonly IForest<T2> forest2;
        readonly IForest<T3> forest3;
        readonly IForest<T4> forest4;
        readonly IForest<T5> forest5;
        readonly IForest<T6> forest6;
        readonly IForest<T7> forest7;

        public DisjointUnionForest(IForest<T0> forest0, IForest<T1> forest1, IForest<T2> forest2, IForest<T3> forest3, IForest<T4> forest4, IForest<T5> forest5, IForest<T6> forest6, IForest<T7> forest7)
        {
            this.forest0 = forest0 ?? throw new ArgumentNullException(nameof(forest0));
            this.forest1 = forest1 ?? throw new ArgumentNullException(nameof(forest1));
            this.forest2 = forest2 ?? throw new ArgumentNullException(nameof(forest2));
            this.forest3 = forest3 ?? throw new ArgumentNullException(nameof(forest3));
            this.forest4 = forest4 ?? throw new ArgumentNullException(nameof(forest4));
            this.forest5 = forest5 ?? throw new ArgumentNullException(nameof(forest5));
            this.forest6 = forest6 ?? throw new ArgumentNullException(nameof(forest6));
            this.forest7 = forest7 ?? throw new ArgumentNullException(nameof(forest7));
        }

        public IEnumerable<Either<T0, T1, T2, T3, T4, T5, T6, T7>> GetChildren(Either<T0, T1, T2, T3, T4, T5, T6, T7> node)
        {
            return node.Apply(nodeInner => forest0.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0), nodeInner => forest1.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1), nodeInner => forest2.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2), nodeInner => forest3.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3), nodeInner => forest4.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4), nodeInner => forest5.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5), nodeInner => forest6.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6), nodeInner => forest7.GetChildren(nodeInner).Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7));
        }
    }

}