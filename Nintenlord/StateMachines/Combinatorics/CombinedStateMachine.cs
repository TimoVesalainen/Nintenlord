using Nintenlord.Utility;

namespace Nintenlord.StateMachines
{
    public sealed class CombinedStateMachine<TState0, TState1, TInput0, TInput1> : IStateMachine<(TState0, TState1), Either<TInput0, TInput1>>
    {
        private readonly IStateMachine<TState0, TInput0> machine0;
        private readonly IStateMachine<TState1, TInput1> machine1;

        public CombinedStateMachine(IStateMachine<TState0, TInput0> machine0, IStateMachine<TState1, TInput1> machine1)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
        }

        public (TState0, TState1) StartState => (machine0.StartState, machine1.StartState);

        public bool IsFinalState((TState0, TState1) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2);
        }

        public (TState0, TState1) Transition((TState0, TState1) currentState, Either<TInput0, TInput1> input)
        {
            return input.Apply(
                input0 => (machine0.Transition(currentState.Item1, input0), currentState.Item2),
                input1 => (currentState.Item1, machine1.Transition(currentState.Item2, input1))
                );
        }
    }

    public sealed class CombinedStateMachine<TState0, TState1, TState2, TInput0, TInput1, TInput2> : IStateMachine<(TState0, TState1, TState2), Either<TInput0, TInput1, TInput2>>
    {
        private readonly IStateMachine<TState0, TInput0> machine0;
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;

        public CombinedStateMachine(IStateMachine<TState0, TInput0> machine0, IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
        }

        public (TState0, TState1, TState2) StartState => (machine0.StartState, machine1.StartState, machine2.StartState);

        public bool IsFinalState((TState0, TState1, TState2) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2) && machine2.IsFinalState(state.Item3);
        }

        public (TState0, TState1, TState2) Transition((TState0, TState1, TState2) currentState, Either<TInput0, TInput1, TInput2> input)
        {
            return input.Apply(
                input0 => (machine0.Transition(currentState.Item1, input0), currentState.Item2, currentState.Item3),
                input1 => (currentState.Item1, machine1.Transition(currentState.Item2, input1), currentState.Item3),
                input2 => (currentState.Item1, currentState.Item2, machine2.Transition(currentState.Item3, input2))
                );
        }
    }

    public sealed class CombinedStateMachine<TState0, TState1, TState2, TState3, TInput0, TInput1, TInput2, TInput3> : IStateMachine<(TState0, TState1, TState2, TState3), Either<TInput0, TInput1, TInput2, TInput3>>
    {
        private readonly IStateMachine<TState0, TInput0> machine0;
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;
        private readonly IStateMachine<TState3, TInput3> machine3;

        public CombinedStateMachine(IStateMachine<TState0, TInput0> machine0, IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2, IStateMachine<TState3, TInput3> machine3)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
        }

        public (TState0, TState1, TState2, TState3) StartState => (machine0.StartState, machine1.StartState, machine2.StartState, machine3.StartState);

        public bool IsFinalState((TState0, TState1, TState2, TState3) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2) && machine2.IsFinalState(state.Item3) && machine3.IsFinalState(state.Item4);
        }

        public (TState0, TState1, TState2, TState3) Transition((TState0, TState1, TState2, TState3) currentState, Either<TInput0, TInput1, TInput2, TInput3> input)
        {
            return input.Apply(
                input0 => (machine0.Transition(currentState.Item1, input0), currentState.Item2, currentState.Item3, currentState.Item4),
                input1 => (currentState.Item1, machine1.Transition(currentState.Item2, input1), currentState.Item3, currentState.Item4),
                input2 => (currentState.Item1, currentState.Item2, machine2.Transition(currentState.Item3, input2), currentState.Item4),
                input3 => (currentState.Item1, currentState.Item2, currentState.Item3, machine3.Transition(currentState.Item4, input3))
                );
        }
    }

    public sealed class CombinedStateMachine<TState0, TState1, TState2, TState3, TState4, TInput0, TInput1, TInput2, TInput3, TInput4> : IStateMachine<(TState0, TState1, TState2, TState3, TState4), Either<TInput0, TInput1, TInput2, TInput3, TInput4>>
    {
        private readonly IStateMachine<TState0, TInput0> machine0;
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;
        private readonly IStateMachine<TState3, TInput3> machine3;
        private readonly IStateMachine<TState4, TInput4> machine4;

        public CombinedStateMachine(IStateMachine<TState0, TInput0> machine0, IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2, IStateMachine<TState3, TInput3> machine3, IStateMachine<TState4, TInput4> machine4)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            this.machine4 = machine4;
        }

        public (TState0, TState1, TState2, TState3, TState4) StartState => (machine0.StartState, machine1.StartState, machine2.StartState, machine3.StartState, machine4.StartState);

        public bool IsFinalState((TState0, TState1, TState2, TState3, TState4) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2) && machine2.IsFinalState(state.Item3) && machine3.IsFinalState(state.Item4) && machine4.IsFinalState(state.Item5);
        }

        public (TState0, TState1, TState2, TState3, TState4) Transition((TState0, TState1, TState2, TState3, TState4) currentState, Either<TInput0, TInput1, TInput2, TInput3, TInput4> input)
        {
            return input.Apply(
                input0 => (machine0.Transition(currentState.Item1, input0), currentState.Item2, currentState.Item3, currentState.Item4, currentState.Item5),
                input1 => (currentState.Item1, machine1.Transition(currentState.Item2, input1), currentState.Item3, currentState.Item4, currentState.Item5),
                input2 => (currentState.Item1, currentState.Item2, machine2.Transition(currentState.Item3, input2), currentState.Item4, currentState.Item5),
                input3 => (currentState.Item1, currentState.Item2, currentState.Item3, machine3.Transition(currentState.Item4, input3), currentState.Item5),
                input4 => (currentState.Item1, currentState.Item2, currentState.Item3, currentState.Item4, machine4.Transition(currentState.Item5, input4))
                );
        }
    }

    public sealed class CombinedStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TInput0, TInput1, TInput2, TInput3, TInput4, TInput5> : IStateMachine<(TState0, TState1, TState2, TState3, TState4, TState5), Either<TInput0, TInput1, TInput2, TInput3, TInput4, TInput5>>
    {
        private readonly IStateMachine<TState0, TInput0> machine0;
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;
        private readonly IStateMachine<TState3, TInput3> machine3;
        private readonly IStateMachine<TState4, TInput4> machine4;
        private readonly IStateMachine<TState5, TInput5> machine5;

        public CombinedStateMachine(IStateMachine<TState0, TInput0> machine0, IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2, IStateMachine<TState3, TInput3> machine3, IStateMachine<TState4, TInput4> machine4, IStateMachine<TState5, TInput5> machine5)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            this.machine4 = machine4;
            this.machine5 = machine5;
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5) StartState => (machine0.StartState, machine1.StartState, machine2.StartState, machine3.StartState, machine4.StartState, machine5.StartState);

        public bool IsFinalState((TState0, TState1, TState2, TState3, TState4, TState5) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2) && machine2.IsFinalState(state.Item3) && machine3.IsFinalState(state.Item4) && machine4.IsFinalState(state.Item5) && machine5.IsFinalState(state.Item6);
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5) Transition((TState0, TState1, TState2, TState3, TState4, TState5) currentState, Either<TInput0, TInput1, TInput2, TInput3, TInput4, TInput5> input)
        {
            return input.Apply(
                input0 => (machine0.Transition(currentState.Item1, input0), currentState.Item2, currentState.Item3, currentState.Item4, currentState.Item5, currentState.Item6),
                input1 => (currentState.Item1, machine1.Transition(currentState.Item2, input1), currentState.Item3, currentState.Item4, currentState.Item5, currentState.Item6),
                input2 => (currentState.Item1, currentState.Item2, machine2.Transition(currentState.Item3, input2), currentState.Item4, currentState.Item5, currentState.Item6),
                input3 => (currentState.Item1, currentState.Item2, currentState.Item3, machine3.Transition(currentState.Item4, input3), currentState.Item5, currentState.Item6),
                input4 => (currentState.Item1, currentState.Item2, currentState.Item3, currentState.Item4, machine4.Transition(currentState.Item5, input4), currentState.Item6),
                input5 => (currentState.Item1, currentState.Item2, currentState.Item3, currentState.Item4, currentState.Item5, machine5.Transition(currentState.Item6, input5))
                );
        }
    }

    public sealed class CombinedStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TState6, TInput0, TInput1, TInput2, TInput3, TInput4, TInput5, TInput6> : IStateMachine<(TState0, TState1, TState2, TState3, TState4, TState5, TState6), Either<TInput0, TInput1, TInput2, TInput3, TInput4, TInput5, TInput6>>
    {
        private readonly IStateMachine<TState0, TInput0> machine0;
        private readonly IStateMachine<TState1, TInput1> machine1;
        private readonly IStateMachine<TState2, TInput2> machine2;
        private readonly IStateMachine<TState3, TInput3> machine3;
        private readonly IStateMachine<TState4, TInput4> machine4;
        private readonly IStateMachine<TState5, TInput5> machine5;
        private readonly IStateMachine<TState6, TInput6> machine6;

        public CombinedStateMachine(IStateMachine<TState0, TInput0> machine0, IStateMachine<TState1, TInput1> machine1, IStateMachine<TState2, TInput2> machine2, IStateMachine<TState3, TInput3> machine3, IStateMachine<TState4, TInput4> machine4, IStateMachine<TState5, TInput5> machine5, IStateMachine<TState6, TInput6> machine6)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            this.machine4 = machine4;
            this.machine5 = machine5;
            this.machine6 = machine6;
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) StartState => (machine0.StartState, machine1.StartState, machine2.StartState, machine3.StartState, machine4.StartState, machine5.StartState, machine6.StartState);

        public bool IsFinalState((TState0, TState1, TState2, TState3, TState4, TState5, TState6) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2) && machine2.IsFinalState(state.Item3) && machine3.IsFinalState(state.Item4) && machine4.IsFinalState(state.Item5) && machine5.IsFinalState(state.Item6) && machine6.IsFinalState(state.Item7);
        }

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) Transition((TState0, TState1, TState2, TState3, TState4, TState5, TState6) currentState, Either<TInput0, TInput1, TInput2, TInput3, TInput4, TInput5, TInput6> input)
        {
            return input.Apply(
                input0 => (machine0.Transition(currentState.Item1, input0), currentState.Item2, currentState.Item3, currentState.Item4, currentState.Item5, currentState.Item6, currentState.Item7),
                input1 => (currentState.Item1, machine1.Transition(currentState.Item2, input1), currentState.Item3, currentState.Item4, currentState.Item5, currentState.Item6, currentState.Item7),
                input2 => (currentState.Item1, currentState.Item2, machine2.Transition(currentState.Item3, input2), currentState.Item4, currentState.Item5, currentState.Item6, currentState.Item7),
                input3 => (currentState.Item1, currentState.Item2, currentState.Item3, machine3.Transition(currentState.Item4, input3), currentState.Item5, currentState.Item6, currentState.Item7),
                input4 => (currentState.Item1, currentState.Item2, currentState.Item3, currentState.Item4, machine4.Transition(currentState.Item5, input4), currentState.Item6, currentState.Item7),
                input5 => (currentState.Item1, currentState.Item2, currentState.Item3, currentState.Item4, currentState.Item5, machine5.Transition(currentState.Item6, input5), currentState.Item7),
                input6 => (currentState.Item1, currentState.Item2, currentState.Item3, currentState.Item4, currentState.Item5, currentState.Item6, machine6.Transition(currentState.Item7, input6))
                );
        }
    }

}
