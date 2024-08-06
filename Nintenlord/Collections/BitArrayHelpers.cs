using Nintenlord.Trees;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections
{
    public static class BitArrayHelpers
    {
        /// <remarks>BitArray instance is shared, clone it if you need to preserve values</remarks>
        /// <returns>A shared BitArray instance with bits set</returns>
        public static IEnumerable<BitArray> GetAllArrays(int length)
        {
            var bitArray = new BitArray(length);
            var children = new[] { true, false };
            var tree = new LambdaTree<bool>(true, _ => children).GetToMaxDepth(length + 1);
            foreach (var child in tree.GetPaths(x => x.Item1))
            {
                for (int i = 0; i < length; i++)
                {
                    bitArray[i] = child[i + 1]; //Skip the root, which is always true
                    yield return bitArray;
                }
            }
        }

        public static IEnumerable<bool> EnumerateBits(this BitArray bits)
        {
            return Enumerable.Range(0, bits.Length).Select(i => bits[i]);
        }
    }
}
