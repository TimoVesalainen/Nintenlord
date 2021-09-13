using System;
using System.Linq;
using Nintenlord.Collections;
using NUnit.Framework;

namespace Nintenlord.Tests.Collections
{
    public class CollectionExtensionTests
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
    }
}
