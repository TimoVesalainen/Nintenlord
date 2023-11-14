using Nintenlord.Matricis;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Geometry.Manifold
{
    public interface IManifold3D<T>
    {
        bool FromTangentSpace(T basePoint, Vector3 tangent, out T otherPoint);
        bool ToTangentSpace(T basePoint, T other, out Vector3 diff);
    }
}
