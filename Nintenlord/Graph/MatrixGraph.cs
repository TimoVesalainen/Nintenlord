using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Graph
{
    /// <summary>
    /// Graph where adjacency is implemented as matrix.
    /// </summary>
    public sealed class MatrixGraph : IEditableGraph<int>
    {
        private readonly bool[,] neighbours;

        public MatrixGraph(int amountOfNodes)
        {
            neighbours = new bool[amountOfNodes, amountOfNodes];
            this.NodeCount = amountOfNodes;
        }

        #region IEditableGraph<int> Members

        public bool this[int from, int to]
        {
            get => neighbours[from, to];
            set => neighbours[from, to] = value;
        }

        public void RemoveEdge(int from, int to)
        {
            neighbours[from, to] = false;
        }

        public void SetEdge(int from, int to)
        {
            neighbours[from, to] = true;
        }

        #endregion

        #region IGraph<TNode> Members

        public int NodeCount
        {
            get;
        }

        public IEnumerable<int> Nodes => Enumerable.Range(0, NodeCount);

        public IEnumerable<int> GetNeighbours(int node)
        {
            for (int i = 0; i < NodeCount; i++)
            {
                if (neighbours[node, i])
                {
                    yield return i;
                }
            }
        }

        public bool IsEdge(int from, int to)
        {
            return neighbours[from, to];
        }

        #endregion         
    }
}
