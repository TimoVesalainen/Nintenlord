using Nintenlord.ProceduralGeneration;
using Nintenlord.Trees;
using NUnit.Framework;
using System.Linq;

namespace Nintenlord.Tests.ProceduralGeneration
{
    public class LindenmayerSystemTest
    {
        [Test]
        public void TestAlgae()
        {
            Assert.AreEqual(
                new[] {
                    "A",
                    "AB",
                    "ABA",
                    "ABAAB",
                    "ABAABABA",
                    "ABAABABAABAAB",
                    "ABAABABAABAABABAABABA",
                    "ABAABABAABAABABAABABAABAABABAABAAB"
                },
                LindenmayerSystemHelpers.Algae.GetGenerations().Take(8));
        }

        [Test]
        public void TestFractalTree()
        {
            Assert.AreEqual(
                new[] {
                    "0",
                    "1[0]0",
                    "11[1[0]0]1[0]0",
                    "1111[11[1[0]0]1[0]0]11[1[0]0]1[0]0"
                },
                LindenmayerSystemHelpers.FractalTree.GetGenerations().Take(4));
        }

        [Test]
        public void TestCantorSet()
        {
            Assert.AreEqual(
                new[] {
                    "A",
                    "ABA",
                    "ABABBBABA",
                    "ABABBBABABBBBBBBBBABABBBABA"
                },
                LindenmayerSystemHelpers.CantorSet.GetGenerations().Take(4));
        }

        [Test]
        public void TestKochCurve()
        {
            Assert.AreEqual(
                new[] {
                    "F",
                    "F+F-F-F+F",
                    "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F",
                    "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F+" +
                    "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F-" +
                    "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F-" +
                    "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F+" +
                    "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F"
                },
                LindenmayerSystemHelpers.KochCurve.GetGenerations().Take(4));
        }
    }
}
