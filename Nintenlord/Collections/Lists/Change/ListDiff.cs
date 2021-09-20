using Nintenlord.Geometry;
using Nintenlord.Utility;
using Nintenlord.Utility.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Lists.Change
{
    public static class ListDiff
    {
        public static IEnumerable<IListChange<T>> GetListDiff<T>(this IList<T> a, IList<T> b, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;

            var equals = a.TensorProduct(b, comparer.Equals);

            var diagonals = FindLargestDiagonals(equals).ToList();

            var stayedTheSame = diagonals.GetBestIncreasingSubsequence(t => t.x, t => t.length).ToList();

            List<bool> removed = a.Select(_ => true).ToList();
            List<bool> added = b.Select(_ => true).ToList();

            IComparer<(int x, int y, int length)> diagonalComparer = null;
            foreach (var diagonal in diagonals)
            {
                for (int i = diagonal.x; i < diagonal.length; i++)
                {
                    removed[i] = false;
                }
                for (int i = diagonal.y; i < diagonal.length; i++)
                {
                    added[i] = false;
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

        static IEnumerable<(int x, int y, int length)> FindLargestDiagonals(bool[,] matrix)
        {
            var yLength = matrix.GetLength(0);
            var xLength = matrix.GetLength(1);

            int[,] diagonalLengths = new int[matrix.GetLength(1), matrix.GetLength(0)];

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
                    var rawDiagonal = diagonalLengths[x, y];

                    return Math.Min(rawDiagonal, Math.Min(maxX - x, maxY - y));
                }

                var lengths = from x in IntegerExtensions.GetIntegers(minX, maxX)
                              from y in IntegerExtensions.GetIntegers(minY, maxY)
                              select (x, y, length: GetDiagonalLengthLimited(x, y));

                var longest = lengths.FindLargest(t => t.length);

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

                throw new NotImplementedException();
            }

            return FindInner(0, xLength, 0, yLength);
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
