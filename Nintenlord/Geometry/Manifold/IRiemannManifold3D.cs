using System.Numerics;

namespace Nintenlord.Geometry.Manifold
{
    public interface IRiemannManifold3D<T> : IManifold3D<T>
    {
        double MetricTensor(T point, Vector3 tangent1, Vector3 tangent2);
    }
}
