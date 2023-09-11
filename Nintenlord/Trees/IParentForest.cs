namespace Nintenlord.Trees
{
    public interface IParentForest<T>
    {
        bool TryGetParent(T child, out T parent);
    }
}
