namespace Nintenlord.Tilings
{
    public interface ITiling<T>
    {
        T this[int x, int y] { get; }
    }
}
