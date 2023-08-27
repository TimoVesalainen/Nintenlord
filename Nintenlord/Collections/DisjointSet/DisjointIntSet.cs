using Nintenlord.Trees;
using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.DisjointSet
{
    public sealed class DisjointIntSet : IDisjointSet<int>, IParentForest<int>, IForest<int>
    {
        readonly int[] parents;
        readonly int[] descendants;

        public int ElementCount => parents.Length;

        public DisjointIntSet(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount), "Negative size");
            }

            parents = new int[amount];

            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = i;
                descendants[i] = 1;
            }
        }

        public int FindRepresentative(int item)
        {
            if (parents[item] != item)
            {
                var parent = FindRepresentative(parents[item]);
                parents[item] = parent;
                return parent;
            }
            else
            {
                return item;
            }
        }

        public bool Union(int item1, int item2)
        {
            var index1 = FindRepresentative(item1);
            var index2 = FindRepresentative(item2);

            if (index1 == index2)
            {
                return false;
            }

            int parentIndex;
            int childIndex;
            if (descendants[index1] < descendants[index2])
            {
                parentIndex = index2;
                childIndex = index1;
            }
            else
            {
                parentIndex = index1;
                childIndex = index2;
            }

            parents[childIndex] = parentIndex;
            descendants[parentIndex] += descendants[childIndex];
            return true;
        }

        public bool AreSameSet(int item1, int item2)
        {
            return FindRepresentative(item1) == FindRepresentative(item2);
        }

        public bool TryGetParent(int child, out int parent)
        {
            parent = parents[child];
            return parents[child] != child;
        }

        public IEnumerable<int> GetChildren(int node)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                if (parents[i] == node)
                {
                    yield return i;
                }
            }
        }
    }
}
