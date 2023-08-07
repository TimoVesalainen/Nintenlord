using Nintenlord.Trees;
using Nintenlord.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Geometry.Hamming
{
    public interface IHammingCubeColouring<out T>
    {
        int Dimensions { get; }

        T GetColour(in HammingCode vertex);
    }

    public static class HammingCubeColouring
    {
        public static IHammingCubeColouring<T> Create<T>(this T[] array, int dimensions)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (typeof(T) == typeof(bool))
            {
                var bitArray = new BitArray(array as bool[]);
                return new HammingCubeBitColouring(dimensions, bitArray) as IHammingCubeColouring<T>;
            }

            return new HammingCubeColouring<T>(dimensions, array);
        }
        public static IHammingCubeColouring<T> Create<T>(this Func<HammingCode, T> colouringFunc, int dimensions)
        {
            if (colouringFunc is null)
            {
                throw new ArgumentNullException(nameof(colouringFunc));
            }

            var cube = HammingCube.ForDimension(dimensions);

            if (typeof(T) == typeof(bool))
            {
                var bitArray = new BitArray(1 << dimensions);

                var booleanFunc = colouringFunc as Func<HammingCode, bool>;
                foreach (var node in cube.Nodes)
                {
                    bitArray[node.Index] = booleanFunc(node);
                }

                return new HammingCubeBitColouring(dimensions, bitArray) as IHammingCubeColouring<T>;
            }

            var colourArray = new T[1 << dimensions];

            foreach (var node in cube.Nodes)
            {
                colourArray[node.Index] = colouringFunc(node);
            }

            return new HammingCubeColouring<T>(dimensions, colourArray);
        }

        public static bool IsMonotone<T>(this IHammingCubeColouring<T> cornerColours, IComparer<T> comparer = null)
        {
            if (cornerColours is null)
            {
                throw new ArgumentNullException(nameof(cornerColours));
            }
            comparer ??= Comparer<T>.Default;

            var cube = HammingCube.ForDimension(cornerColours.Dimensions);

            return cube.IsIncreasing(node => cornerColours.GetColour(node));
        }
    }
}
