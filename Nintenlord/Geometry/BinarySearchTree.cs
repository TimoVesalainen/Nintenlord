using Nintenlord.Trees;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nintenlord.Geometry
{
    public static class BinarySearchTree
    {
        public static ITree<(int start, int length)> GetTree(int itemCount)
        {
            if (itemCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemCount), itemCount, "Value can't be negative");
            }

            static ITree<(int start, int length)> Create(int length)
            {
                return binarySearchForest.SetRoot((0, length));
            }

            return treesCache.GetOrAdd(itemCount, Create);
        }

        private static IEnumerable<(int start, int length)> GetChildren((int start, int length) node)
        {
            if (node.length > 1)
            {
                var length1 = node.length / 2;

                var length2 = node.length - length1 - 1;

                if (length1 > 0)
                {
                    yield return (node.start, length1);
                }
                if (length2 > 0)
                {
                    yield return (node.start + length1 + 1, length2);
                }
            }
        }

        private static readonly IForest<(int start, int length)> binarySearchForest = new LambdaForest<(int start, int length)>(GetChildren);

        static readonly ConcurrentDictionary<int, ITree<(int, int)>> treesCache = new();
    }
}
