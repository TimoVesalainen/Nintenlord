using Nintenlord.Collections;
using Nintenlord.Trees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Tests.Trees
{
    public class TreeTests
    {
        RoseTree<int> tree;
        string treePrinted;

        [SetUp]
        public void Setup()
        {
            int i = 0;
            RoseTreeNode<int> GetNewNode(params RoseTreeNode<int>[] children)
            {
                return new RoseTreeNode<int>(children, i++);
            }

            tree = new RoseTree<int>(
                GetNewNode(
                    GetNewNode(
                        GetNewNode(
                            GetNewNode(),
                            GetNewNode()),
                        GetNewNode()),
                    GetNewNode(
                        GetNewNode())));
            treePrinted = tree.PrettyPrint(tree.Root, node => node.Value.ToString());
        }

        [Test]
        public void TestTraversal()
        {
            Assert.AreEqual(new[] { 7, 4, 6, 2, 3, 5, 0, 1 },
                tree.BreadthFirstTraversal().Select(node => node.Value));
            Assert.AreEqual(new[] { 7, 4, 2, 0, 1, 3, 6, 5 },
                tree.DepthFirstTraversal().Select(node => node.Value));
        }

        [Test]
        public void TestEquality()
        {
            Assert.IsTrue(tree.StructuralEquality(tree.Root, tree, tree.Root));
            Assert.IsTrue(tree.ForestEquality(tree.Root, tree, tree.Root));
        }

        [Test]
        public void TestLeafs()
        {
            var leaves = new[] {0, 1, 3, 5 };
            Assert.AreEqual(leaves, tree.GetLeaves(tree.Root).Select(x => x.Value));
        }

        [Test]
        public void TestPaths()
        {
            Assert.AreEqual(new[] {
                new[] { 7, 4, 2, 0 },
                new[] { 7, 4, 2, 1 },
                new[] { 7, 4, 3 },
                new[] { 7, 6, 5 }
            },
                tree.GetPaths(tree.Root).Select(x => x.Select(node => node.Value)));
        }

        [Test]
        public void TestGenerations()
        {
            Assert.AreEqual(new[] { new[] { 7 }, new[] { 4,6 }, new[] { 2,3,5 }, new[] { 0,1 } },
                tree.GetGenerations().Select(x => x.Select(x => x.Value)));
        }

        [Test]
        public void TestDepthWithBreadthFirstTraversal()
        {
            Assert.IsTrue(
                tree.GetDepth()
                .BreadthFirstTraversal()
                .Select(node => node.depth)
                .GetSequentialPairs()
                .All(pair => pair.current == pair.next || pair.current + 1 == pair.next));
        }

        [Test]
        public void TestDepthWithPaths()
        {
            IEnumerable<int> NonNegativeInts()
            {
                int i = 0;
                while (true)
                {
                    yield return i;
                    i++;
                }
            }

            Assert.IsTrue(
                tree.GetDepth()
                .GetPaths()
                .Select(path => path.Select(node => node.depth).Zip(NonNegativeInts(), (x, y) => x == y).And()).And());
        }

        [Test]
        public void TestPruningWithPaths()
        {
            Assert.AreEqual(new[] {
                new[] { 7, 6, 5 }
            }, tree.PruneForest(x => x.Value != 4).GetPaths(tree.Root).Select(x => x.Select(node => node.Value))
                .Select(l => l.ToArray())
                .ToArray()
                );

            Assert.AreEqual(new[] {
                new[] { 7, 4, 3 },
                new[] { 7, 6, 5 }
            }, tree.PruneForest(x => x.Value != 2).GetPaths(tree.Root).Select(x => x.Select(node => node.Value))
                .Select(l => l.ToArray())
                .ToArray()
                );
        }

        [Test]
        public void TestToMaxDepthWithPaths()
        {
            var paths = tree.GetToMaxDepth(tree.Root, 2).GetPaths((tree.Root, 0)).Select(x => x.Select(node => node.Item1.Value))
                .Select(l => l.ToArray())
                .ToArray();

            Assert.AreEqual(new[] {
                new[] { 7, 4, 2 },
                new[] { 7, 4, 3 },
                new[] { 7, 6, 5 }
            }, paths
                );
        }
    }
}
