using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Trees
{
    /// <summary>
    /// Fisher–Yates shuffle
    /// </summary>
    public sealed class PermutationTree : ITree<(int index1, int index2)>
    {
        readonly static ConcurrentDictionary<int, PermutationTree> values = new();

        /// <param name="length">Length of the array to shuffle</param>
        public static PermutationTree ForLength(int length)
        {
            return values.GetOrAdd(length, l => new PermutationTree(l));
        }

        readonly int length;

        /// <param name="length">Length of the array to shuffle</param>
        private PermutationTree(int length)
        {
            this.length = length;
        }

        public (int index1, int index2) Root => (-1, -1);

        public IEnumerable<(int index1, int index2)> GetChildren((int index1, int index2) node)
        {
            var (index1, _) = node;
            if (index1 < length - 1)
            {
                for (int i = index1 + 1; i < length; i++)
                {
                    yield return (index1 + 1, i);
                }
            }
        }
    }
}
