// -----------------------------------------------------------------------
// <copyright file="TransposeGraph.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Graph
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TransposeGraph<TNode> : IGraph<TNode>
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
