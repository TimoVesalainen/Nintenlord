using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Mostly versions of forest utilities that set the root as node parameter
    /// </summary>
    public static class TreeHelpers
    {
        public static ITree<TNode> SetRoot<TNode>(this IForest<TNode> forest, TNode newRoot)
        {
            return new LambdaTree<TNode>(newRoot, forest.GetChildren);
        }

        public static ITree<Either<TNode, TRoot>> PrependRoot<TNode, TRoot>(this ITree<TNode> tree, TRoot newRoot)
        {
            IEnumerable<Either<TNode, TRoot>> GetChildren(Either<TNode, TRoot> node)
            {
                return node.Apply(
                    originalNode => tree.GetChildren(originalNode).Select(Either<TNode, TRoot>.From0),
                    root => Enumerable.Repeat((Either<TNode, TRoot>)tree.Root, 1));
            }

            return new LambdaTree<Either<TNode, TRoot>>(newRoot, GetChildren);
        }

        public static IEnumerable<TNode> BreadthFirstTraversal<TNode>(this ITree<TNode> tree)
        {
            return tree.BreadthFirstTraversal(tree.Root);
        }

        public static IEnumerable<TNode[]> GetGenerations<TNode>(this ITree<TNode> tree)
        {
            return tree.GetGenerations(tree.Root);
        }

        public static TAggregate Aggregate<TAggregate, TNode>(this ITree<TNode> tree, Func<TNode, IEnumerable<TAggregate>, TAggregate> combine)
        {
            return tree.Aggregate(combine, tree.Root);
        }

        public static IEnumerable<TNode> DepthFirstTraversal<TNode>(this ITree<TNode> tree)
        {
            return tree.DepthFirstTraversal(tree.Root);
        }

        public static RoseTree<TNode> GetRoseTree<TNode>(this ITree<TNode> tree, IEqualityComparer<TNode> nodeComparer = null)
        {
            return tree.GetRoseTree(tree.Root, nodeComparer);
        }

        public static int LongestPath<TNode>(this ITree<TNode> tree)
        {
            return tree.LongestPath(tree.Root);
        }

        public static IEnumerable<TNode> GetLeaves<TNode>(this ITree<TNode> tree)
        {
            return tree.GetLeaves(tree.Root);
        }

        public static string PrettyPrint<TNode>(this ITree<TNode> tree, Func<TNode, string> toString = null)
        {
            return tree.PrettyPrint(tree.Root, toString);
        }

        public static IEnumerable<string> PrettyPrintLines<TNode>(this ITree<TNode> tree, Func<TNode, string> toString = null)
        {
            return tree.PrettyPrintLines(tree.Root, toString);
        }

        public static ITree<(TNode, TAggregate)> AggregateTree<TAggregate, TNode>(this ITree<TNode> tree, Func<TNode, TAggregate, TAggregate> combine, TAggregate startValue)
        {
            return tree.AggregateTree(combine, startValue, tree.Root);
        }

        public static IEnumerable<ImmutableList<TNode>> GetPaths<TNode>(this ITree<TNode> tree)
        {
            return tree.GetPaths(tree.Root);
        }

        public static ITree<(TNode1, TNode2)> ZipTree<TNode1, TNode2>(this ITree<TNode1> tree1, ITree<TNode2> tree2)
        {
            if (tree1 is null)
            {
                throw new ArgumentNullException(nameof(tree1));
            }

            if (tree2 is null)
            {
                throw new ArgumentNullException(nameof(tree2));
            }

            return new LambdaTree<(TNode1, TNode2)>((tree1.Root, tree2.Root), pair => tree1.GetChildren(pair.Item1).Zip(tree2.GetChildren(pair.Item2), (x, y) => (x, y)));
        }

        public static ITree<(TNode, Maybe<TChild>)> ZipChildren<TChild, TNode>(this ITree<TNode> tree, IEnumerable<TChild> toCombine)
        {
            return tree.ZipChildren(toCombine, tree.Root);
        }

        public static ITree<(TNode node, ImmutableList<TBranch> path)> GetBranchesTo<TNode, TBranch>(this ITree<TNode> tree, IEnumerable<TBranch> branches)
        {
            return tree.GetBranchesTo(branches, tree.Root);
        }

        public static ITree<(TNode, int depth)> GetDepth<TNode>(this ITree<TNode> tree)
        {
            return tree.GetDepth(tree.Root);
        }

        public static ITree<TNode> EditChildrenTree<TNode>(this ITree<TNode> tree, Func<IEnumerable<TNode>, IEnumerable<TNode>> editChildren)
        {
            return new LambdaTree<TNode>(tree.Root, node => editChildren(tree.GetChildren(node)));
        }

        public static ITree<TNode> PruneTree<TNode>(this ITree<TNode> forest, Predicate<TNode> nodeFilter)
        {
            IEnumerable<TNode> GetChildren(IEnumerable<TNode> children)
            {
                return children.Where(node => nodeFilter(node));
            }

            return forest.EditChildrenTree(GetChildren);
        }

        public static ITree<TNode2> SelectTree<TNode, TNode2>(this ITree<TNode> tree, Func<TNode, TNode2> select1, Func<TNode2, TNode> select2)
        {
            return new SelectTree<TNode2, TNode>(select2, select1, tree);
        }

        public static bool StructuralEquality<TNode1, TNode2>(this ITree<TNode1> tree1, ITree<TNode2> tree2)
        {
            return tree1.StructuralEquality(tree1.Root, tree2, tree2.Root);
        }

        public static bool TreeEquality<TNode>(this ITree<TNode> tree1, ITree<TNode> tree2, IEqualityComparer<TNode> comparer = null)
        {
            return tree1.ForestEquality(tree1.Root, tree2, tree2.Root, comparer);
        }
    }
}
