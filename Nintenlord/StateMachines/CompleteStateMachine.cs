using System;
using System.Collections.Generic;

namespace Nintenlord.Grammars
{
    public sealed class CompleteStateMachine<T> : IStateMachine<T, T>
    {
        private readonly T startState;
        private readonly IEnumerable<T> statesToUse;
        private readonly Predicate<T> finalState;

        public CompleteStateMachine(T startState, IEnumerable<T> statesToUse, Predicate<T> finalState)
        {
            this.startState = startState;
            this.statesToUse = statesToUse;
            this.finalState = finalState;
        }

        #region IStateMachine<T,T> Members

        public T StartState => startState;

        public IEnumerable<T> GetStates()
        {
            return statesToUse;
        }

        public bool IsFinalState(T state)
        {
            return finalState(state);
        }

        public T Transition(T currentState, T input)
        {
            return input;
        }

        #endregion
    }
}
