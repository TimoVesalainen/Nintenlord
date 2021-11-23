using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Graph
{
    public class MatrixGraph<T> : IEditableGraph<T>
    {
        readonly T[] items;
        readonly MatrixIntGraph graph;

        public MatrixGraph(IEnumerable<T> items)
        {
            this.items = items.ToArray();
            graph = new MatrixIntGraph(this.items.Length);
        }

        public bool this[T from, T to]
        {
            get => IsEdge(from, to);
            set => graph[Array.IndexOf(items, from), Array.IndexOf(items, to)] = value;
        }

        public IEnumerable<T> Nodes => items.AsEnumerable();

        public IEnumerable<T> GetNeighbours(T node)
        {
            var index = Array.IndexOf(items, node);
            return graph.GetNeighbours(index).Select(i => items[i]);
        }

        public bool IsEdge(T from, T to)
        {
            return graph.IsEdge(Array.IndexOf(items, from), Array.IndexOf(items, to));
        }

        public void RemoveEdge(T from, T to)
        {
            graph.RemoveEdge(Array.IndexOf(items, from), Array.IndexOf(items, to));
        }

        public void SetEdge(T from, T to)
        {
            graph.SetEdge(Array.IndexOf(items, from), Array.IndexOf(items, to));
        }
    }
}
