using Nintenlord.Numerics;
using NUnit.Framework;

namespace Nintenlord.Tests.Numerics
{
    public class DivisionHelpers
    {
        [Test]
        public void TestBase5()
        {
            Assert.AreEqual(0, (-5).ToMod(5));
            Assert.AreEqual(1, (-4).ToMod(5));
            Assert.AreEqual(2, (-3).ToMod(5));
            Assert.AreEqual(3, (-2).ToMod(5));
            Assert.AreEqual(4, (-1).ToMod(5));
            Assert.AreEqual(0, 0.ToMod(5));
            Assert.AreEqual(1, 1.ToMod(5));
            Assert.AreEqual(2, 2.ToMod(5));
            Assert.AreEqual(3, 3.ToMod(5));
            Assert.AreEqual(4, 4.ToMod(5));
            Assert.AreEqual(0, 5.ToMod(5));
        }
    }
}
