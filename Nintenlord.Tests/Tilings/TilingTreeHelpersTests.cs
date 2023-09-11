using Nintenlord.Tilings;
using Nintenlord.Trees;
using NUnit.Framework;
using System;

namespace Nintenlord.Tests.Tilings
{
    public class TilingTreeHelpersTests
    {
        IForest<string> tree;
        (int relX, int relY)[] relPositions = new[] {
                (0, 0),
                (1, 0),
                (-1, 0),
                (0, 1),
                (0, -1),
                (1, 1),
                (-1, -1),
                (-1, 1),
                (1, -1)
            };

        [SetUp]
        public void Setup()
        {

            var children = new[] {
                "Center",
                "Right",
                "Left",
                "Up",
                "Down",
                "RightUp",
                "LeftDown",
                "RightDown",
                "LeftUp"
            };

            tree = new LambdaForest<string>(_ => children);
        }

        [TestCase(5)]
        [TestCase(7)]
        [TestCase(8)]//This takes 2 minutes
        public void TestExpanding(int generations)
        {
            var toDepth = tree.GetToMaxDepth("Origin", generations);
            var (w, h) = toDepth.GetSize(_ => relPositions);

            Assert.AreEqual(Math.Pow(3, generations), w);
            Assert.AreEqual(Math.Pow(3, generations), h);
        }
    }
}
