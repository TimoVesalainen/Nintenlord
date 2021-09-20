using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
