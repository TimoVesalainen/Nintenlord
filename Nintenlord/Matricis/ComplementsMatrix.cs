namespace Nintenlord.Matricis
{
    public sealed class ComplementsMatrix<T> : IMatrix<IMatrix<T>>
    {
        readonly IMatrix<T> matrix;

        public ComplementsMatrix(IMatrix<T> matrix)
        {
            this.matrix = matrix;
        }

        public IMatrix<T> this[int x, int y] => new ElementComplementMatrix<T>(matrix, x, y);

        public int Width => matrix.Width;

        public int Height => matrix.Height;
    }
}
