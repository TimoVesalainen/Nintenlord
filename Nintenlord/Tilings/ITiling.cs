using System.Numerics;
using System.Text;

namespace Nintenlord.Tilings
{
    public interface ITiling<T>
    {
        T this[int x, int y] { get; }
    }
}
