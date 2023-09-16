namespace Nintenlord.StateMachines
{
    public sealed class ParallelStateMachine<TState0, TState1, TInput> : IStateMachine<(TState0, TState1), TInput>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;

        public ParallelStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
        }

        public (TState0, TState1) StartState => (machine0.StartState, machine1.StartState);

        public bool IsFinalState((TState0, TState1) state)
        {
            return machine0.IsFinalState(state.Item1) && machine1.IsFinalState(state.Item2);
        }

        public (TState0, TState1) Transition((TState0, TState1) currentState, TInput input)
        {
            return (machine0.Transition(currentState.Item1, input), machine1.Transition(currentState.Item2, input));
        }
    }
    public sealed class ParallelStateMachine<TState0, TState1, TState2, TInput> : IStateMachine<(TState0, TState1, TState2), TInput>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;

        public ParallelStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2)
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

        public (TState0, TState1, TState2) Transition((TState0, TState1, TState2) currentState, TInput input)
        {
            return (machine0.Transition(currentState.Item1, input), machine1.Transition(currentState.Item2, input), machine2.Transition(currentState.Item3, input));
        }
    }
    public sealed class ParallelStateMachine<TState0, TState1, TState2, TState3, TInput> : IStateMachine<(TState0, TState1, TState2, TState3), TInput>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;

        public ParallelStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3)
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

        public (TState0, TState1, TState2, TState3) Transition((TState0, TState1, TState2, TState3) currentState, TInput input)
        {
            return (machine0.Transition(currentState.Item1, input), machine1.Transition(currentState.Item2, input), machine2.Transition(currentState.Item3, input), machine3.Transition(currentState.Item4, input));
        }
    }
    public sealed class ParallelStateMachine<TState0, TState1, TState2, TState3, TState4, TInput> : IStateMachine<(TState0, TState1, TState2, TState3, TState4), TInput>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;
        private readonly IStateMachine<TState4, TInput> machine4;

        public ParallelStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3, IStateMachine<TState4, TInput> machine4)
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

        public (TState0, TState1, TState2, TState3, TState4) Transition((TState0, TState1, TState2, TState3, TState4) currentState, TInput input)
        {
            return (machine0.Transition(currentState.Item1, input), machine1.Transition(currentState.Item2, input), machine2.Transition(currentState.Item3, input), machine3.Transition(currentState.Item4, input), machine4.Transition(currentState.Item5, input));
        }
    }
    public sealed class ParallelStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TInput> : IStateMachine<(TState0, TState1, TState2, TState3, TState4, TState5), TInput>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;
        private readonly IStateMachine<TState4, TInput> machine4;
        private readonly IStateMachine<TState5, TInput> machine5;

        public ParallelStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3, IStateMachine<TState4, TInput> machine4, IStateMachine<TState5, TInput> machine5)
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

        public (TState0, TState1, TState2, TState3, TState4, TState5) Transition((TState0, TState1, TState2, TState3, TState4, TState5) currentState, TInput input)
        {
            return (machine0.Transition(currentState.Item1, input), machine1.Transition(currentState.Item2, input), machine2.Transition(currentState.Item3, input), machine3.Transition(currentState.Item4, input), machine4.Transition(currentState.Item5, input), machine5.Transition(currentState.Item6, input));
        }
    }
    public sealed class ParallelStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TState6, TInput> : IStateMachine<(TState0, TState1, TState2, TState3, TState4, TState5, TState6), TInput>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;
        private readonly IStateMachine<TState4, TInput> machine4;
        private readonly IStateMachine<TState5, TInput> machine5;
        private readonly IStateMachine<TState6, TInput> machine6;

        public ParallelStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3, IStateMachine<TState4, TInput> machine4, IStateMachine<TState5, TInput> machine5, IStateMachine<TState6, TInput> machine6)
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

        public (TState0, TState1, TState2, TState3, TState4, TState5, TState6) Transition((TState0, TState1, TState2, TState3, TState4, TState5, TState6) currentState, TInput input)
        {
            return (machine0.Transition(currentState.Item1, input), machine1.Transition(currentState.Item2, input), machine2.Transition(currentState.Item3, input), machine3.Transition(currentState.Item4, input), machine4.Transition(currentState.Item5, input), machine5.Transition(currentState.Item6, input), machine6.Transition(currentState.Item7, input));
        }
    }
}
