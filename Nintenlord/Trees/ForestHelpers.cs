using Nintenlord.Collections;
using Nintenlord.Trees.Nodes;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Nintenlord.Trees
{
    public static class ForestHelpers
    {
        public static IEnumerable<TNode> BreadthFirstTraversal<TNode>(this IForest<TNode> forest, TNode start)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            Queue<TNode> toTraverse = new Queue<TNode>();
            toTraverse.Enqueue(start);

            yield return start;

            while (toTraverse.Count > 0)
            {
                var current = toTraverse.Dequeue();

                foreach (var child in forest.GetChildren(current))
                {
                    yield return child;
                    toTraverse.Enqueue(child);
                }
            }
        }
        public static IEnumerable<IEnumerable<TNode>> GetGenerations<TNode>(this IForest<TNode> forest, TNode start)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            bool isFirst = true;
            List<TNode> first = new List<TNode>();
            List<TNode> second = new List<TNode>();

            first.Add(start);

            List<TNode> Current() => isFirst ? first : second;
            List<TNode> Next() => isFirst ? second : first;

            while (Current().Count > 0)
            {
                yield return Current();
                Next().Clear();

                foreach (var item in Current())
                {
                    Next().AddRange(forest.GetChildren(item));
                }

                isFirst = !isFirst;
            }
        }


        public static TAggregate Aggregate<TAggregate, TNode>(this IForest<TNode> forest, Func<TNode, IEnumerable<TAggregate>, TAggregate> combine, TNode start)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (combine is null)
            {
                throw new ArgumentNullException(nameof(combine));
            }

            TAggregate AggregateNode(TNode node)
            {
                return combine(node, forest.GetChildren(node).Select(AggregateNode));
            }

            return AggregateNode(start);
        }

        public static IEnumerable<TNode> DepthFirstTraversal<TNode>(this IForest<TNode> forest, TNode start)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            IEnumerable<TNode> Combine(TNode node, IEnumerable<IEnumerable<TNode>> childTraversals)
            {
                yield return node;
                foreach (var child in childTraversals)
                {
                    foreach (var item in child)
                    {
                        yield return item;
                    }
                }
            }

            return forest.Aggregate<IEnumerable<TNode>, TNode>(Combine, start);
        }
        public static RoseTree<TNode> GetRoseTree<TNode>(this IForest<TNode> forest, TNode root, IEqualityComparer<TNode> nodeComparer = null)
        {
            return forest.GetRoseTree(root, x => x, nodeComparer);
        }

        public static RoseTree<TResult> GetRoseTree<TNode, TResult>(this IForest<TNode> forest, TNode root, Func<TNode, TResult> selector, IEqualityComparer<TNode> nodeComparer = null)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var rootNode = forest.GetConcreteTree<TNode, RoseTreeNode<TResult>>(root,
                (node, children)  => new RoseTreeNode<TResult>(children, selector(node)), nodeComparer);

            return new RoseTree<TResult>(rootNode);
        }

        public static Maybe<BinaryTree<TNode>> TryGetBinaryTree<TNode>(this IForest<TNode> forest, TNode root, IEqualityComparer<TNode> nodeComparer = null)
        {
            return forest.TryGetBinaryTree(root, x => x, nodeComparer);
        }

        public static Maybe<BinaryTree<TResult>> TryGetBinaryTree<TNode, TResult>(this IForest<TNode> forest, TNode root, Func<TNode, TResult> selector, IEqualityComparer<TNode> nodeComparer = null)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<BinaryTreeNode<TResult>> GetNode(TNode node, IEnumerable<Maybe<BinaryTreeNode<TResult>>> children)
            {
                var childrenArray = children.ToList();

                if (childrenArray.Count == 0)
                {
                    return new BinaryTreeNode<TResult>(selector(node));
                }
                else if (childrenArray.Count == 2)
                {
                    return childrenArray[0].Zip(childrenArray[1], (left, right) => new BinaryTreeNode<TResult>(left, right, selector(node)));
                }
                else
                {
                    return Maybe<BinaryTreeNode<TResult>>.Nothing;
                }
            }

            var rootNode = forest.GetConcreteTree<TNode, Maybe<BinaryTreeNode<TResult>>>(root, GetNode, nodeComparer);

            return rootNode.Select(binaryRoot => new BinaryTree<TResult>(binaryRoot));
        }

        public static TOut GetConcreteTree<TNode, TOut>(this IForest<TNode> forest, TNode root,
            Func<TNode, IEnumerable<TOut>, TOut> createNode,
            IEqualityComparer<TNode> nodeComparer = null)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            nodeComparer = nodeComparer ?? EqualityComparer<TNode>.Default;

            var nodeCache = new Dictionary<TNode, TOut>(nodeComparer);

            TOut GetNode(TNode node, IEnumerable<TOut> children)
            {
                if (nodeCache.TryGetValue(node, out var cachedNode))
                {
                    return cachedNode;
                }
                else
                {
                    var newNode = createNode(node, children);
                    nodeCache.Add(node, newNode);
                    return newNode;
                }
            }

            return forest.Aggregate<TOut, TNode>(GetNode, root);
        }

        public static int LongestPath<TNode>(this IForest<TNode> forest, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            int SumLength(TNode _, IEnumerable<int> childLengths)
            {
                return childLengths.MaxSafe().GetValueOrDefault(0) + 1;
            }

            return forest.Aggregate<int, TNode>(SumLength, root);
        }

        public static IEnumerable<TNode> GetLeaves<TNode>(this IForest<TNode> forest, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            IEnumerable<TNode> ConcatLeaves(TNode node, IEnumerable<IEnumerable<TNode>> childLeaves)
            {
                if (childLeaves.Any())
                {
                    foreach (var leaves in childLeaves)
                    {
                        foreach (var leaf in leaves)
                        {
                            yield return leaf;
                        }
                    }
                }
                else
                {
                    yield return node;
                }
            }

            return forest.Aggregate<IEnumerable<TNode>, TNode>(ConcatLeaves, root);
        }

        public static string PrettyPrint<TNode>(this IForest<TNode> forest, TNode root, Func<TNode, string> toString = null)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            return string.Join(Environment.NewLine, forest.PrettyPrintLines(root, toString));
        }

        public static IEnumerable<string> PrettyPrintLines<TNode>(this IForest<TNode> forest, TNode root, Func<TNode, string> toString = null)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            toString = toString ?? (node => node.ToString());

            IEnumerable<string> PrependLines(IEnumerable<string> lines, bool isLast)
            {
                IEnumerable<string> Prefixes()
                {
                    yield return "+-";

                    while (true)
                    {
                        yield return "| ";
                    }
                }

                IEnumerable<string> LastPrefixes()
                {
                    yield return "`-";

                    while (true)
                    {
                        yield return "  ";
                    }
                }

                return lines.Zip(isLast ? LastPrefixes() : Prefixes(), (line, prefix) => prefix + line);
            }

            IEnumerable<string> GetNodeLines(TNode node, IEnumerable<IEnumerable<string>> childLines)
            {
                yield return toString(node);
                if (childLines.Any())
                {
                    yield return "| ";
                    foreach (var line in childLines.GetIsLast()
                                                   .SelectMany(tuple => PrependLines(tuple.Item1, tuple.isLast)))
                    {
                        yield return line;
                    }
                }
            }

            return forest.Aggregate<IEnumerable<string>, TNode>(GetNodeLines, root);
        }

        public static ITree<(TNode, TAggregate)> AggregateTree<TAggregate, TNode>(this IForest<TNode> forest, Func<TNode, TAggregate, TAggregate> combine, TAggregate startValue, TNode start)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (combine is null)
            {
                throw new ArgumentNullException(nameof(combine));
            }

            IEnumerable<(TNode, TAggregate)> GetChildren((TNode, TAggregate) pair)
            {
                var (node, parentAggregate) = pair;
                var children = forest.GetChildren(node);
                return children.Select(child => (child, combine(child, parentAggregate)));
            }

            return new LambdaTree<(TNode, TAggregate)>((start, combine(start, startValue)), GetChildren);
        }

        public static IEnumerable<ImmutableList<TNode>> GetPaths<TNode>(this IForest<TNode> forest, TNode root)
        {
            return forest.GetPaths(root, x => x);
        }

        public static IEnumerable<ImmutableList<TResult>> GetPaths<TNode, TResult>(this IForest<TNode> forest, TNode root, Func<TNode, TResult> selector)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            ImmutableList<TResult> GetPaths(TNode child, ImmutableList<TResult> path)
            {
                return path.Add(selector(child));
            }

            return forest.AggregateTree(GetPaths, ImmutableList<TResult>.Empty, root)
                         .GetLeaves()
                         .Select(pair => pair.Item2);
        }

        public static IForest<(TNode1, TNode2)> ZipForest<TNode1, TNode2>(this IForest<TNode1> forest1, IForest<TNode2> forest2)
        {
            if (forest1 is null)
            {
                throw new ArgumentNullException(nameof(forest1));
            }

            if (forest2 is null)
            {
                throw new ArgumentNullException(nameof(forest2));
            }

            return new LambdaForest<(TNode1, TNode2)>(pair => forest1.GetChildren(pair.Item1).Zip(forest2.GetChildren(pair.Item2), (x,y) => (x, y)));
        }

        public static ITree<(TNode, Maybe<TChild>)> ZipChildren<TChild, TNode>(this IForest<TNode> forest, IEnumerable<TChild> toCombine, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (toCombine is null)
            {
                throw new ArgumentNullException(nameof(toCombine));
            }

            return forest.ZipChildren(_ => toCombine, root);
        }

        public static ITree<(TNode, Maybe<TChild>)> ZipChildren<TChild, TNode>(this IForest<TNode> forest, Func<TNode, IEnumerable<TChild>> toCombine, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (toCombine is null)
            {
                throw new ArgumentNullException(nameof(toCombine));
            }

            return forest.ZipAggregateChildren((node, _) => toCombine(node).Select(Maybe<TChild>.Just), Maybe<TChild>.Nothing, root);
        }

        public static ITree<(TNode, TAggregate)> ZipAggregateChildren<TAggregate, TNode>(this IForest<TNode> forest,
            Func<TNode, TAggregate, IEnumerable<TAggregate>> toCombine,
            TAggregate start,
            TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (toCombine is null)
            {
                throw new ArgumentNullException(nameof(toCombine));
            }

            IEnumerable<(TNode, TAggregate)> GetChildren((TNode, TAggregate) pair)
            {
                var (node, child) = pair;
                return forest.GetChildren(node).Zip(toCombine(node, child), (x, y) => (x, y));
            }

            return new LambdaTree<(TNode, TAggregate)>((root, start), GetChildren);
        }

        public static ITree<(TNode node, ImmutableList<TBranch> path)> GetBranchesTo<TNode, TBranch>(this IForest<TNode> forest, IEnumerable<TBranch> branches, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (branches is null)
            {
                throw new ArgumentNullException(nameof(branches));
            }

            ImmutableList<TBranch> Aggregate(
                (TNode, Maybe<TBranch>) nodeAndBranch,
                ImmutableList<TBranch> aggregatePath)
            {
                return nodeAndBranch.Item2.Select(newEnd => aggregatePath.Add(newEnd))
                                          .GetValueOrDefault(ImmutableList<TBranch>.Empty);
            }

            return forest.ZipChildren(branches, root)
                         .AggregateTree(Aggregate, ImmutableList<TBranch>.Empty, (root, Maybe<TBranch>.Nothing))
                         .SelectTree(pair => (pair.Item1.Item1, pair.Item2),
                                     pair => ((pair.Item1, pair.Item2.LastSafe()), pair.Item2));
        }

        public static ITree<(TNode, int depth)> GetDepth<TNode>(this IForest<TNode> forest, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            int GetPaths(TNode child, int depth)
            {
                return depth + 1;
            }

            return forest.AggregateTree(GetPaths, -1, root);
        }

        public static IForest<TNode> EditChildrenForest<TNode>(this IForest<TNode> forest, Func<IEnumerable<TNode>, IEnumerable<TNode>> editChildren)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (editChildren is null)
            {
                throw new ArgumentNullException(nameof(editChildren));
            }

            return new LambdaForest<TNode>(node => editChildren(forest.GetChildren(node)));
        }

        public static IForest<TNode> PruneForest<TNode>(this IForest<TNode> forest, Predicate<TNode> nodeFilter)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (nodeFilter is null)
            {
                throw new ArgumentNullException(nameof(nodeFilter));
            }

            IEnumerable<TNode> GetChildren(IEnumerable<TNode> children)
            {
                return children.Where(node => nodeFilter(node));
            }

            return forest.EditChildrenForest(GetChildren);
        }

        public static IForest<TNode2> SelectForest<TNode, TNode2>(this IForest<TNode> forest, Func<TNode, TNode2> select1, Func<TNode2, TNode> select2)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            if (select1 is null)
            {
                throw new ArgumentNullException(nameof(select1));
            }

            if (select2 is null)
            {
                throw new ArgumentNullException(nameof(select2));
            }

            return new SelectForest<TNode2, TNode>(select2, select1, forest);
        }

        public static ITree<(TNode, int depth)> GetToMaxDepth<TNode>(this IForest<TNode> forest, TNode root, int maxDepth)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            return forest.GetDepth(root)
                         .PruneTree(pair => pair.depth <= maxDepth);
            //Removing height with SelectTree doesn't work naively
        }

        public static bool StructuralEquality<TNode1, TNode2>(this
            IForest<TNode1> forest1, TNode1 root1,
            IForest<TNode2> forest2, TNode2 root2)
        {
            if (forest1 is null)
            {
                throw new ArgumentNullException(nameof(forest1));
            }

            if (forest2 is null)
            {
                throw new ArgumentNullException(nameof(forest2));
            }

            bool AreEqual(TNode1 node1, TNode2 node2)
            {
                bool AreEqualMaybe(Maybe<TNode1> maybeNode1, Maybe<TNode2> maybeNode2)
                {
                    return maybeNode1.Zip(maybeNode2, AreEqual).GetValueOrDefault(false);
                }

                return forest1.GetChildren(node1).ZipLong(forest2.GetChildren(node2), AreEqualMaybe).And();
            }

            return AreEqual(root1, root2);
        }

        public static bool ForestEquality<TNode>(this
            IForest<TNode> forest1, TNode root1,
            IForest<TNode> forest2, TNode root2,
            IEqualityComparer<TNode> comparer = null)
        {
            if (forest1 is null)
            {
                throw new ArgumentNullException(nameof(forest1));
            }

            if (forest2 is null)
            {
                throw new ArgumentNullException(nameof(forest2));
            }
            comparer = comparer ?? EqualityComparer<TNode>.Default;

            bool AreEqual(TNode node1, TNode node2)
            {
                bool AreEqualMaybe(Maybe<TNode> maybeNode1, Maybe<TNode> maybeNode2)
                {
                    return maybeNode1.Zip(maybeNode2, AreEqual).GetValueOrDefault(false);
                }

                return
                    comparer.Equals(node1, node2) &&
                    forest1.GetChildren(node1).ZipLong(forest2.GetChildren(node2), AreEqualMaybe).And();
            }

            return AreEqual(root1, root2);
        }

        public static IForest<TNode> Union<TNode>(
            this IForest<TNode> forest1, params IForest<TNode>[] forests)
        {
            return new UnionForest<TNode>(forests.Prepend(forest1));
        }

        public static IForest<TNode> Union<TNode>(
            this IEnumerable<IForest<TNode>> forests)
        {
            return new UnionForest<TNode>(forests);
        }

        public static IForest<Either<TNode1, TNode2>> DisjointUnion<TNode1, TNode2>(
            this IForest<TNode1> forest1, IForest<TNode2> forest2)
        {
            return new DisjointUnionForest<TNode1, TNode2>(forest1, forest2);
        }

        public static IForest<Either<TNode1, TNode2, TNode3>> DisjointUnion<TNode1, TNode2, TNode3>(
            this IForest<TNode1> forest1, IForest<TNode2> forest2, IForest<TNode3> forest3)
        {
            return new DisjointUnionForest<TNode1, TNode2, TNode3>(forest1, forest2, forest3);
        }

        public static ArrayTree<T> ConvertTo<T>(this IForest<T> tree, T root, int n)
        {
            return ConvertTo(tree, root, n, x => x);
        }

        public static ArrayTree<TOut> ConvertTo<T, TOut>(this IForest<T> tree, T root, int n, Func<T, TOut> selector)
        {
            var treeStructure = TreeHelpers.GetIndexTree(n);
            ArrayTree<TOut> result = new ArrayTree<TOut>(treeStructure);

            foreach (var (node, index) in tree.SetRoot(root).ZipTree(treeStructure).BreadthFirstTraversal())
            {
                result.SetItemToIndex(index, selector(node));
            }

            return result;
        }

        public static AddRootTree<T> AddRoot<T>(this IForest<T> tree, T newRoot, IEnumerable<T> rootChildren, IEqualityComparer<T> comparer = null)
        {
            return new AddRootTree<T>(tree, newRoot, rootChildren, comparer);
        }

        public static ITree<ITree<T>> ToTrees<T>(this IForest<T> forest, T root)
        {
            IEnumerable<ITree<T>> GetChildren(ITree<T> childTree)
            {
                foreach (var child in forest.GetChildren(childTree.Root))
                {
                    yield return forest.SetRoot(child);
                }
            }

            return new LambdaTree<ITree<T>>(forest.SetRoot(root), GetChildren);
        }
    }
}
