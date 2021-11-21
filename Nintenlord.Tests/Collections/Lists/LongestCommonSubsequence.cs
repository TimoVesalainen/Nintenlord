using Nintenlord.Collections.Lists;
using NUnit.Framework;
using System.Linq;

namespace Nintenlord.Tests.Collections.Lists
{
    public class LongestCommonSubsequence
    {
        [Test]
        public void Test()
        {
            var list1 = "XMJYAUZ".ToList();
            var list2 = "MZJAWXU".ToList();

            var result = list1.GetLongestCommonSubsequence(list2).ToList();
            Assert.AreEqual("MJAU", result);
        }
    }
}
