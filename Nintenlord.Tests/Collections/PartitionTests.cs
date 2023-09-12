using Nintenlord.Collections;
using Nintenlord.Collections.Comparers;
using Nintenlord.StateMachines.Finite;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Tests.Collections
{
    internal class PartitionTests
    {
        [Test]
        public static void TestPartitioningByParity()
        {
            var partition = new Partition<int>(Enumerable.Range(0, 10));
            var wasSplit = partition.Split(Comparer<int>.Default.Select<int, int>(item => item % 2));

            Assert.AreEqual(true, wasSplit);
            Assert.AreEqual(2, partition.PartitionCount);
            var expected = new HashSet<HashSet<int>> { new HashSet<int> { 0, 2, 4, 6, 8 }, new HashSet<int> { 1, 3, 5, 7, 9 } };
            Assert.IsTrue(partition.GetPartitions().All(x => expected.Any(ex => ex.IsSubsetOf(x) && ex.IsSupersetOf(x))));
        }

        [Test]
        public static void TestPartitioningByHalf()
        {
            var partition = new Partition<int>(Enumerable.Range(0, 16));

            Assert.AreEqual(true, partition.SplitToHalf(Comparer<int>.Default));
            Assert.AreEqual(true, partition.SplitToHalf(Comparer<int>.Default));
            Assert.AreEqual(true, partition.SplitToHalf(Comparer<int>.Default));
            Assert.AreEqual(true, partition.SplitToHalf(Comparer<int>.Default));

            Assert.AreEqual(16, partition.PartitionCount);
            Assert.AreEqual(Enumerable.Range(0, 16).Select(_ => 1), partition.GetPartitions().Select(x => x.Count()));
        }
    }
}
