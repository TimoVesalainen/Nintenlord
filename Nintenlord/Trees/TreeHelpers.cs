using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Mostly versions of forest utilities that set the root as node parameter
    /// </summary>
    public static class TreeHelpers
    {
        public static IEnumerable<TNode> BreadthFirstTraversal<TNode>(this ITree<TNode> tree)
        {
            return tree.BreadthFirstTraversal(tree.Root);
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

        public static IEnumerable<TAggregate> AggregateAscend<TAggregate, TNode>(this ITree<TNode> tree, Func<TNode, TAggregate, TAggregate> combine, TAggregate startValue)
        {
            return tree.AggregateAscend(combine, startValue, tree.Root);
        }

        public static IEnumerable<TNode> GetLeaves2<TNode>(this ITree<TNode> tree)
        {
            return tree.GetLeaves2(tree.Root);
        }

        public static IEnumerable<ImmutableList<TNode>> GetPaths<TNode>(this ITree<TNode> tree)
        {
            return tree.GetPaths(tree.Root);
        }

        public static bool StructuralEquality<TNode1, TNode2>(this ITree<TNode1> tree1, ITree<TNode2> tree2)
        {
            return tree1.StructuralEquality(tree1.Root, tree2, tree2.Root);
        }

        public static bool TreeEquality<TNode>(this ITree<TNode> tree1, ITree<TNode> tree2, IEqualityComparer<TNode> comparer = null)
        {
            return tree1.ForestEquality(tree1.Root, tree2, tree2.Root, comparer);
        }

        public static IEnumerable<TNode[]> GetGenerations<TNode>(this ITree<TNode> tree)
        {
            return tree.GetGenerations(tree.Root);
        }
    }
}
