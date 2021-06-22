using System.Collections.Generic;

namespace Nintenlord.Graph.Colouring
{

    public interface IColouredGraph<TNode, out TColour> : IGraph<TNode>
    {
        IEnumerable<TColour> GetColours();
    }

    public interface IVertexColouring<TNode, out TColour> : IColouredGraph<TNode, TColour>
    {
        TColour this[TNode node] { get; }
    }

    public interface IEdgeColouring<TNode, out TColour> : IColouredGraph<TNode, TColour>
    {
        TColour this[TNode startNode, TNode endNode] { get; }
    }
}
