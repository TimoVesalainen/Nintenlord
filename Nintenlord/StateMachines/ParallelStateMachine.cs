using System.Collections.Generic;

namespace Nintenlord.StateMachines
{
    public sealed class ParallelStateMachine<TState1, TState2, TInput> : IStateMachine<(TState1, TState2), TInput>
    {
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;

        public ParallelStateMachine(IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2)
        {
            this.machine1 = machine1;
            this.machine2 = machine2;
        }

        public (TState1, TState2) StartState => (machine1.StartState, machine2.StartState);

        public bool IsFinalState((TState1, TState2) state)
        {
            return machine1.IsFinalState(state.Item1) && machine2.IsFinalState(state.Item2);
        }

        public (TState1, TState2) Transition((TState1, TState2) currentState, TInput input)
        {
            return (machine1.Transition(currentState.Item1, input), machine2.Transition(currentState.Item2, input));
        }
    }
}
