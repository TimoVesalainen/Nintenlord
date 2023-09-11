using Nintenlord.Utility;
using NUnit.Framework;

namespace Nintenlord.Tests.Utility
{
    public class ComparableUtilsTest
    {
        [Test]
        public static void TestMin()
        {
            Assert.AreEqual(ComparableUtils.Min(0, 1), 0);
            Assert.AreEqual(ComparableUtils.Min(1, 0), 0);

            Assert.AreEqual(ComparableUtils.MinNullIsLargest<int>(1, 0), 0);
            Assert.AreEqual(ComparableUtils.MinNullIsLargest<int>(0, null), 0);
            Assert.AreEqual(ComparableUtils.MinNullIsLargest<int>(null, 0), 0);
            Assert.AreEqual(ComparableUtils.MinNullIsLargest<int>(null, null), null);

            Assert.AreEqual(ComparableUtils.MinNullIsSmallest<int>(1, 0), 0);
            Assert.AreEqual(ComparableUtils.MinNullIsSmallest<int>(0, null), null);
            Assert.AreEqual(ComparableUtils.MinNullIsSmallest<int>(null, 0), null);
            Assert.AreEqual(ComparableUtils.MinNullIsSmallest<int>(null, null), null);
        }

        [Test]
        public static void TestMax()
        {
            Assert.AreEqual(ComparableUtils.Max(0, 1), 1);
            Assert.AreEqual(ComparableUtils.Max(1, 0), 1);

            Assert.AreEqual(ComparableUtils.MaxNullIsLargest<int>(1, 0), 1);
            Assert.AreEqual(ComparableUtils.MaxNullIsLargest<int>(0, null), null);
            Assert.AreEqual(ComparableUtils.MaxNullIsLargest<int>(null, 0), null);
            Assert.AreEqual(ComparableUtils.MaxNullIsLargest<int>(null, null), null);

            Assert.AreEqual(ComparableUtils.MaxNullIsSmallest<int>(1, 0), 1);
            Assert.AreEqual(ComparableUtils.MaxNullIsSmallest<int>(0, null), 0);
            Assert.AreEqual(ComparableUtils.MaxNullIsSmallest<int>(null, 0), 0);
            Assert.AreEqual(ComparableUtils.MaxNullIsSmallest<int>(null, null), null);
        }
    }
}
