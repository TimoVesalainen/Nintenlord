namespace Nintenlord.Trees.Nodes
{
    public interface IValuedTreeNode<out T, out TChild> : ITreeNode<TChild>
        where TChild : IValuedTreeNode<T, TChild>
    {
        T Value { get; }
        bool HasValue { get; }
    }
}
