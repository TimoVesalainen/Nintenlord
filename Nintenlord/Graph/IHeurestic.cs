namespace Nintenlord.Graph
{
    public interface IHeurestic<TNode>
    {
        /// <summary>
        /// The goal this heurestics is evaluating cost to.
        /// </summary>
        TNode Goal { get; }

        /// <summary>
        /// Get's the estimated cost from the node to the goal.
        /// </summary>
        /// <param name="from">Node to start from.</param>
        /// <returns>The estimate.</returns>
        int GetCostEstimate(TNode from);
    }
}
