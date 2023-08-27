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

        public sealed class Builder
        {
            sealed class StateInfo
            {
                private bool isFinal = false;

                public readonly TState State;
                public readonly Dictionary<TInput, TState> Transitions;

                public bool IsFinal { get => isFinal; set => isFinal = value; }

                public StateInfo(TState state, IEqualityComparer<TInput> inputComparer)
                {
                    Transitions = new(inputComparer);
                    State = state;
                }
            }

            readonly IEqualityComparer<TState> stateComparer;
            readonly IEqualityComparer<TInput> inputComparer;
            readonly Dictionary<TState, StateInfo> states;
            TState startState = default;
            bool isStartStateSet = false;

            public Builder(IEqualityComparer<TState> stateComparer = null, IEqualityComparer<TInput> inputComparer = null)
            {
                this.stateComparer = stateComparer ?? EqualityComparer<TState>.Default;
                this.inputComparer = inputComparer ?? EqualityComparer<TInput>.Default;
                states = new(this.stateComparer);
            }

            public Builder AddState(TState state)
            {
                states[state] = new StateInfo(state, inputComparer);
                return this;
            }

            public Builder SetIsFinalState(TState state, bool isFinal)
            {
                states[state].IsFinal = isFinal;
                return this;
            }

            public Builder AddTransition(TState startState, TInput input, TState endState)
            {
                states[startState].Transitions[input] = endState;
                return this;
            }

            public Builder SetStartState(TState startState)
            {
                this.startState = startState;
                isStartStateSet = true;
                return this;
            }

            public DictionaryStateMachine<TState, TInput> Build()
            {
                if (!isStartStateSet)
                {
                    throw new InvalidOperationException("No start state set");
                }

                var finalStates = states.Where(keyValue => keyValue.Value.IsFinal)
                    .Select(keyValue => keyValue.Key)
                    .ToHashSet(stateComparer);

                var transitions = states.SelectMany(keyValue => keyValue.Value.Transitions.Select(keyValue2 => (keyValue.Key, keyValue2.Key, keyValue2.Value)))
                    .ToDictionary(t => (t.Item1, t.Item2),
                    t => t.Value,
                    TupleEqualityComparer<TState, TInput>.Create(stateComparer, inputComparer));

                return new DictionaryStateMachine<TState, TInput>(transitions, finalStates.Contains, startState, stateComparer);
            }
        }
    }
}
