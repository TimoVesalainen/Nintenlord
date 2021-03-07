using System.Collections.Generic;

namespace Nintenlord.Collections.Trees
{
    public interface ITree<out TChild>
    {
        IEnumerable<TChild> GetChildren();
    }

}
