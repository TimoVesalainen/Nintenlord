using Nintenlord.Grammars;
using Nintenlord.Utility;
using System.Collections.Generic;

namespace Nintenlord.StateMachines
{
    internal class CombinedStateMachine<TState1, TState2, TInput1, TInput2> : IStateMachine<(TState1, TState2), Either<TInput1, TInput2>>
    {
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;

        public CombinedStateMachine(IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2)
        {
            this.machine1 = machine1;
            this.machine2 = machine2;
        }

        public (TState1, TState2) StartState => (machine1.StartState, machine2.StartState);

        public IEnumerable<(TState1, TState2)> GetStates()
        {
            foreach (var state1 in machine1.GetStates())
            {
                foreach (var state2 in machine2.GetStates())
                {
                    yield return (state1, state2);
                }
            }
        }

        public bool IsFinalState((TState1, TState2) state)
        {
            return machine1.IsFinalState(state.Item1) && machine2.IsFinalState(state.Item2);
        }

        public (TState1, TState2) Transition((TState1, TState2) currentState, Either<TInput1, TInput2> input)
        {
            return input.Apply(
                input1 => (machine1.Transition(currentState.Item1, input1), currentState.Item2),
                input2 => (currentState.Item1, machine2.Transition(currentState.Item2, input2))
                );
        }
    }
}
