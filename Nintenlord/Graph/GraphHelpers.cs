namespace Nintenlord.Graph
{
    using Nintenlord.Trees;
    using Nintenlord.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GraphHelpers
    {
        public static bool HasCycle<TNode>(this IGraph<TNode> graph, out List<TNode> cycle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the topological sorting of cycleless graph
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static IEnumerable<TNode> TopologicalSort<TNode>(this IGraph<TNode> graph)
        {
            return graph.DepthFirstTraversalAllNodes(
                GraphTraversal.DepthFirstTraversalOrdering.PostOrdering)
                .Reverse();
        }

        /// <summary>
        /// Returns all nodes in a directed graph with no edges pointing to it.
        /// </summary>
        /// <typeparam name="TNode">Type of nodes</typeparam>
        /// <param name="graph">Graph with nodes</param>
        /// <returns>List of top nodes</returns>
        public static List<TNode> GetTopNodes<TNode>(this IGraph<TNode> graph)
        {
            List<TNode> nodes = graph.Nodes.ToList();

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (graph.Nodes.Any(node => graph.IsEdge(node, nodes[i])))
                {
                    nodes.RemoveAt(i);
                }
            }

            return nodes;
        }

        public static IEnumerable<TEdge> GetEdges<TNode, TEdge>(this IGraph<TNode> graph, Func<TNode, TNode, TEdge> createEdge)
        {
            return from node in graph.Nodes
                   from neighbour in graph.GetNeighbours(node)
                   select createEdge(node, neighbour);
        }

        public static IEnumerable<Tuple<TNode, TNode>> GetEdges<TNode>(this IGraph<TNode> graph)
        {
            return graph.GetEdges(Tuple.Create);
        }

        public static bool IsConnected<TNode>(this IGraph<TNode> graph)
        {
            if (!graph.Nodes.Any())
            {
                return true;
            }

            var transpose = graph.GetTranspose();

            var dfs = transpose.DepthFirstTraversalAllNodes(
                GraphTraversal.DepthFirstTraversalOrdering.PostOrdering).ToList();

            int index = 0;

            while (index < dfs.Count)
            {
                int i;
                for (i = index + 1; i < dfs.Count; i++)
                {
                    if (graph.IsEdge(dfs[index], dfs[i]))
                    {
                        i = index;
                        break;
                    }
                }
                if (i == dfs.Count)
                {
                    break;
                }
            }

            return index == dfs.Count - 1;//return if reached top of transpose
        }

        public static IGraph<TNode> GetTranspose<TNode>(this IGraph<TNode> graph)
        {
            if (graph is TransposeGraph<TNode> transposeGraph)
            {
                return transposeGraph.OriginalGraph;
            }
            else
            {
                return new TransposeGraph<TNode>(graph);
            }
        }
    }
}
