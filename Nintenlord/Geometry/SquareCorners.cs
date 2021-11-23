using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Geometry
{
    public enum SquareCorners
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public static class SquareCornersHelpers
    {
        public static SquareCorners OppositeCorner(this SquareCorners corner)
        {
            switch (corner)
            {
                case SquareCorners.TopLeft:
                    return SquareCorners.BottomRight;
                case SquareCorners.TopRight:
                    return SquareCorners.BottomLeft;
                case SquareCorners.BottomLeft:
                    return SquareCorners.TopRight;
                case SquareCorners.BottomRight:
                    return SquareCorners.TopLeft;
                default:
                    throw new ArgumentOutOfRangeException(nameof(corner), corner, "Invalid corner");
            }
        }
    }
}
