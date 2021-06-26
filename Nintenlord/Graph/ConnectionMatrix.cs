using Nintenlord.Matricis;
using System.Linq;

namespace Nintenlord.Graph
{
    public sealed class ConnectionMatrix<TNode> : IMatrix<bool>
    {
        readonly IGraph<TNode> graph;
        readonly TNode[] nodeArray;

        public ConnectionMatrix(IGraph<TNode> graph)
        {
            this.graph = graph;
            this.nodeArray = graph.Nodes.ToArray();
        }

        public bool this[int x, int y] => graph.IsEdge(nodeArray[x], nodeArray[y]);

        public int Width => nodeArray.Length;

        public int Height => nodeArray.Length;
    }
}
