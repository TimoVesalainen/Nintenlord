namespace Nintenlord.Graph
{
    using Nintenlord.Trees;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public static class GraphTraversal
    {
        public static IEnumerable<TNode> BreadthFirstTraversal<TNode>(
            this IGraph<TNode> graph,
            TNode startNode)
        {
            HashSet<TNode> travelledNodes = new HashSet<TNode>();

            Queue<TNode> queue = new Queue<TNode>();

            queue.Enqueue(startNode);
            travelledNodes.Add(startNode);

            while (queue.Count > 0)
            {
                var nextNode = queue.Dequeue();

                yield return nextNode;

                foreach (var node in graph.GetNeighbours(nextNode))
                {
                    if (!travelledNodes.Contains(node))
                    {
                        queue.Enqueue(node);
                        travelledNodes.Add(node);
                    }
                }
            }
        }

        #region Depth first traversal

        public enum DepthFirstTraversalOrdering
        {
            PreOrdering,
            PostOrdering
        }

        public static IEnumerable<TNode> DepthFirstTraversal<TNode>(
            this IGraph<TNode> graph,
            TNode startNode,
            DepthFirstTraversalOrdering ordering)
        {
            HashSet<TNode> travelledNodes = new HashSet<TNode>();

            switch (ordering)
            {
                case DepthFirstTraversalOrdering.PreOrdering:
                    return DepthFirstTraversalVisitPreOrder(graph, startNode, travelledNodes);
                case DepthFirstTraversalOrdering.PostOrdering:
                    return DepthFirstTraversalVisitPostOrder(graph, startNode, travelledNodes);
                default:
                    throw new ArgumentException();
            }
        }

        private static IEnumerable<TNode> DepthFirstTraversalVisitPreOrder<TNode>(
            IGraph<TNode> graph, TNode node, HashSet<TNode> travelledNodes)
        {
            travelledNodes.Add(node);

            yield return node;

            foreach (var neighbours in graph.GetNeighbours(node))
            {
                foreach (var travelledNode in DepthFirstTraversalVisitPreOrder(graph, neighbours, travelledNodes))
                {
                    yield return travelledNode;
                }
            }
        }

        private static IEnumerable<TNode> DepthFirstTraversalVisitPostOrder<TNode>(
            IGraph<TNode> graph, TNode node, HashSet<TNode> travelledNodes)
        {
            travelledNodes.Add(node);

            foreach (var neighbours in graph.GetNeighbours(node))
            {
                foreach (var travelledNode in DepthFirstTraversalVisitPostOrder(graph, neighbours, travelledNodes))
                {
                    yield return travelledNode;
                }
            }

            yield return node;
        }


        public static IEnumerable<TNode> DepthFirstTraversalAllNodes<TNode>(
            this IGraph<TNode> graph,
            DepthFirstTraversalOrdering ordering)
        {
            HashSet<TNode> travelledNodes = new HashSet<TNode>();

            switch (ordering)
            {
                case DepthFirstTraversalOrdering.PreOrdering:
                    foreach (var node in graph.Nodes)
                    {
                        if (!travelledNodes.Contains(node))
                        {
                            foreach (var node2 in DepthFirstTraversalVisitPreOrder(graph, node, travelledNodes))
                            {
                                yield return node2;
                            }
                        }

                    }
                    break;
                case DepthFirstTraversalOrdering.PostOrdering:
                    foreach (var node in graph.Nodes)
                    {
                        if (!travelledNodes.Contains(node))
                        {
                            foreach (var node2 in DepthFirstTraversalVisitPostOrder(graph, node, travelledNodes))
                            {
                                yield return node2;
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        #endregion

        public static ITree<T> Traversal<T>(this IGraph<T> graph, T root, IEqualityComparer<T> equality = null)
        {
            equality ??= EqualityComparer<T>.Default;

            var parents = new Dictionary<T, T>(equality);

            void SetParents(T node)
            {
                var newChildren = new List<T>();
                foreach (var item in graph.GetNeighbours(node))
                {
                    if (!parents.ContainsKey(item))
                    {
                        parents[item] = node;
                        newChildren.Add(item);
                    }
                }
                foreach (var item in newChildren)
                {
                    SetParents(item);
                }
            }

            SetParents(root);

            return parents.ToTree(root, equality);
        }
    }
}
