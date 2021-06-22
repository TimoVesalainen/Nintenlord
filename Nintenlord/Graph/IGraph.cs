using System.Collections.Generic;

namespace Nintenlord.Graph
{
    /// <summary>
    /// A graph of nodes and edges.
    /// </summary>
    public interface IGraph<TNode>
    {
        IEnumerable<TNode> Nodes { get; }

        IEnumerable<TNode> GetNeighbours(TNode node);

        bool IsEdge(TNode from, TNode to);
    }
}
