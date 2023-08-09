using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public sealed class DictionaryStateMachine<TState, TInput> : IFiniteStateMachine<TState, TInput>
    {
        private readonly Dictionary<(TState, TInput), TState> transitions;
        private readonly Predicate<TState> finalStates;
        private readonly TState startState;

        public DictionaryStateMachine(Dictionary<(TState, TInput), TState> transitions,
            Predicate<TState> finalStates, TState startState)
        {
            this.transitions = transitions;
            this.finalStates = finalStates;
            this.startState = startState;
        }

        public TState StartState => startState;

        public IEnumerable<TState> States => transitions.Keys.Select(keyValuePair => keyValuePair.Item1);

        public bool IsFinalState(TState state)
        {
            return finalStates(state);
        }

        public TState Transition(TState currentState, TInput input)
        {
            return transitions[(currentState, input)];
        }
    }
}
