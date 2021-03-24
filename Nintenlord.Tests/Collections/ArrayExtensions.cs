using NUnit.Framework;
using System.Linq;
using static Nintenlord.Collections.ArrayExtensions;

namespace Nintenlord.Tests.Collections
{
    public class ArrayExtensionsTests
    {
        [Test]
        public void TrivialEmbed()
        {
            Assert.AreEqual(
                new[] { new int[,] { { 1 } } },
                new[,] { { 1 } }.EmbedTo(0, 0, 0));
        }

        [Test]
        public void EmbedOnceEachDimension()
        {
            Assert.AreEqual(new[] {
                new int[,] { { 1, 0 },{ 0, 0 } },
                new int[,] { { 0, 1 },{ 0, 0 } },
                new int[,] { { 0, 0 },{ 1, 0 } },
                new int[,] { { 0, 0 },{ 0, 1 } }},
                new[,] { { 1 } }.EmbedTo(1, 1, 0)
            );
        }
    }
}
