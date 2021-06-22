using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Graph
{
    public sealed class TransposeGraph<TNode> : IGraph<TNode>
    {
        private readonly IGraph<TNode> originalGraph;

        public IGraph<TNode> OriginalGraph => originalGraph;

        public IEnumerable<TNode> Nodes => originalGraph.Nodes;

        public TransposeGraph(IGraph<TNode> originalGraph)
        {
            this.originalGraph = originalGraph;
        }

        #region IGraph<TNode> Members

        public IEnumerable<TNode> GetNeighbours(TNode node)
        {
            return originalGraph.Nodes.Where(node2 => this.IsEdge(node, node2));
        }

        public bool IsEdge(TNode from, TNode to)
        {
            return !originalGraph.IsEdge(from, to);
        }

        #endregion

    }
}
