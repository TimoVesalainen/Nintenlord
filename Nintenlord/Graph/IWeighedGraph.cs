namespace Nintenlord.Graph
{
    public interface IWeighedGraph<TNode> : IGraph<TNode>
    {
        int GetMovementCost(TNode from, TNode to);

        ICostCollection<TNode> GetTempCostCollection();
    }
}
