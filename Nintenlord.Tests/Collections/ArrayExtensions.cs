﻿using NUnit.Framework;
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

        [Test]
        public void EmbedOnceEachDimensionBigger()
        {
            Assert.AreEqual(new[] {
                new int[,] { { 1, 2, 0 }, { 3, 4, 0 }, { 0, 0, 0 } },
                new int[,] { { 0, 1, 2 }, { 0, 3, 4 }, { 0, 0, 0 } },
                new int[,] { { 0, 0, 0 }, { 1, 2, 0 }, { 3, 4, 0 } },
                new int[,] { { 0, 0, 0 }, { 0, 1, 2 }, { 0, 3, 4 } }},
                new[,] { { 1, 2 }, { 3, 4 } }.EmbedTo(1, 1, 0)
            );
        }
    }
}
