using Nintenlord.Collections;
using System.Collections.Generic;

namespace Nintenlord.Graph.PathFinding
{
    //http://www.policyalmanac.org/games/aStarTutorial.htm
    //http://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html#S1

    public static class A_Star
    {
        public static List<TNode> GetPath<TNode>(TNode start, TNode goal,
            IWeighedGraph<TNode> map, IHeurestic<TNode> heurestics)
        {
            return GetPath(start, goal, map, heurestics, EqualityComparer<TNode>.Default);
        }

        public static List<TNode> GetPath<TNode>(TNode start, TNode goal,
            IWeighedGraph<TNode> map, IHeurestic<TNode> heurestics, IEqualityComparer<TNode> nodeComparer,
            IDictionary<TNode, int> costCacheG = null, IDictionary<TNode, int> costCacheH = null)
        {
            costCacheG = costCacheG ?? new Dictionary<TNode, int>();
            costCacheH = costCacheH ?? new Dictionary<TNode, int>();

            IPriorityQueue<int, TNode> open =
                new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new HashSet<TNode>(nodeComparer);
            IDictionary<TNode, TNode> parents = new Dictionary<TNode, TNode>(nodeComparer);

            open.Enqueue(start, 0);
            costCacheG[start] = 0;
            costCacheH[start] = 0;
            while (open.Count > 0 && !nodeComparer.Equals(open.Peek(), goal))
            {
                TNode current = open.Dequeue();
                closed.Add(current);

                foreach (TNode neighbour in map.GetNeighbours(current))
                {
                    int gCost = costCacheG[current] + map.GetMovementCost(current, neighbour);
                    if (costCacheG.TryGetValue(neighbour, out int oldGcost) && gCost < oldGcost)
                    {//If we found a better route to neighbour 
                        int hCost = costCacheH[neighbour];
                        open.Remove(neighbour, oldGcost + hCost);
                        closed.Remove(neighbour);

                        costCacheG[neighbour] = gCost;
                        open.Enqueue(neighbour, gCost + hCost);
                        parents[neighbour] = current;

                    }
                    else if (!closed.Contains(neighbour) && !open.Contains(neighbour))
                    {//If we got here the first time
                        int hCost = heurestics.GetCostEstimate(neighbour);
                        costCacheH[neighbour] = hCost;

                        costCacheG[neighbour] = gCost;
                        open.Enqueue(neighbour, gCost + hCost);
                        parents[neighbour] = current;
                    }
                }
            }

            if (open.Count == 0)//No path exists
            {
                return new List<TNode>();
            }

            TNode last = open.Dequeue();
            open.Clear();

            List<TNode> result = new List<TNode>();
            while (parents.ContainsKey(last))
            {
                result.Add(last);
                last = parents[last];
            }

            result.Add(last);
            result.Reverse();
            return result;
        }
    }
}
