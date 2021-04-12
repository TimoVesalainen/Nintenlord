using Nintenlord.Utility;
using System.Collections.Generic;

namespace Nintenlord.StateMachines
{
    public class CombinedStateMachine<TState1, TState2, TInput1, TInput2> : IStateMachine<(TState1, TState2), Either<TInput1, TInput2>>
    {
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;

        public CombinedStateMachine(IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2)
        {
            this.machine1 = machine1;
            this.machine2 = machine2;
        }

        public (TState1, TState2) StartState => (machine1.StartState, machine2.StartState);

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

    public class CombinedStateMachine<TState1, TState2, TState3, TInput1, TInput2, TInput3> : IStateMachine<(TState1, TState2, TState3), Either<TInput1, TInput2, TInput3>>
    {
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;
        private readonly IStateMachine<TState3, TInput3> machine3;

        public CombinedStateMachine(IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2, IStateMachine<TState3, TInput3> machine3)
        {
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
        }

        public (TState1, TState2, TState3) StartState => (machine1.StartState, machine2.StartState, machine3.StartState);

        public bool IsFinalState((TState1, TState2, TState3) state)
        {
            return machine1.IsFinalState(state.Item1) && machine2.IsFinalState(state.Item2) && machine3.IsFinalState(state.Item3);
        }

        public (TState1, TState2, TState3) Transition((TState1, TState2, TState3) currentState, Either<TInput1, TInput2, TInput3> input)
        {
            return input.Apply(
                input1 => (machine1.Transition(currentState.Item1, input1), currentState.Item2, currentState.Item3),
                input2 => (currentState.Item1, machine2.Transition(currentState.Item2, input2), currentState.Item3),
                input3 => (currentState.Item1, currentState.Item2, machine3.Transition(currentState.Item3, input3))
                );
        }
    }
}
