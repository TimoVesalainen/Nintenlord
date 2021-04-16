using Nintenlord.Collections;
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

        public static IEnumerable<TNode[]> GetGenerations<TNode>(this IForest<TNode> forest, TNode start)
        {
            bool isFirst = true;
            List<TNode> first = new List<TNode>();
            List<TNode> second = new List<TNode>();

            first.Add(start);

            List<TNode> Current() => isFirst ? first : second;
            List<TNode> Next() => isFirst ? second : first;

            while (Current().Count > 0)
            {
                yield return Current().ToArray();
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
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            nodeComparer = nodeComparer ?? EqualityComparer<TNode>.Default;

            var nodeCache = new Dictionary<TNode, RoseTreeNode<TNode>>(nodeComparer);

            RoseTreeNode<TNode> GetRoseTreeNode(TNode node, IEnumerable<RoseTreeNode<TNode>> children)
            {
                if (nodeCache.TryGetValue(node, out var cachedNode))
                {
                    return cachedNode;
                }
                else
                {
                    var newNode = new RoseTreeNode<TNode>(children, node);
                    nodeCache.Add(node, newNode);
                    return newNode;
                }
            }

            var rootNode = forest.Aggregate<RoseTreeNode<TNode>, TNode>(GetRoseTreeNode, root);

            return new RoseTree<TNode>(rootNode);
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

            IEnumerable<(TNode, TAggregate)> GetChildren((TNode, TAggregate) pair)
            {
                var (node, parentAggregate) = pair;
                var children = forest.GetChildren(node);
                return children.Select(child => (child, combine(child, parentAggregate)));
            }

            return new LambdaTree<(TNode, TAggregate)>((start, startValue), GetChildren);
        }

        public static IEnumerable<ImmutableList<TNode>> GetPaths<TNode>(this IForest<TNode> forest, TNode root)
        {
            if (forest is null)
            {
                throw new ArgumentNullException(nameof(forest));
            }

            ImmutableList<TNode> GetPaths(TNode child, ImmutableList<TNode> path)
            {
                return path.Add(child);
            }

            return forest.AggregateTree(GetPaths, ImmutableList<TNode>.Empty, root)
                         .GetLeaves()
                         .Select(pair => pair.Item2.Add(pair.Item1));
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
    }
}
