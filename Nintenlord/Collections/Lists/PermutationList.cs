using Nintenlord.Trees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections.Lists
{
    public sealed class PermutationList<T> : IReadOnlyList<T>
    {
        public static IEnumerable<PermutationList<T>> GetPermutations(IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            var array = enumerable.ToArray();
            var permutationTree = PermutationTree.ForLength(array.Length);

            IEnumerable<PermutationList<T>> Inner()
            {
                foreach (var permutation in permutationTree.GetPaths())
                {
                    var indexArray = new int[array.Length];

                    foreach (var (index, value) in permutation)
                    {
                        indexArray[index] = value;
                    }

                    yield return new PermutationList<T>(array, indexArray);
                }
            }

            return Inner();
        }

        readonly T[] originalArray;
        readonly int[] indexArray;

        private PermutationList(T[] originalArray, int[] indexArray)
        {
            this.originalArray = originalArray ?? throw new ArgumentNullException(nameof(originalArray));
            this.indexArray = indexArray ?? throw new ArgumentNullException(nameof(indexArray));
        }

        public T this[int index] => originalArray[indexArray[index]];

        public int Count => originalArray.Length;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var index in indexArray)
            {
                yield return originalArray[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
