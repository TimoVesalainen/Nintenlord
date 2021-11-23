namespace Nintenlord.Collections.DisjointSet
{
    public interface IDisjointSet<T>
    {
        bool AreSameSet(T item1, T item2);
        T FindRepresentative(T item);
        void Union(T item1, T item2);
    }
}