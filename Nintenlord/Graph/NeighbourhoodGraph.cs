﻿using System.Collections.Generic;

namespace Nintenlord.Graph
{
    public sealed class NeighbourhoodGraph<TNode> : IEditableGraph<TNode>
    {
        private readonly Dictionary<TNode, List<TNode>> neighbours;

        public NeighbourhoodGraph(IEnumerable<TNode> nodes)
        {
            this.neighbours = new Dictionary<TNode, List<TNode>>();

            foreach (var node in nodes)
            {
                this.neighbours[node] = new List<TNode>();
            }
        }

        public NeighbourhoodGraph(IEnumerable<KeyValuePair<TNode, IEnumerable<TNode>>> neighbours)
        {
            this.neighbours = new Dictionary<TNode, List<TNode>>();

            foreach (var pair in neighbours)
            {
                this.neighbours[pair.Key] = new List<TNode>(pair.Value);
            }
        }


        #region IEditableGraph<TNode> Members

        public void SetEdge(TNode from, TNode to)
        {
            neighbours[from].Add(to);
        }

        public void RemoveEdge(TNode from, TNode to)
        {
            neighbours[from].Remove(to);
        }

        public bool this[TNode from, TNode to]
        {
            get => neighbours[from].Contains(to);
            set
            {
                if (value)
                {
                    neighbours[from].Add(to);
                }
                else
                {
                    neighbours[from].Remove(to);
                }
            }
        }

        #endregion

        #region IGraph<TNode> Members

        public int NodeCount => neighbours.Count;

        public IEnumerable<TNode> Nodes => neighbours.Keys;

        public IEnumerable<TNode> GetNeighbours(TNode node)
        {
            return neighbours[node];
        }

        public bool IsEdge(TNode from, TNode to)
        {
            return neighbours[from].Contains(to);
        }

        #endregion
    }
}
