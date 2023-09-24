using System;
using System.Linq;
using Nintenlord.Collections;
using NUnit.Framework;

namespace Nintenlord.Tests.Collections
{
    public class EnumerableExtensionsTests
    {
        [Test]
        public void TestIsLast()
        {
            Assert.AreEqual(
                new (int, bool)[] { },
                Enumerable.Empty<int>().GetIsLast());

            Assert.AreEqual(
                new[] { (0, true) },
                Enumerable.Range(0, 1).GetIsLast());

            Assert.AreEqual(
                new[] { (0, false), (1, false), (2, true) },
                Enumerable.Range(0, 3).GetIsLast());
        }

        [TestCase(4, 6)]
        [TestCase(1, 10)]
        [TestCase(5, 5)]
        public void TestZipLong(int length1, int length2)
        {
            var list1 = Enumerable.Range(0, length1);
            var list2 = Enumerable.Range(0, length2);

            Assert.AreEqual(Math.Max(length1, length2), list1.ZipLong(list2, (x, y) => 0).Count());
        }

        [Test]
        public void TestIntersperse()
        {
            Assert.AreEqual(
                new int[] { },
                Enumerable.Empty<int>().Intersperse(1));

            Assert.AreEqual(
                new[] { 0, 1, 0, 1, 0 },
                Enumerable.Repeat(0, 3).Intersperse(1));

            Assert.AreEqual(
                new[] { 0, 1, 0 },
                Enumerable.Repeat(0, 2).Intersperse(1));

            Assert.AreEqual(
                new[] { 0 },
                Enumerable.Repeat(0, 1).Intersperse(1));
        }

        [Test]
        public void TestUntilNotDistinct()
        {
            Assert.AreEqual(
                new int[] { 0, 1, 1 }.UntilNotDistinct(),
                new int[] { 0, 1 }
                );
            Assert.AreEqual(
                new int[] { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, }.UntilNotDistinct(),
                new int[] { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, }
                );
        }

        [Test]
        public void TestIndexLists()
        {
            for (int i = 0; i < 5; i++)
            {
                var items = EnumerableExtensions.GetIndexLists(i).ToList();

                Assert.AreEqual(Math.Pow(i, i), items.Count);
                Assert.IsTrue(items.All(x => x.Count() == i));
            }
        }


        [Test]
        public void TestNTakingFirst()
        {
            var toTest = Enumerable.Range(0, 10);

            Assert.AreEqual(toTest.GetFirst2(), toTest.GetSequential2s().First());
            Assert.AreEqual(toTest.GetFirst3(), toTest.GetSequential3s().First());
            Assert.AreEqual(toTest.GetFirst4(), toTest.GetSequential4s().First());
            Assert.AreEqual(toTest.GetFirst5(), toTest.GetSequential5s().First());
            Assert.AreEqual(toTest.GetFirst6(), toTest.GetSequential6s().First());
        }

        [Test]
        public void TestTwoTaking()
        {
            var n = 10;
            var toTest = Enumerable.Range(0, n + 2);

            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(
                    toTest.GetSequential2s().GetNth(i + 1).Enumerate().GetNth(0),
                    toTest.GetSequential2s().GetNth(i).Enumerate().GetNth(1));
            }
        }


        [Test]
        public void TestThreeTaking()
        {
            var n = 10;
            var toTest = Enumerable.Range(0, n + 3);

            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(
                    toTest.GetSequential3s().GetNth(i + 1).Enumerate().GetNth(0),
                    toTest.GetSequential3s().GetNth(i).Enumerate().GetNth(1));

                Assert.AreEqual(
                    toTest.GetSequential3s().GetNth(i + 1).Enumerate().GetNth(1),
                    toTest.GetSequential3s().GetNth(i).Enumerate().GetNth(2));
            }
        }
    }
}
