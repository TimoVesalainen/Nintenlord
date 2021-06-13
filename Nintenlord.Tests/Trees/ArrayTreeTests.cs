using Nintenlord.Trees;
using Nintenlord.Collections.Comparers;
using NUnit.Framework;
using System.Collections.Generic;
using Nintenlord.Collections.EqualityComparer;

namespace Nintenlord.Tests.Trees
{
    class ArrayTreeTests
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
        public void TestAddingNewRoot()
        {
            var arrayTree = tree.ConvertTo(2);
            var newNode = new RoseTreeNode<int>(1000);

            var newTreeWay1 = tree.AddRoot(newNode).GetRoseTree(x => x.Value);
            var newTreeWay2 = ArrayTree<RoseTreeNode<int>>.AddNewRoot(arrayTree, newNode).GetRoseTree(x => x.item.Value);

            Assert.IsTrue(TreeHelpers.TreeEquality(newTreeWay1, newTreeWay2,
                EqualityComparer<int>.Default.Select<int, RoseTreeNode<int>>(x => x.Value)));
        }
    }
}
