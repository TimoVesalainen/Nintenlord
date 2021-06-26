namespace Nintenlord.Matricis
{
    public sealed class ConstMatrix<T> : IMatrix<T>
    {
        readonly T item;

        public ConstMatrix(T item, int width, int height)
        {
            this.item = item;
            Width = width;
            Height = height;
        }

        public T this[int x, int y] => item;

        public int Width { get; }

        public int Height { get; }
    }
}