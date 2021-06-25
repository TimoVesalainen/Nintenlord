namespace Nintenlord.Matrix
{
    public interface IMatrix<T>
    {
        T this[int x, int y] { get; }

        int Width { get; }
        int Height { get; }
    }
}