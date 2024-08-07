﻿using Nintenlord.Trees;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Tilings
{
    public static class TilingTreeHelpers
    {
        public static (int width, int height) GetSize<T>(this ITree<T> tree, Func<T, IEnumerable<(int relX, int relY)>> getChildRelativePosition)
        {
            Dictionary<int, int> widths = new();
            Dictionary<int, int> heights = new();

            (int x, int y, int w, int h) Combine((T, Maybe<(int x, int y)>) parent, IEnumerable<(int x, int y, int w, int h)> childRects)
            {
                var (node, maybeRelPos) = parent;
                var (x, y) = maybeRelPos.GetValueOrDefault((0, 0));

                if (!childRects.Any())
                {
                    return (x, y, 1, 1);
                }
                else
                {
                    var rects = childRects.ToArray();
                    widths.Clear();
                    heights.Clear();

                    foreach (var (cx, cy, cw, ch) in rects)
                    {
                        if (!widths.TryGetValue(cx, out var oldWidth))
                        {
                            widths[cx] = cw;
                        }
                        else if (oldWidth != cw)
                        {
                            throw new Exception($"Widths in {cx} disagree, {oldWidth} VS {cw}");
                        }

                        if (!heights.TryGetValue(cy, out var oldHeight))
                        {
                            heights[cy] = ch;
                        }
                        else if (oldHeight != ch)
                        {
                            throw new Exception($"Heights in {cy} disagree, {oldHeight} VS {ch}");
                        }
                    }

                    var width = widths.Values.Sum();
                    var height = heights.Values.Sum();

                    int newX = x - Enumerable.Range(0, -widths.Keys.First()).Select(i => widths[i]).Sum();
                    int newY = y - Enumerable.Range(0, -heights.Keys.First()).Select(i => heights[i]).Sum();

                    return (newX, newY, width, height);
                }
            }

            var zipped = tree.ZipChildren(getChildRelativePosition);

            var (xs, ys, w, h) = zipped.Aggregate<(int x, int y, int w, int h), (T, Maybe<(int x, int y)>)>(Combine);

            return (w, h);
        }
    }
}
