using Nintenlord.Matricis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public sealed class IntMatrixStateMachine : IFiniteStateMachine<int, int>
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

        public IEnumerable<int> States => Enumerable.Range(0, matrix.Width);

        public bool IsFinalState(int state) => isFinal(state);

        public int Transition(int currentState, int input)
        {
            return matrix[currentState, input];
        }
    }
}
