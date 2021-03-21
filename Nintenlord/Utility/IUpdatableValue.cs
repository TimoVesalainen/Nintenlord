namespace Nintenlord.Utility
{
    public interface IUpdatableValue<T>
    {
        T Value { get; }

        void NeedsUpdate();
    }
}