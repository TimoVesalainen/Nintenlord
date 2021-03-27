using System;
using System.Collections.Generic;

namespace Nintenlord.Graph.PathFinding
{
    public class DijkstraBasedHeurestic<T> : IHeurestic<T>, IDisposable
    {
        private readonly T goal;
        private readonly IDictionary<T, int> costs;

        public DijkstraBasedHeurestic(T goal, IWeighedGraph<T> map,
            IEqualityComparer<T> comparer)
        {
            this.goal = goal;
            costs = Dijkstra_algorithm.GetCosts(goal, map, comparer);

        }
        #region IHeurestic<Tile> Members

        public T Goal => goal;

        public int GetCostEstimate(T from)
        {
            return costs[from];
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            costs.Clear();
        }

        #endregion
    }

}
