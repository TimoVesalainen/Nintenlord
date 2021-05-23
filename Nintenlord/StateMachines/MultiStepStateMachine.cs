using System;
using System.Collections.Generic;

namespace Nintenlord.StateMachines
{
    public sealed class MultiStepStateMachine<TState, TInput> : IStateMachine<TState, IEnumerable<TInput>>
    {
        readonly IStateMachine<TState, TInput> original;

        public MultiStepStateMachine(IStateMachine<TState, TInput> original)
        {
            this.original = original;
        }

        public TState StartState => original.StartState;

        public bool IsFinalState(TState state)
        {
            return original.IsFinalState(state);
        }

        public TState Transition(TState currentState, IEnumerable<TInput> input)
        {
            foreach (var inputStep in input)
            {
                currentState = original.Transition(currentState, inputStep);
            }
            return currentState;
        }
    }
}
