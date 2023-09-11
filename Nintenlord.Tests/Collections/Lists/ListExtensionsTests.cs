using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Collections.Lists;
using NUnit.Framework;

namespace Nintenlord.Tests.Collections.Lists
{
    public class ListExtensionsTests
    {
        [Test]
        public void GetLongestIncreasingSubsequence()
        {
            var array = new[] { 0, 9, 1, 8, 2, 7, 3, 6, 4, 5 };

            Assert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, array.GetLongestIncreasingSubsequence().ToArray());
        }

        [Test]
        public void SortedIndexFind()
        {
            var items = Enumerable.Range(0, 100).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                Assert.AreEqual(i, items.FindSortedIndex(i));
            }
        }

        [Test]
        public void SortedIndexFind2()
        {
            var items = Enumerable.Range(0, 50).Select(x => x * 2).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                Assert.AreEqual(i / 2, items.FindSortedIndex(i));
            }
        }

        [Test]
        public void SortedInsertionMiddle1()
        {
            var items = new List<int> { 0, 2 };
            items.SortedInsert(1);

            AssertIsSorted(items);
            Assert.AreEqual(new[] { 0, 1, 2 }, items);
        }

        [Test]
        public void SortedInsertionMiddle2()
        {
            var items = new List<int> { 0, 1 };
            items.SortedInsert(1);

            AssertIsSorted(items);
            Assert.AreEqual(new[] { 0, 1, 1 }, items);
        }

        [TestCase(100)]
        public void SortedInsertionStart(int amount)
        {
            var items = Enumerable.Range(0, amount).ToList();
            items.SortedInsert(0);

            AssertIsSorted(items);
            Assert.AreEqual(Enumerable.Range(0, amount).Prepend(0).ToList(), items);
        }

        [TestCase(50)]
        public void SortedInsertionEnd(int amount)
        {
            var items = Enumerable.Range(0, amount).ToList();
            items.SortedInsert(amount);

            AssertIsSorted(items);
            Assert.AreEqual(Enumerable.Range(0, amount + 1).ToList(), items);
        }

        [Test]
        public void SortedDeletionMiddle()
        {
            var items = new List<int> { 0, 1, 2 };
            items.SortedDelete(1);

            AssertIsSorted(items);
            Assert.AreEqual(new[] { 0, 2 }, items);
        }

        [TestCase(100)]
        public void SortedDeletionStart(int amount)
        {
            var items = Enumerable.Range(0, amount).ToList();
            items.SortedDelete(0);

            AssertIsSorted(items);
            Assert.AreEqual(Enumerable.Range(1, amount - 1).ToList(), items);
        }

        [TestCase(50)]
        public void SortedDeletionEnd(int amount)
        {
            var items = Enumerable.Range(0, amount).ToList();
            items.SortedDelete(amount - 1);

            AssertIsSorted(items);
            Assert.AreEqual(Enumerable.Range(0, amount - 1).ToList(), items);
        }

        public void AssertIsSorted<T>(IList<T> list, IComparer<T> comparer = null)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            comparer ??= Comparer<T>.Default;

            for (int i = 0; i < list.Count - 1; i++)
            {
                Assert.LessOrEqual(comparer.Compare(list[i], list[i + 1]), 0, "{0} > {1}", list[i], list[i + 1]);
            }
        }
    }
}
