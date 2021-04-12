using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Nintenlord.Utility.Primitives;

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
    }
}
