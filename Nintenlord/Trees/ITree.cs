namespace Nintenlord.Trees
{
    public interface ITree<TNode> : IForest<TNode>
    {
        TNode Root { get; }
    }
}
