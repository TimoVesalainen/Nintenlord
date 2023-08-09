using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public sealed class SingleStateMachine<TState, TInput> : IFiniteStateMachine<TState, TInput>
    {
        public TState StartState { get; }

        public IEnumerable<TState> States => Enumerable.Repeat(StartState, 1);

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
