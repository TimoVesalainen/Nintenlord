﻿using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Mostly versions of forest utilities that set the root as node parameter
    /// </summary>
    public static class TreeHelpers
    {
        public static ITree<T> Create<T>(T root, Func<T, IEnumerable<T>> getChildren)
        {
            return new LambdaTree<T>(root, getChildren);
        }

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

        public static IEnumerable<IEnumerable<TNode>> GetGenerations<TNode>(this ITree<TNode> tree)
        {
            return tree.GetGenerations(tree.Root);
        }

        public static TAggregate Aggregate<TAggregate, TNode>(this ITree<TNode> tree, Func<TNode, IEnumerable<TAggregate>, TAggregate> combine)
        {
            return tree.Aggregate(combine, tree.Root);
        }

        public static Task<TAggregate> AggregateAsync<TAggregate, TNode>(this ITree<TNode> tree, Func<TNode, IEnumerable<TAggregate>, Task<TAggregate>> combine)
        {
            return tree.AggregateAsync(combine, tree.Root);
        }

        public static IEnumerable<TNode> DepthFirstTraversal<TNode>(this ITree<TNode> tree)
        {
            return tree.DepthFirstTraversal(tree.Root);
        }

        public static RoseTree<TNode> GetRoseTree<TNode>(this ITree<TNode> tree, IEqualityComparer<TNode> nodeComparer = null)
        {
            return tree.GetRoseTree(tree.Root, nodeComparer);
        }

        public static RoseTree<TResult> GetRoseTree<TNode, TResult>(this ITree<TNode> tree, Func<TNode, TResult> selector, IEqualityComparer<TNode> nodeComparer = null)
        {
            return tree.GetRoseTree(tree.Root, selector, nodeComparer);
        }

        public static Maybe<BinaryTree<TNode>> TryGetBinaryTree<TNode>(this ITree<TNode> tree, IEqualityComparer<TNode> nodeComparer = null)
        {
            return tree.TryGetBinaryTree(tree.Root, nodeComparer);
        }

        public static Maybe<BinaryTree<TResult>> TryGetBinaryTree<TNode, TResult>(this ITree<TNode> tree, Func<TNode, TResult> selector, IEqualityComparer<TNode> nodeComparer = null)
        {
            return tree.TryGetBinaryTree(tree.Root, selector, nodeComparer);
        }

        public static TOut GetConcreteTree<TNode, TOut>(this ITree<TNode> tree,
            Func<TNode, IEnumerable<TOut>, TOut> createNode,
            IEqualityComparer<TNode> nodeComparer = null)
        {
            return tree.GetConcreteTree(tree.Root, createNode, nodeComparer);
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

        public static ITree<(TNode node, Maybe<TNode> parent)> GetParents<TNode>(this ITree<TNode> tree)
        {
            return tree.GetParents(tree.Root);
        }

        public static bool IsIncreasing<TNode, TColor>(this ITree<TNode> tree, Func<TNode, TColor> colouring, IComparer<TColor> comparer = null)
        {
            return tree.IsIncreasing(colouring, tree.Root, comparer);
        }

        public static bool IsDecreasing<TNode, TColor>(this ITree<TNode> tree, Func<TNode, TColor> colouring, IComparer<TColor> comparer = null)
        {
            return tree.IsDecreasing(colouring, tree.Root, comparer);
        }

        public static IEnumerable<ImmutableList<TNode>> GetPaths<TNode>(this ITree<TNode> tree)
        {
            return tree.GetPaths(tree.Root);
        }

        public static IEnumerable<ImmutableList<TResult>> GetPaths<TNode, TResult>(this ITree<TNode> tree, Func<TNode, TResult> selector)
        {
            return tree.GetPaths(tree.Root, selector);
        }

        public static TOut ZipAggregate<TNode1, TNode2, TOut>(this
            ITree<TNode1> tree1, ITree<TNode2> tree2,
            Func<TNode1, TNode2, TOut> zipper,
            Func<TOut, IEnumerable<TOut>, TOut> foldChildren)
        {
            if (tree1 is null)
            {
                throw new ArgumentNullException(nameof(tree1));
            }

            if (tree2 is null)
            {
                throw new ArgumentNullException(nameof(tree2));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (foldChildren is null)
            {
                throw new ArgumentNullException(nameof(foldChildren));
            }

            return tree1.ZipAggregate(tree1.Root, tree2, tree2.Root, zipper, foldChildren);
        }

        public static TOut ZipAggregateLong<TNode1, TNode2, TOut>(this
            ITree<TNode1> tree1, ITree<TNode2> tree2,
            Func<TNode1, TNode2, TOut> zipper,
            Func<TNode1, TOut> zipperLeft, Func<TNode2, TOut> zipperRight,
            Func<TOut, IEnumerable<TOut>, TOut> foldChildren)
        {
            if (tree1 is null)
            {
                throw new ArgumentNullException(nameof(tree1));
            }

            if (tree2 is null)
            {
                throw new ArgumentNullException(nameof(tree2));
            }

            if (zipper is null)
            {
                throw new ArgumentNullException(nameof(zipper));
            }

            if (zipperLeft is null)
            {
                throw new ArgumentNullException(nameof(zipperLeft));
            }

            if (zipperRight is null)
            {
                throw new ArgumentNullException(nameof(zipperRight));
            }

            if (foldChildren is null)
            {
                throw new ArgumentNullException(nameof(foldChildren));
            }
            return tree1.ZipAggregateLong(tree1.Root, tree2, tree2.Root, zipper, zipperLeft, zipperRight, foldChildren);
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

            return tree1.ZipTree(tree1.Root, tree2, tree2.Root);
        }

        public static ITree<(TNode, Maybe<TChild>)> ZipChildren<TChild, TNode>(this ITree<TNode> tree, IEnumerable<TChild> toCombine)
        {
            return tree.ZipChildren(toCombine, tree.Root);
        }

        public static ITree<(TNode, Maybe<TChild>)> ZipChildren<TChild, TNode>(this ITree<TNode> tree, Func<TNode, IEnumerable<TChild>> toCombine)
        {
            return tree.ZipChildren(toCombine, tree.Root);
        }

        public static ITree<(TNode, TAggregate)> ZipAggregateChildren<TAggregate, TNode>(this ITree<TNode> tree, Func<TNode, TAggregate, IEnumerable<TAggregate>> toCombine, TAggregate start)
        {
            return tree.ZipAggregateChildren(toCombine, start, tree.Root);
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

        public static ITree<TNode> PruneTree<TNode>(this ITree<TNode> tree, Predicate<TNode> nodeFilter)
        {
            IEnumerable<TNode> GetChildren(IEnumerable<TNode> children)
            {
                return children.Where(node => nodeFilter(node));
            }

            return tree.EditChildrenTree(GetChildren);
        }

        public static ITree<TNode2> SelectTree<TNode, TNode2>(this ITree<TNode> tree, Func<TNode, TNode2> select1, Func<TNode2, TNode> select2)
        {
            return new SelectTree<TNode2, TNode>(select2, select1, tree);
        }

        public static ITree<(TNode, int depth)> GetToMaxDepth<TNode>(this ITree<TNode> tree, int maxDepth)
        {
            if (tree is null)
            {
                throw new ArgumentNullException(nameof(tree));
            }

            return tree.GetToMaxDepth(tree.Root, maxDepth);
        }

        public static bool StructuralEquality<TNode1, TNode2>(this ITree<TNode1> tree1, ITree<TNode2> tree2)
        {
            return tree1.StructuralEquality(tree1.Root, tree2, tree2.Root);
        }

        public static bool TreeEquality<TNode>(this ITree<TNode> tree1, ITree<TNode> tree2, IEqualityComparer<TNode> comparer = null)
        {
            return tree1.ForestEquality(tree1.Root, tree2, tree2.Root, comparer);
        }

        public static ArrayTree<T> ConvertTo<T>(BinaryTree<T> tree)
        {
            var treeStructure = IndexBinaryTree.Instance;
            ArrayTree<T> result = new(treeStructure);

            foreach (var (node, index) in tree.ZipTree(treeStructure).BreadthFirstTraversal())
            {
                result.SetItemToIndex(index, node.Value);
            }

            return result;
        }

        public static ITree<int> GetIndexTree(int n)
        {
            if (n == 2)
            {
                return IndexBinaryTree.Instance;
            }
            else if (n > 2)
            {
                return IndexNTree.GetTree(n);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Parameter needs to be atleast 2");
            }
        }

        public static ArrayTree<T> ConvertTo<T>(this ITree<T> tree, int n)
        {
            return tree.ConvertTo(tree.Root, n);
        }

        public static ArrayTree<TOut> ConvertTo<T, TOut>(this ITree<T> tree, int n, Func<T, TOut> selector)
        {
            return tree.ConvertTo(tree.Root, n, selector);
        }


        public static AddRootTree<T> AddRoot<T>(this ITree<T> tree, T newRoot, IEqualityComparer<T> comparer = null)
        {
            return new AddRootTree<T>(tree, newRoot, new[] { tree.Root }, comparer);
        }

        public static ITree<ITree<T>> ToTrees<T>(this ITree<T> tree)
        {
            return tree.ToTrees(tree.Root);
        }

        public static IEnumerable<T> RandomWalk<T>(this ITree<T> tree, Random random)
        {
            return tree.RandomWalk(tree.Root, random);
        }

        public static IEnumerable<T> Walk<T>(this ITree<T> tree, Func<T, IEnumerable<T>, T> chooseBranch)
        {
            return tree.Walk(tree.Root, chooseBranch);
        }

        public static IEnumerable<T> Walk<T>(this ITree<T> tree, TryGetDelegate<T, IEnumerable<T>, T> chooseBranch)
        {
            return tree.Walk(tree.Root, chooseBranch);
        }

        public static IEnumerable<T> OrderedEnumeration<T, TKey>(this ITree<T> tree, Func<T, TKey> getKey, IComparer<TKey> comparer = null)
        {
            return tree.OrderedEnumeration(tree.Root, getKey, comparer);
        }

        public static ITree<T> ToTree<T>(this IDictionary<T, T> parents, T root, IEqualityComparer<T> equality = null)
        {
            var children = parents.GroupBy(x => x.Value, equality)
                .ToImmutableDictionary(t => t.Key, t => t.Select(x => x.Value).ToImmutableList());

            return new LambdaTree<T>(root, parent => children[parent]);
        }

        public static ITree<(T1, T2)> ProductTree<T1, T2>(this ITree<T1> tree1, ITree<T2> tree2)
        {
            return tree1.ProductTree(tree1.Root, tree2, tree2.Root);
        }

        public static ITree<(int index, T item)> ToVerticalTree<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (!items.Any())
            {
                throw new ArgumentException("Empty collection", nameof(items));
            }

            var list = items.ToList();

            IEnumerable<(int index, T item)> GetChildren((int index, T item) parent)
            {
                var childIndex = parent.index - 1;
                if (childIndex >= 0 && childIndex < list.Count)
                {
                    yield return (childIndex, list[childIndex]);
                }
            }

            return new LambdaTree<(int index, T item)>((0, list[0]), GetChildren);
        }

        public static ITree<Maybe<T>> ToHorizontalTree<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var children = items.Select(Maybe<T>.Just);

            IEnumerable<Maybe<T>> GetChildren(Maybe<T> parent)
            {
                return parent.Select(_ => Enumerable.Empty<Maybe<T>>()).GetValueOrDefault(children);
            }

            return new LambdaTree<Maybe<T>>(Maybe<T>.Nothing, GetChildren);
        }

        public static ITree<T> CombineTrees<T>(this IEnumerable<ITree<T>> trees, T newParent, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;

            IEnumerable<T> GetChildren(T node)
            {
                if (comparer.Equals(node, newParent))
                {
                    return trees.Select(tree => tree.Root);
                }
                else
                {
                    return trees.SelectMany(tree => tree.GetChildren(node));
                }
            }

            return new LambdaTree<T>(newParent, GetChildren);
        }
    }
}
