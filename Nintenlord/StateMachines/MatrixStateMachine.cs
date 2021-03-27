using System;
using System.Collections.Generic;

namespace Nintenlord.StateMachines
{
    public sealed class MatrixStateMachine<TState, TInput> : IStateMachine<TState, TInput>
    {
        private readonly TState[] states;
        private readonly TInput[] inputs;
        private readonly TState[,] outputs;
        private readonly int[] finalStates;
        private readonly IEqualityComparer<TState> stateComparer;
        private readonly IEqualityComparer<TInput> inputComparer;

        public MatrixStateMachine(TState[] states, TInput[] inputs, TState[,] outputs, int[] finalStates,
            IEqualityComparer<TState> stateComparer = null, IEqualityComparer<TInput> inputComparer = null)
        {
            this.states = states ?? throw new ArgumentNullException(nameof(states));
            this.inputs = inputs ?? throw new ArgumentNullException(nameof(inputs));
            this.outputs = outputs ?? throw new ArgumentNullException(nameof(outputs));
            this.finalStates = finalStates ?? throw new ArgumentNullException(nameof(finalStates));
            this.stateComparer = stateComparer ?? EqualityComparer<TState>.Default;
            this.inputComparer = inputComparer ?? EqualityComparer<TInput>.Default;
        }

        public TState StartState => states[0];

        public IEnumerable<TState> GetStates()
        {
            return states;
        }

        public bool IsFinalState(TState state)
        {
            for (int i = 0; i < finalStates.Length; i++)
            {
                if (Equals(states[finalStates[i]], state))
                {
                    return true;
                }
            }
            return false;
        }

        public TState Transition(TState currentState, TInput input)
        {
            var stateIndex = Array.FindIndex(states, state => stateComparer.Equals(state, currentState));
            var inputIndex = Array.FindIndex(inputs, input2 => inputComparer.Equals(input2, input));

            return outputs[stateIndex, inputIndex];
        }
    }
}
