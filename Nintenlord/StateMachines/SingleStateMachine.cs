namespace Nintenlord.StateMachines
{
    public sealed class SingleStateMachine<TState, TInput> : IStateMachine<TState, TInput>
    {
        public TState StartState { get; }

        public SingleStateMachine(TState startState)
        {
            StartState = startState;
        }

        public bool IsFinalState(TState state)
        {
            return false;
        }

        public TState Transition(TState currentState, TInput input)
        {
            return StartState;
        }
    }
}
