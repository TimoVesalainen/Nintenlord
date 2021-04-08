using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Nintenlord.Collections.Comparers;

namespace Nintenlord.Tests.Collections.Comparers
{
    public class ComparerExtensionHelpers
    {
        [Test]
        public void TestMax()
        {
            Assert.AreEqual(1, Comparer<int>.Default.Max(1, 0));
        }

        [Test]
        public void TestMin()
        {
            Assert.AreEqual(0, Comparer<int>.Default.Min(1, 0));
        }
    }
}
