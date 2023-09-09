namespace Nintenlord.Collections.Foldable
{
    /// <summary>
    /// Inspired by: https://github.com/Gabriella439/foldl
    /// </summary>
    public interface IFolder<in TIn, TState, out TOut>
    {
        TState Start { get; }

        TState Fold(TState state, TIn input);

        TOut Transform(TState state);
    }
}
