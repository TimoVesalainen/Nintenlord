using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Geometry.Manifold
{
    public static class ManifoldHelpers
    {
        public static IEnumerable<T> Move<T>(this IManifold3D<T> space, T startPoint, IEnumerable<Vector3> diffs)
        {
            if (space is null)
            {
                throw new ArgumentNullException(nameof(space));
            }

            if (diffs is null)
            {
                throw new ArgumentNullException(nameof(diffs));
            }

            IEnumerable<T> Inner()
            {
                using var enumerator = diffs.GetEnumerator();

                var currentPoint = startPoint;
                do
                {
                    yield return currentPoint;
                } while (enumerator.MoveNext() && space.FromTangentSpace(currentPoint, enumerator.Current, out currentPoint));
            }

            return Inner();
        }

        public static double? GetDistance<T>(this IRiemannManifold3D<T> space, T point1, T point2)
        {
            if (space.ToTangentSpace(point1, point2, out var diff))
            {
                return space.MetricTensor(point1, diff, diff);
            }
            return null;
        }

        public static double? GetDotProduct<T>(this IRiemannManifold3D<T> space, T corner, T point1, T point2)
        {
            if (space.ToTangentSpace(corner, point1, out var diff1) &&
                space.ToTangentSpace(corner, point2, out var diff2))
            {
                return space.MetricTensor(corner, diff1, diff2);
            }
            return null;
        }

        /// <returns>Angle in radians</returns>
        public static double? GetAngle<T>(this IRiemannManifold3D<T> space, T corner, T point1, T point2)
        {
            if (space.ToTangentSpace(corner, point1, out var diff1) &&
                space.ToTangentSpace(corner, point2, out var diff2))
            {
                var dot = space.MetricTensor(corner, diff1, diff2);
                return Math.Acos(dot / (space.MetricTensor(corner, diff1, diff1) * space.MetricTensor(corner, diff2, diff2)));
            }
            return null;
        }

        /// <returns>Angle in radians</returns>
        public static double? GetTriangleSumOfCorners<T>(this IRiemannManifold3D<T> space, T corner1, T corner2, T corner3)
        {
            var angle1 = space.GetAngle(corner1, corner2, corner3);
            var angle2 = space.GetAngle(corner2, corner3, corner1);
            var angle3 = space.GetAngle(corner3, corner1, corner2);

            if (angle1 != null && angle2 != null && angle3 != null)
            {
                return Math.Abs((double)angle1) + Math.Abs((double)angle2) + Math.Abs((double)angle3);
            }
            return null;
        }
    }
}
