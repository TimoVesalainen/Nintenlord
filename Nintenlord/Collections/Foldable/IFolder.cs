namespace Nintenlord.Collections.Foldable
{
    /// <summary>
    /// Inspired by: https://github.com/Gabriella439/foldl
    /// </summary>
    public interface IFolder<in TIn, TState, out TOut>
    {
        TState Start { get; }

        TState Fold(TState state, TIn input)
        {
            var (newState, _) = FoldMaybe(state, input);
            return newState;
        }

        (TState state, bool skipRest) FoldMaybe(TState state, TIn input)
        {
            return (Fold(state, input), false);
        }

        TOut Transform(TState state);
    }
}
