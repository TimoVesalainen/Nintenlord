using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public sealed class DictionaryStateMachine<TState, TInput> : IStateMachine<TState, TInput>
    {
        private readonly Dictionary<(TState, TInput), TState> transitions;
        private readonly Predicate<TState> finalStates;
        private readonly IEqualityComparer<TState> stateComparer;
        private readonly TState startState;

        public DictionaryStateMachine(Dictionary<(TState, TInput), TState> transitions,
            Predicate<TState> finalStates, TState startState, IEqualityComparer<TState> stateComparer = null)
        {
            this.transitions = transitions;
            this.finalStates = finalStates;
            this.startState = startState;
            this.stateComparer = stateComparer ?? EqualityComparer<TState>.Default;
        }

        public TState StartState => startState;

        public IEnumerable<TState> GetStates()
        {
            return transitions.Keys.Select(keyValuePair => keyValuePair.Item1)
                .Concat(transitions.Values).Distinct(stateComparer);
        }

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
