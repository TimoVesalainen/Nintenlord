using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public interface IForest<TNode>
    {
        IEnumerable<TNode> GetChildren(TNode node);
    }
}
