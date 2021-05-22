using Nintenlord.Tilings;
using Nintenlord.Trees;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;

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

        [Test]
        public void TestExpanding()
        {
            var toDepth = tree.GetToMaxDepth("Origin", 5);
            var text = toDepth.PrettyPrint();
            var (w, h) = toDepth.GetSize(_ => relPositions);

            Assert.AreEqual(Math.Pow(3, 5), w);
            Assert.AreEqual(Math.Pow(3, 5), h);
        }
    }
}
