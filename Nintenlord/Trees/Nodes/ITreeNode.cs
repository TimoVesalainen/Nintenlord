using System.Collections.Generic;

namespace Nintenlord.Trees.Nodes
{
    public interface ITreeNode<out TChild>
    {
        IEnumerable<TChild> GetChildren();
    }

}
