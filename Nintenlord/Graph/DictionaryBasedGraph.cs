using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Graph
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class DictionaryBasedGraph<TNode> : IGraph<TNode>
    {
        private readonly IDictionary<TNode, IEnumerable<TNode>> neighbours;

        public DictionaryBasedGraph(IDictionary<TNode, IEnumerable<TNode>> neighbours)
        {
            this.neighbours = neighbours;
        }

        #region IGraph<T> Members

        public int NodeCount => neighbours.Count;

        public IEnumerable<TNode> Nodes => neighbours.Keys;

        public IEnumerable<TNode> GetNeighbours(TNode node)
        {
            return neighbours[node];
        }

        public bool IsEdge(TNode node1, TNode node2)
        {
            return neighbours[node1].Contains(node2);
        }

        #endregion
    }
}
