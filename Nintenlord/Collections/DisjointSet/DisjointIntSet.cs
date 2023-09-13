using Nintenlord.Trees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.DisjointSet
{
    public sealed class DisjointIntSet : IDisjointSet<int>, IParentForest<int>, IForest<int>
    {
        readonly int[] parents;
        readonly int[] descendants;

        public int ElementCount => parents.Length;

        public IEnumerable<int> Items => Enumerable.Range(0, parents.Length);

        public DisjointIntSet(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Negative size", nameof(amount));
            }

            parents = new int[amount];
            descendants = new int[amount];

            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = i;
                descendants[i] = 1;
            }
        }

        private DisjointIntSet(int[] parents, int[] descendants)
        {
            this.parents = parents ?? throw new ArgumentNullException(nameof(parents));
            this.descendants = descendants ?? throw new ArgumentNullException(nameof(descendants));
        }

        public static DisjointIntSet Create(IEnumerable<IEnumerable<int>> partitions)
        {
            var parents = new List<int>();
            var descendants = new List<int>();

            foreach (var partition in partitions)
            {
                int parent = -1;
                int count = 0;
                foreach (var child in partition)
                {
                    if (parent == -1)
                    {
                        parent = child;
                    }
                    while (parents.Count <= child)
                    {
                        parents.Add(parents.Count);
                        descendants.Add(1);
                    }
                    parents[child] = parent;
                    count++;
                }
                if (parent != -1)
                {
                    descendants[parent] = count;
                }
            }
            return new DisjointIntSet(parents.ToArray(), descendants.ToArray());
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
