using Nintenlord.Numerics;
using Nintenlord.Trees;
using System;
using System.Collections.Generic;

namespace Nintenlord.Geometry.Hamming
{
    public static class HammingCubeIsometries
    {
        public static IEnumerable<Func<HammingCode, HammingCode>> GetIsometries(int dimensions)
        {
            var maxInvert = 1 << dimensions;

            var permutationTree = PermutationTree.ForLength(dimensions);

            foreach (var permutation in permutationTree.GetPaths())
            {
                for (int toInvert = 0; toInvert < maxInvert; toInvert++)
                {
                    HammingCode Isometry(HammingCode original)
                    {
                        var value = original.Index;

                        for (int j = 1; j < permutation.Count; j++)
                        {
                            var (index1, index2) = permutation[j];

                            if (index1 != index2)
                            {
                                value = value.SwapBits(index1, index2);
                            }
                        }

                        return new HammingCode(value ^ toInvert, dimensions);
                    }

                    yield return Isometry;
                }
            }
        }
    }
}
