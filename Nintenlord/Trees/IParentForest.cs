using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Trees
{
    public interface IParentForest<T>
    {
        bool TryGetParent(T child, out T parent);
    }
}
