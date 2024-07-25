using Nintenlord.Collections;
using Nintenlord.Collections.Foldable;
using Nintenlord.Utility;
using NUnit.Framework;
using System;
using System.Linq;

namespace Nintenlord.Tests.Collections
{
    public class FolderTests
    {
        [Test]
        public void AllFolder()
        {
            Assert.IsTrue(Folders.And.Fold(Array.Empty<bool>()));
            Assert.IsTrue(Folders.And.Fold(new[] { true }));
            Assert.IsFalse(Folders.And.Fold(new[] { false }));
            Assert.IsFalse(Folders.And.Fold(new[] { false, true }));
        }

        [Test]
        public void AnyFolder()
        {
            Assert.IsFalse(Folders.Or.Fold(Array.Empty<bool>()));
            Assert.IsTrue(Folders.Or.Fold(new[] { true }));
            Assert.IsFalse(Folders.Or.Fold(new[] { false }));
            Assert.IsTrue(Folders.Or.Fold(new[] { false, true }));
        }

        [Test]
        public void SumFolder()
        {
            Assert.AreEqual(0, Folders.SumI.Fold(Array.Empty<int>()));
            Assert.AreEqual(0, Folders.SumI.Fold(new int[] { 0 }));
            Assert.AreEqual(4, Folders.SumI.Fold(new int[] { 4 }));
            Assert.AreEqual(2, Folders.SumI.Fold(new int[] { 1, 1 }));
            Assert.AreEqual(1, Folders.SumD.Fold(new double[] { 0.5, 0.5 }));
        }

        [Test]
        public void ProductFolder()
        {
            Assert.AreEqual(1, Folders.ProductI.Fold(Array.Empty<int>()), 1);
            Assert.AreEqual(0, Folders.ProductI.Fold(new int[] { 0 }));
            Assert.AreEqual(4, Folders.ProductI.Fold(new int[] { 4 }));
            Assert.AreEqual(1, Folders.ProductI.Fold(new int[] { 1, 1 }));
            Assert.AreEqual(0.25, Folders.ProductD.Fold(new double[] { 0.5, 0.5 }));
        }

        [Test]
        public void EmptyFolder()
        {
            Assert.IsTrue(Folders.Empty<int>().Fold(Array.Empty<int>()));
            Assert.IsFalse(Folders.Empty<int>().Fold(new int[] { 0 }));
        }

        [Test]
        public void CountFolder()
        {
            Assert.AreEqual(0, Folders.CountI<int>().Fold(Array.Empty<int>()));
            Assert.AreEqual(5, Folders.CountI<int>().Fold(new[] { 1, 2, 3, 4, 5 }));
            Assert.AreEqual(5, Folders.CountI<int>(x => x % 2 == 0).Fold(Enumerable.Range(0, 10)));
        }

        [Test]
        public void MeanFolder()
        {
            Assert.AreEqual(5, Folders.AverageInteger.Fold(Enumerable.Repeat<long>(5, 5)));
            Assert.AreEqual(3, Folders.AverageInteger.Fold(new long[] { 1, 2, 3, 4, 5 }));
            Assert.AreEqual(5, Folders.AverageFloat.Fold(Enumerable.Repeat<double>(5, 5)));
            Assert.AreEqual(3, Folders.AverageFloat.Fold(new double[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void VarianceFolder()
        {
            Assert.AreEqual(0, Folders.VarianceInteger.Fold(Enumerable.Repeat<long>(5, 5)));
            Assert.AreEqual(0, Folders.VarianceFloat.Fold(Enumerable.Repeat<double>(5, 5)));
        }

        [Test]
        public void MaxMinFolder()
        {
            Assert.IsFalse(Folders.Min<int>().Fold(Array.Empty<int>()).HasValue);
            Assert.AreEqual(Maybe<int>.Just(1), Folders.Min<int>().Fold(new int[] { 1, 2, 3, 4, 5 }));
            Assert.IsFalse(Folders.Max<int>().Fold(Array.Empty<int>()).HasValue);
            Assert.AreEqual(Maybe<int>.Just(5), Folders.Max<int>().Fold(new int[] { 1, 2, 3, 4, 5 }));
            Assert.IsFalse(Folders.MinMax<int>().Fold(Array.Empty<int>()).HasValue);
            Assert.AreEqual(Maybe<(int, int)>.Just((1, 5)), Folders.MinMax<int>().Fold(new int[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void FirstLastFolder()
        {
            Assert.IsFalse(Folders.First<int>().Fold(Array.Empty<int>()).HasValue);
            Assert.AreEqual(Maybe<int>.Just(1), Folders.First<int>().Fold(new int[] { 1, 2, 3, 4, 5 }));
            Assert.IsFalse(Folders.Last<int>().Fold(Array.Empty<int>()).HasValue);
            Assert.AreEqual(Maybe<int>.Just(5), Folders.Last<int>().Fold(new int[] { 1, 2, 3, 4, 5 }));
        }
    }
}
