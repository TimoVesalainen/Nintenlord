using System.Collections.Generic;

namespace Nintenlord.Collections.Trees
{
    public interface ITreeNode<out TChild>
    {
        IEnumerable<TChild> GetChildren();
    }

}
