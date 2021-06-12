﻿using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class IndexNTree : ITree<int>
    {
        private static readonly ConcurrentDictionary<int, IndexNTree> indexTreeCache = new ConcurrentDictionary<int, IndexNTree>();

        public static IndexNTree GetTree(int n)
        {
            return indexTreeCache.GetOrAdd(n, n2 => new IndexNTree(n2));
        }

        private readonly int n;

        public int Root => 1;

        private IndexNTree(int n)
        {
            this.n = n;
        }

        public IEnumerable<int> GetChildren(int node)
        {
            var firstChild = node * n;
            for (int i = 0; i < n; i++)
            {
                yield return firstChild + i;
            }
        }

        public IEnumerable<int> GetParentIndicis(int startIndex)
        {
            while (startIndex > 0)
            {
                yield return startIndex;
                startIndex /= n;
            }
        }
    }
}
