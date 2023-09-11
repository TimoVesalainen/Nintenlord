using NUnit.Framework;
using System.Linq;
using System.Text;
using Nintenlord.Matricis;
using Nintenlord.Collections.Lists.Change;

namespace Nintenlord.Tests.Collections.Lists.Change
{
    public class ListDiffTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void FindLargestDiagonalsTest(int n)
        {
            var diagonal = new DiagonalMatrix<bool>(Enumerable.Repeat(true, n), false).ToArray();

            Assert.AreEqual(new []{ (0,0,n) }, ListDiff.FindLargestDiagonals(diagonal).ToArray());
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void FindLargestFromAllTrueTest(int n)
        {
            var diagonal = new ConstMatrix<bool>(true, n ,n).ToArray();

            Assert.AreEqual(new[] { (0, 0, n) }, ListDiff.FindLargestDiagonals(diagonal).ToArray());
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void FindLargestFromAllFalseTest(int n)
        {
            var diagonal = new ConstMatrix<bool>(false, n, n).ToArray();

            Assert.AreEqual(new(int, int, int)[0], ListDiff.FindLargestDiagonals(diagonal).ToArray());
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void FindLargestDiagonalsDiagonalFalseTest(int n)
        {
            var diagonal = new DiagonalMatrix<bool>(Enumerable.Repeat(false, n), true).ToArray();

            Assert.AreEqual(new[] { (0, 1, n - 1) }, ListDiff.FindLargestDiagonals(diagonal).ToArray());
        }

        [Test]
        public void TestNoChanges()
        {
            var array = Enumerable.Range(0, 10).ToArray();

            Assert.AreEqual(Enumerable.Empty<IListChange<int>>(), array.GetListDiff(array));
        }

        [Test]
        public void TestIdenticalItems()
        {
            var array = Enumerable.Repeat(0, 10).ToArray();

            Assert.AreEqual(Enumerable.Empty<IListChange<int>>(), array.GetListDiff(array));
        }

        [Test]
        public void TestAddedItemStart()
        {
            var array = Enumerable.Range(0, 10).ToArray();
            var array2 = array.Prepend(-1).ToArray();

            Assert.AreEqual(new[] { IListChange<int>.Added(0, 1, array, array2)}, array.GetListDiff(array2).ToArray());
        }

        [Test]
        public void TestAddedItemEnd()
        {
            var array = Enumerable.Range(0, 10).ToArray();
            var array2 = array.Append(-1).ToArray();

            Assert.AreEqual(new[] { IListChange<int>.Added(array.Length, 1, array, array2) }, array.GetListDiff(array2));
        }

        [Test]
        public void TestAddedItemMiddle()
        {
            var array = Enumerable.Range(0, 10).ToArray();
            var array2 = Enumerable.Range(0, 5).Append(-1).Concat(Enumerable.Range(5, 5)).ToArray();

            Assert.AreEqual(new[] { IListChange<int>.Added(5, 1, array, array2) }, array.GetListDiff(array2));
        }

        [Test]
        public void TestRemovedItemStart()
        {
            var array = Enumerable.Range(0, 10).ToArray();
            var array2 = Enumerable.Range(1, 9).ToArray();

            Assert.AreEqual(new[] { IListChange<int>.Removed(0, 1, array, array2) }, array.GetListDiff(array2).ToArray());
        }

        [Test]
        public void TestRemovedItemEnd()
        {
            var array = Enumerable.Range(0, 10).ToArray();
            var array2 = Enumerable.Range(0, 9).ToArray();

            Assert.AreEqual(new[] { IListChange<int>.Removed(array2.Length, 1, array, array2) }, array.GetListDiff(array2));
        }

        [Test]
        public void TestRemovedItemMiddle()
        {
            var array = Enumerable.Range(0, 10).ToArray();
            var array2 = Enumerable.Range(0, 4).Concat(Enumerable.Range(5, 5)).ToArray();

            var diff = array.GetListDiff(array2).ToArray();
            Assert.AreEqual(new[] { IListChange<int>.Removed(4, 1, array, array2) }, diff);
        }
    }
}
