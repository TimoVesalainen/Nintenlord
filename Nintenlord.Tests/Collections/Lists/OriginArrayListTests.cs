using NUnit.Framework;
using System;
using Nintenlord.Collections.Lists;
using System.Linq;

namespace Nintenlord.Tests.Collections.Lists
{
    public class OriginArrayListTests
    {
        [Test]
        public void EmptyList()
        {
            OriginArrayList<int> empty = new OriginArrayList<int>();

            Assert.AreEqual(0, empty.Count);
            Assert.Throws<InvalidOperationException>(() => { var t = empty.FirstItem; });
            Assert.Throws<InvalidOperationException>(() => { var t = empty.LastItem; });
            Assert.Throws<InvalidOperationException>(() => { var t = empty.FirstIndex; });
            Assert.Throws<InvalidOperationException>(() => { var t = empty.LastIndex; });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(69)]
        public void SingletonList(int item)
        {
            OriginArrayList<int> list = new OriginArrayList<int>();

            list.AddFirst(item);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(item, list.FirstItem);
            Assert.AreEqual(item, list.LastItem);
            Assert.AreEqual(0, list.FirstIndex);
            Assert.AreEqual(0, list.LastIndex);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(69)]
        public void SingletonListLast(int item)
        {
            OriginArrayList<int> list = new OriginArrayList<int>();

            list.AddLast(item);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(item, list.FirstItem);
            Assert.AreEqual(item, list.LastItem);
            Assert.AreEqual(0, list.FirstIndex);
            Assert.AreEqual(0, list.LastIndex);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(69)]
        public void AddSeveralStart(int length)
        {
            OriginArrayList<int> list = new OriginArrayList<int>();

            var items = Enumerable.Range(0, length).ToArray();

            foreach (var item in items)
            {
                list.AddFirst(item);
            }

            Assert.AreEqual(length, list.Count);
            Assert.AreEqual(length - 1, list.FirstItem);
            Assert.AreEqual(0, list.LastItem);
            Assert.AreEqual(-length + 1, list.FirstIndex);
            Assert.AreEqual(0, list.LastIndex);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(69)]
        public void AddSeveralEnd(int length)
        {
            OriginArrayList<int> list = new OriginArrayList<int>();

            var items = Enumerable.Range(0, length).ToArray();

            foreach (var item in items)
            {
                list.AddLast(item);
            }

            Assert.AreEqual(length, list.Count);
            Assert.AreEqual(0, list.FirstItem);
            Assert.AreEqual(length - 1, list.LastItem);
            Assert.AreEqual(0, list.FirstIndex);
            Assert.AreEqual(length - 1, list.LastIndex);
        }
    }
}
