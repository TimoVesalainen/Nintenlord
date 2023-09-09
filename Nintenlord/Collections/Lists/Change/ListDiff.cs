using Nintenlord.Collections.Comparers;
using Nintenlord.Geometry;
using Nintenlord.Numerics;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Lists.Change
{
    public static class ListDiff
    {
        static readonly IComparer<(int x, int y, int length)> diagonalComparer = 
            new FunctionComparer<(int x, int y, int length)>(t => t.x);

        public static IEnumerable<IListChange<T>> GetListDiff<T>(this IReadOnlyList<T> a, IReadOnlyList<T> b, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;

            var equals = a.TensorProduct(b, comparer.Equals);

            var diagonals = FindLargestDiagonals(equals).OrderBy(t => t.x).ToList();

            var stayedTheSame = diagonals.GetBestIncreasingSubsequence(t => t.y, t => t.length).ToList();

            List<bool> removed = a.Select(_ => true).ToList();
            List<bool> added = b.Select(_ => true).ToList();

            foreach (var diagonal in diagonals)
            {
                for (int i = 0; i < diagonal.length; i++)
                {
                    removed[i + diagonal.x] = false;
                    added[i + diagonal.y] = false;
                }

                if (stayedTheSame.BinarySearch(diagonal, diagonalComparer) < 0)
                {
                    yield return IListChange<T>.Moved(diagonal.x, diagonal.y, diagonal.length, a, b);
                }
            }

            foreach (var (isRemoved, index, length) in removed.GroupWithIndex())
            {
                if (isRemoved)
                {
                    yield return IListChange<T>.Removed(index, length, a, b);
                }
            }
            foreach (var (isAdded, index, length) in added.GroupWithIndex())
            {
                if (isAdded)
                {
                    yield return IListChange<T>.Added(index, length, a, b);
                }
            }
        }

        readonly static IComparer<IEnumerable<(int x, int y, int length)>> comparer
            = new FunctionComparer<(int x, int y, int length)>(x => x.length).ToLexicographic();

        public static IEnumerable<(int x, int y, int length)> FindLargestDiagonals(bool[,] matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var yLength = matrix.GetLength(0);
            var xLength = matrix.GetLength(1);

            int[,] diagonalLengths = new int[yLength, xLength];

            for (int y = yLength - 1; y >= 0; y--)
            {
                for (int x = xLength - 1; x >= 0; x--)
                {
                    diagonalLengths[y, x] = matrix[y, x]
                        ? diagonalLengths.GetSafe(y + 1, x + 1).Select(i => i + 1).GetValueOrDefault(1)
                        : 0;
                }
            }

            IEnumerable<(int x, int y, int length)> FindInner(int minX, int maxX, int minY, int maxY)
            {
                int GetDiagonalLengthLimited(int x, int y)
                {
                    var rawDiagonal = diagonalLengths[y, x];

                    return Math.Min(rawDiagonal, Math.Min(maxX - x + 1, maxY - y + 1));
                }

                var lengths = from x in IntegerExtensions.GetIntegers(minX, maxX)
                              from y in IntegerExtensions.GetIntegers(minY, maxY)
                              select (x, y, length: GetDiagonalLengthLimited(x, y));

                var longest = lengths.Where(t => t.length > 0).FindLargest(t => t.length);

                IEnumerable<(int minX, int maxX, int minY, int maxY, SquareCorners)> GetSmallerRectangles(int x, int y, int length)
                {
                    var leftSpace = x > minX;
                    var rightSpace = x + length < maxX;
                    var topSpace = y > minY;
                    var bottomSpace = y + length < maxY;

                    if (leftSpace && topSpace)
                    {
                        yield return (minX, x, minY, y, SquareCorners.TopLeft);
                    }

                    if (leftSpace && bottomSpace)
                    {
                        yield return (minX, x, y + length, maxY, SquareCorners.BottomLeft);
                    }

                    if (rightSpace && topSpace)
                    {
                        yield return (x + length, maxX, minY, y, SquareCorners.TopRight);
                    }

                    if (rightSpace && bottomSpace)
                    {
                        yield return (x + length, maxX, y + length, maxY, SquareCorners.BottomRight);
                    }
                }

                IEnumerable<(int x, int y, int length)> GetOther(int x, int y, int length)
                {
                    if (length == 0)
                    {
                        return Enumerable.Empty<(int x, int y, int length)>();
                    }

                    var corners = GetSmallerRectangles(x, y, length);

                    var cornerSolutions = corners.Select(t => (t.Item5, FindInner(t.minX, t.maxX, t.minY, t.maxY))).ToArray();

                    var bottomLeft = cornerSolutions.FirstSafe(t => t.Item1 == SquareCorners.BottomLeft).Select(t => t.Item2);
                    var bottomRight = cornerSolutions.FirstSafe(t => t.Item1 == SquareCorners.BottomRight).Select(t => t.Item2);
                    var topLeft = cornerSolutions.FirstSafe(t => t.Item1 == SquareCorners.TopLeft).Select(t => t.Item2);
                    var topRight = cornerSolutions.FirstSafe(t => t.Item1 == SquareCorners.TopRight).Select(t => t.Item2);

                    var diagonal = topLeft.GetValueOrDefault(Enumerable.Empty<(int x, int y, int length)>()).Concat(
                        bottomRight.GetValueOrDefault(Enumerable.Empty<(int x, int y, int length)>()));

                    var reverseDiagonal = topRight.GetValueOrDefault(Enumerable.Empty<(int x, int y, int length)>()).Concat(
                        bottomLeft.GetValueOrDefault(Enumerable.Empty<(int x, int y, int length)>()));

                    return comparer.Max(diagonal, reverseDiagonal);
                }

                return longest.Select(t => GetOther(t.x, t.y, t.length).Prepend((t.x, t.y, t.length))).MaxSafe(comparer).GetValueOrDefault(Enumerable.Empty<(int x, int y, int length)>());
            }

            return FindInner(0, xLength - 1, 0, yLength - 1);
        }

        static public List<T> ApplyChange<T>(this IList<T> original, IListChange<T> change)
        {
            var items = original.ToList();
            var cache = new List<T>();
            ApplyInner(change, items, cache);
            return items;
        }

        static public List<T> ApplyChange<T>(this IList<T> original, IEnumerable<IListChange<T>> changes)
        {
            var items = original.ToList();
            var cache = new List<T>();
            foreach (var change in changes)
            {
                ApplyInner(change, items, cache);
            }
            return items;
        }

        private static void ApplyInner<T>(IListChange<T> change, List<T> items, List<T> cache)
        {
            switch (change)
            {
                case Added<T> added:
                    items.InsertRange(added.NextIndex, added.AddedItems());
                    break;
                case Moved<T> moved:
                    for (int i = 0; i < moved.OriginalLength; i++)
                    {
                        cache.Add(items[i + moved.OriginalIndex]);
                    }
                    items.RemoveRange(moved.OriginalIndex, moved.OriginalLength);
                    items.InsertRange(moved.NextIndex, cache);
                    cache.Clear();
                    break;
                case Removed<T> removed:
                    items.RemoveRange(removed.OriginalIndex, removed.OriginalLength);
                    break;
                default:
                    throw new ArgumentException($"Unknown change: {change.GetType().Name}", nameof(change));
            }
        }
    }
}
