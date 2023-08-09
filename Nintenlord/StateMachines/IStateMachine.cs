using System.Collections;

namespace Nintenlord.StateMachines
{
    public interface IStateMachine<TState, in TInput>
    {
        TState StartState { get; }
        bool IsFinalState(TState state);
        TState Transition(TState currentState, TInput input);
    }
}
