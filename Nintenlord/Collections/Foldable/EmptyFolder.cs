namespace Nintenlord.Collections.Foldable
{
    public sealed class EmptyFolder<T> : IFolder<T, bool, bool>
    {
        public static readonly EmptyFolder<T> Instance = new();

        private EmptyFolder() { }

        public bool Start => true;

        public bool Fold(bool state, T input)
        {
            return false;
        }

        public bool Transform(bool state)
        {
            return state;
        }
    }
}
