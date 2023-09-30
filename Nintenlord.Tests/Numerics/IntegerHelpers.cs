using System;
using System.Linq;
using System.Text;
using Nintenlord.Numerics;
using NUnit.Framework;

namespace Nintenlord.Tests.Numerics
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
                new int[] { 1 , 1 , 1 , 1 , 1 } }, 5.Compositions());
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void TestCompositionsFirstAndLast(int number)
        {
            Assert.AreEqual(new int[] { number }, number.Compositions().First());
            Assert.AreEqual(Enumerable.Repeat(1, number), number.Compositions().Last());
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void TestCompositionsSum(int number)
        {
            Assert.IsTrue(number.Compositions()
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

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(23)]
        public void TestSwappingWithZero(int index)
        {
            var value = 1 << index;

            for (int i = 0; i < 31; i++)
            {
                if (i != index)
                {
                    Assert.AreEqual(1 << i, value.SwapBits(index, i));
                }
            }
        }

        [TestCase(0, 5)]
        [TestCase(14, 30)]
        [TestCase(23, 1)]
        public void TestSwappingOnes(int index1, int index2)
        {
            var value = 1 << index1 | 1 << index2;

            Assert.AreEqual(value, value.SwapBits(index1, index2));
        }

        [TestCase(0, 4)]
        [TestCase(14, 13)]
        [TestCase(23, 2)]
        public void TestSwappingWithZero2(int index1, int index2)
        {
            var value = 1 << index1;

            for (int i = 0; i < 31; i++)
            {
                if (i != index1 && i != index2)
                {
                    Assert.AreEqual(1 << i, value.SwapBits(index1, i, index2));
                }
            }
        }

        [TestCase(0, 5, 3)]
        [TestCase(14, 30, 3)]
        [TestCase(23, 1, 15)]
        public void TestSwappingOnes2(int index1, int index2, int index3)
        {
            var value = 1 << index1 | 1 << index2 | 1 << index3;

            Assert.AreEqual(value, value.SwapBits(index1, index2, index3));
        }

        [TestCase(1, 0)]
        [TestCase(3, 2)]
        [TestCase(6, 4)]
        public void TestBinomialDistribution(int row, int k)
        {
            var value1 = IntegerExtensions.BinomialCoefficient(row, k);
            var value2 = IntegerExtensions.BinomialCoefficient(row, row - k);
            Assert.AreEqual(value1, value2);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(6)]
        public void TestBinomialDistributionRows(int row)
        {
            var rowSum1 = IntegerExtensions.BinomialCoefficients(row).Sum();
            var rowSum2 = Enumerable.Range(0, row + 1).Select(m => IntegerExtensions.BinomialCoefficient(row, m)).Sum();
            Assert.AreEqual((int)Math.Pow(2, row), rowSum1);
            Assert.AreEqual((int)Math.Pow(2, row), rowSum2);
        }

        [Test]
        public void TestBinomialDistributionAll()
        {
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i + 1, IntegerExtensions.BinomialCoefficients(i).Count());
                Assert.AreEqual(i + 1, IntegerExtensions.BinomialCoefficientRows<int>().Skip(i).Count());
                Assert.AreEqual(
                    IntegerExtensions.BinomialCoefficients(i),
                    IntegerExtensions.BinomialCoefficientRows<int>().Skip(i).First());
            }
        }

        [Test]
        public void TestIntersection()
        {
            Assert.IsTrue(NumberExtensions.Intersects(1, 1, 1, 1));
            Assert.IsTrue(NumberExtensions.Intersects(1, 3, 3, 1));

            Assert.IsFalse(NumberExtensions.Intersects(1, 0, 1, 0));
            Assert.IsFalse(NumberExtensions.Intersects(1, 3, 3, 0));
            Assert.IsFalse(NumberExtensions.Intersects(1, 1, 2, 1));
        }
    }
}
