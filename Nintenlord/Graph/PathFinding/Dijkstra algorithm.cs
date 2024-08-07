﻿using Nintenlord.Collections;
using Nintenlord.Collections.SkipList;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Graph.PathFinding
{
    public static class Dijkstra_algorithm
    {
        public static List<TNode> GetArea<TNode>(TNode toStartFrom, IWeighedGraph<TNode> map,
            IEqualityComparer<TNode> nodeComparer, int movementLimit, IDictionary<TNode, int> costs = null)
        {
            IPriorityQueue<int, TNode> open = new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new(nodeComparer);
            costs ??= new Dictionary<TNode, int>();

            costs[toStartFrom] = 0;
            open.Enqueue(toStartFrom, 0);

            while (open.Count > 0)
            {
                TNode node = open.Dequeue(out int cost);
                closed.Add(node);

                if (cost < movementLimit)
                {
                    foreach (TNode neighbour in map.GetNeighbours(node))
                    {
                        int newCost = cost + map.GetMovementCost(node, neighbour);
                        if (!costs.TryGetValue(neighbour, out int oldCost))
                        {
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }
                        else if (newCost < oldCost)
                        {
                            open.Remove(neighbour, oldCost);
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }
                    }
                }
            }

            List<TNode> result = new(closed.Count);
            result.AddRange(closed.Where(node => costs[node] < movementLimit));
            return result;
        }

        public static int GetCost<TNode>(TNode toStartFrom, TNode toEnd,
            IWeighedGraph<TNode> map, IEqualityComparer<TNode> nodeComparer, IDictionary<TNode, int> costs = null)
        {
            costs ??= new Dictionary<TNode, int>();
            IPriorityQueue<int, TNode> open = new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new(nodeComparer);

            costs[toStartFrom] = 0;
            open.Enqueue(toStartFrom, 0);

            while (open.Count > 0)
            {
                TNode node = open.Dequeue(out int cost);
                closed.Add(node);
                if (!costs.TryGetValue(toEnd, out int endCost) || cost < endCost)
                {
                    foreach (TNode neighbour in map.GetNeighbours(node))
                    {
                        int newCost = cost + map.GetMovementCost(node, neighbour);
                        if (!costs.TryGetValue(neighbour, out int oldCost))
                        {
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }
                        else if (newCost < oldCost)
                        {
                            open.Remove(neighbour, oldCost);
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }//http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html
                    }
                }
            }

            return closed.Contains(toEnd) ? costs[toEnd] : int.MinValue;
        }

        public static IDictionary<TNode, int> GetCosts<TNode>(TNode toStartFrom,
            IWeighedGraph<TNode> map, IEqualityComparer<TNode> nodeComparer)
        {
            IPriorityQueue<int, TNode> open = new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new(nodeComparer);
            IDictionary<TNode, int> costs = new Dictionary<TNode, int>(nodeComparer)
            {
                [toStartFrom] = 0
            };
            open.Enqueue(toStartFrom, 0);

            while (open.Count > 0)
            {
                TNode node = open.Dequeue(out int cost);
                closed.Add(node);

                foreach (TNode neighbour in map.GetNeighbours(node))
                {
                    int newCost = cost + map.GetMovementCost(node, neighbour);
                    if (!costs.TryGetValue(neighbour, out int oldCost))
                    {
                        open.Enqueue(neighbour, newCost);
                        costs[neighbour] = newCost;
                    }
                    else if (newCost < oldCost)
                    {
                        open.Remove(neighbour, oldCost);
                        open.Enqueue(neighbour, newCost);
                        costs[neighbour] = newCost;
                    }//http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html
                }
            }
            return costs;
        }

        public static HashSet<TNode> GetConnectedNodes<TNode>(TNode toStartFrom,
            IGraph<TNode> map, IEqualityComparer<TNode> nodeComparer)
        {
            var open = new Queue<TNode>();
            HashSet<TNode> closed = new(nodeComparer);

            open.Enqueue(toStartFrom);

            while (open.Count > 0)
            {
                TNode node = open.Dequeue();
                closed.Add(node);

                foreach (TNode neighbour in map.GetNeighbours(node))
                {
                    if (!closed.Contains(neighbour))
                    {
                        open.Enqueue(neighbour);
                    }
                }
            }
            return closed;
        }
    }
}
