using Nintenlord.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Geometry.Manifold
{
    /// <summary>
    /// A differentiable manifold
    /// </summary>
    public interface IDiffManifold3D<T> : IRiemannManifold3D<T>
    {
        /// <summary>
        /// Transforms vector from one coordinate frame to another
        /// </summary>
        Vector3 TransformVector(T point, Vector3 placeDiff, Vector3 vector);
    }

    public static class DifferentiaManifolds
    {
        public static IEnumerable<T> IterateGeodesicPoints<T>(IDiffManifold3D<T> manifold, T startPoint, Vector3 speed, IEnumerable<float> timeDiffs)
        {
            if (manifold is null)
            {
                throw new ArgumentNullException(nameof(manifold));
            }

            if (timeDiffs is null)
            {
                throw new ArgumentNullException(nameof(timeDiffs));
            }

            IEnumerable<T> Inner(T current, Vector3 currentFrameSpeed)
            {
                yield return current;
                foreach (var timeDiff in timeDiffs)
                {
                    var diff = timeDiff * currentFrameSpeed;
                    if (manifold.FromTangentSpace(current, diff, out var nextPoint))
                    {
                        var newSpeed = manifold.TransformVector(current, diff, currentFrameSpeed);
                        current = nextPoint;
                        currentFrameSpeed = newSpeed;
                        yield return current;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (speed == Vector3.Zero)
            {
                return timeDiffs.Select(x => startPoint).Prepend(startPoint);
            }

            return Inner(startPoint, speed);
        }

        private static readonly Vector3[] Base = new[] { Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ };

        public static float GetChristoffelSymbol2<T>(this IDiffManifold3D<T> manifold, T point, int a, int b, int c)
        {
            if (manifold is null)
            {
                throw new ArgumentNullException(nameof(manifold));
            }

            var diff = Base[c];
            var toDiff = Base[b];

            var diffed = manifold.TransformVector(point, diff, toDiff);

            return diffed[a];
        }

        public static double GetChristoffelSymbol1<T>(this IDiffManifold3D<T> manifold, T point, int a, int b, int c)
        {
            if (manifold is null)
            {
                throw new ArgumentNullException(nameof(manifold));
            }

            return Base.Select((baseVector, d) => {
                return manifold.MetricTensor(point, Base[c], baseVector) * manifold.GetChristoffelSymbol2(point, d, a, b);
            }).Sum();
        }
    }
}
