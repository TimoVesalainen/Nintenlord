using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility.Primitives;
using NUnit.Framework;

namespace Nintenlord.Tests.Utility.Primitives
{
    public class IntegerHelpers
    {
        [Test]
        public void TestCompositions()
        {
            Assert.AreEqual(new int[][] { 
                new int[] { 5 },
                new int[] { 4 , 1},
                new int[] { 3 , 2},
                new int[] {3 , 1 , 1},
                new int[] {2 , 3},
                new int[] { 2 , 2 , 1},
                new int[] { 2 , 1 , 2},
                new int[] { 2 , 1 , 1 , 1},
                new int[] { 1 , 4},
                new int[] { 1 , 3 , 1},
                new int[] { 1 , 2 , 2},
                new int[] { 1 , 2 , 1 , 1},
                new int[] { 1 , 1 , 3},
                new int[] { 1 , 1 , 2 , 1},
                new int[] { 1 , 1 , 1 , 2},
                new int[] { 1 , 1 , 1 , 1 , 1 } }, IntegerExtensions.Compositions(5));
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void TestCompositionsFirstAndLast(int number)
        {
            Assert.AreEqual(new int[] { number }, IntegerExtensions.Compositions(number).First());
            Assert.AreEqual(Enumerable.Repeat(1, number), IntegerExtensions.Compositions(number).Last());
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void TestCompositionsSum(int number)
        {
            Assert.IsTrue(IntegerExtensions.Compositions(number)
                .Select(composition => composition.Sum() == number).All(x => x));
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(17)]
        [TestCase(30)]
        public void TestPower2BaseNRepresentation(int shiftAmount)
        {
            Assert.AreEqual(Enumerable.Repeat(0, shiftAmount).Append(1),
                (1 << shiftAmount).BaseNRepresentation(2));
        }
    }
}
