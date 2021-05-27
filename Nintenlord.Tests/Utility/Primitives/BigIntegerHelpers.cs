using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Nintenlord.Utility.Primitives;
using System.Linq;

namespace Nintenlord.Tests.Utility.Primitives
{
    public class BigIntegerHelpers
    {
        [Test]
        public void TestBase10()
        {
            BigInteger value = 12345678;
            Assert.AreEqual(new[] { 8,7,6,5,4,3,2,1 }, value.BaseNRepresentation(10));
        }

        [Test]
        public void TestBase16()
        {
            BigInteger value = 0xDEADBEEF;
            Assert.AreEqual(new[] { 0xF, 0xE, 0xE, 0xB, 0xD, 0xA, 0xE, 0xD }, value.BaseNRepresentation(16));
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(17)]
        [TestCase(30)]
        public void TestPower2BaseNRepresentation(int shiftAmount)
        {
            Assert.AreEqual(Enumerable.Repeat(0, shiftAmount).Append(1),
                (BigInteger.One << shiftAmount).BaseNRepresentation(2));
        }
    }
}
