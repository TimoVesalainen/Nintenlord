using Nintenlord.Matrix;
using NUnit.Framework;

namespace Nintenlord.Tests.Matrix
{
    class BinaryMatrixTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(10)]
        [TestCase(15)]
        public void TestMultiplication(int index)
        {
            var identity = BinaryMatrix.GetIdentity(2);

            var other = new BinaryMatrix(2, 2);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    other[x, y] = (index & 1 << x + y * 2) != 0;
                }
            }

            Assert.AreEqual(other, other.Multiplication(identity));
            Assert.AreEqual(other, identity.Multiplication(other));

            Assert.AreEqual(other, other.LogicalMultiplication(identity));
            Assert.AreEqual(other, identity.LogicalMultiplication(other));
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(64)]
        public void TestIdentity(int dimension)
        {
            var identity = BinaryMatrix.GetIdentity(dimension);

            Assert.AreEqual(identity, identity.Multiplication(identity));
            Assert.AreEqual(identity, identity.LogicalMultiplication(identity));
        }
    }
}
