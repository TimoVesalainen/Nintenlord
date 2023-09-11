using System;

namespace Nintenlord.StateMachines
{
    public sealed class CompleteStateMachine<T> : IStateMachine<T, T>
    {
        private readonly T startState;
        private readonly Predicate<T> finalState;

        public CompleteStateMachine(T startState, Predicate<T> finalState)
        {
            this.startState = startState;
            this.finalState = finalState;
        }

        public T StartState => startState;

        public bool IsFinalState(T state)
        {
            return finalState(state);
        }

        public T Transition(T currentState, T input)
        {
            return input;
        }
    }
}
