using Nintenlord.Collections.EqualityComparer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.StateMachines.Finite
{
    public sealed class DictionaryStateMachine<TState, TInput> : IFiniteStateMachine<TState, TInput>
    {
        private readonly Dictionary<(TState, TInput), TState> transitions;
        private readonly Predicate<TState> finalStates;
        private readonly TState startState;
        private readonly IEqualityComparer<TState> stateComparer;

        public DictionaryStateMachine(Dictionary<(TState, TInput), TState> transitions,
            Predicate<TState> finalStates, TState startState, IEqualityComparer<TState> stateComparer = null)
        {
            this.transitions = transitions;
            this.finalStates = finalStates;
            this.startState = startState;
            this.stateComparer = stateComparer ?? EqualityComparer<TState>.Default;
        }

        public TState StartState => startState;

        public IEnumerable<TState> States => transitions.Keys.Select(keyValuePair => keyValuePair.Item1).Distinct(stateComparer);

        public bool IsFinalState(TState state)
        {
            return finalStates(state);
        }

        public TState Transition(TState currentState, TInput input)
        {
            if (transitions.TryGetValue((currentState, input), out var result))
            {
                return result;
            }
            else
            {
                return currentState;
            }
        }
    }
}
