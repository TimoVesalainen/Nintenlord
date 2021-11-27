using Nintenlord.Matricis;
using System;

namespace Nintenlord.StateMachines
{
    public sealed class IntMatrixStateMachine : IStateMachine<int, int>
    {
        readonly IMatrix<int> matrix;
        readonly Predicate<int> isFinal;

        public IntMatrixStateMachine(IMatrix<int> matrix, Predicate<int> isFinal, int startState)
        {
            this.matrix = matrix;
            this.isFinal = isFinal;
            StartState = startState;
        }

        public int StartState { get; }

        public bool IsFinalState(int state) => isFinal(state);

        public int Transition(int currentState, int input)
        {
            return matrix[currentState, input];
        }
    }
}
