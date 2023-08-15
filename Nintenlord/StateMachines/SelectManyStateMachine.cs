using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.StateMachines
{
    public sealed class SelectManyStateMachine<TState1, TState2, TInput> : IStateMachine<(TState1, TState2), TInput>
    {
        readonly IStateMachine<TState1, TInput> outerStateMachine;
        readonly Func<TState1, IStateMachine<TState2, TInput>> innerStateMachine;

        public (TState1, TState2) StartState => (outerStateMachine.StartState, innerStateMachine(outerStateMachine.StartState).StartState);

        public bool IsFinalState((TState1, TState2) state)
        {
            return outerStateMachine.IsFinalState(state.Item1) &&
                innerStateMachine(state.Item1).IsFinalState(state.Item2);
        }

        public (TState1, TState2) Transition((TState1, TState2) currentState, TInput input)
        {
            var current = innerStateMachine(currentState.Item1);
            if (current.IsFinalState(currentState.Item2))
            {
                var newOuterState = outerStateMachine.Transition(currentState.Item1, input);
                return (newOuterState, innerStateMachine(newOuterState).StartState);
            }
            else
            {
                return (currentState.Item1, current.Transition(currentState.Item2, input));
            }
        }
    }
}
