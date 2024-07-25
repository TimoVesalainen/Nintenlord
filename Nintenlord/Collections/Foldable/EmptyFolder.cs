namespace Nintenlord.Collections.Foldable
{
    public sealed class EmptyFolder<T> : IFolder<T, bool, bool>
    {
        public static readonly EmptyFolder<T> Instance = new();

        private EmptyFolder() { }

        public bool Start => true;

        public (bool state, bool skipRest) FoldMayEnd(bool state, T input)
        {
            return (false, true);
        }

        public bool Transform(bool state)
        {
            return state;
        }
    }
}
