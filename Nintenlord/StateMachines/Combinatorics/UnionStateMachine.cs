using Nintenlord.Utility;
using System;

namespace Nintenlord.StateMachines
{
    public sealed class UnionStateMachine<TState0, TState1, TInput> :
    IStateMachine<Either<TState0, TState1>, Either<TInput, UnionStateMachine<TState0, TState1, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;

        public UnionStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1,
            Either<TState0, TState1> startState = null)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            StartState = startState ?? machine0.StartState;
        }

        public Either<TState0, TState1> StartState { get; }

        public bool IsFinalState(Either<TState0, TState1> state)
        {
            return state.Apply(machine0.IsFinalState, machine1.IsFinalState);
        }

        public Either<TState0, TState1> Transition(Either<TState0, TState1> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState0, TState1>>(innerState => machine0.Transition(innerState, inputProper), innerState => machine1.Transition(innerState, inputProper)),
                switchTo => switchTo.Value switch
                    {
                        Enum2.Item0 => (Either<TState0, TState1>)machine0.StartState,
                        Enum2.Item1 => (Either<TState0, TState1>)machine1.StartState,
                        _ => throw new InvalidProgramException(),
                    });
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo SwitchTo0 = new(Enum2.Item0);
            public static readonly SwitchTo SwitchTo1 = new(Enum2.Item1);

            public Enum2 Value { get; }

            private SwitchTo(Enum2 value)
            {
                Value = value;
            }
        }
    }
    public sealed class UnionStateMachine<TState0, TState1, TState2, TInput> :
    IStateMachine<Either<TState0, TState1, TState2>, Either<TInput, UnionStateMachine<TState0, TState1, TState2, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;

        public UnionStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2,
            Either<TState0, TState1, TState2> startState = null)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            StartState = startState ?? machine0.StartState;
        }

        public Either<TState0, TState1, TState2> StartState { get; }

        public bool IsFinalState(Either<TState0, TState1, TState2> state)
        {
            return state.Apply(machine0.IsFinalState, machine1.IsFinalState, machine2.IsFinalState);
        }

        public Either<TState0, TState1, TState2> Transition(Either<TState0, TState1, TState2> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState0, TState1, TState2>>(innerState => machine0.Transition(innerState, inputProper), innerState => machine1.Transition(innerState, inputProper), innerState => machine2.Transition(innerState, inputProper)),
                switchTo => switchTo.Value switch
                    {
                        Enum3.Item0 => (Either<TState0, TState1, TState2>)machine0.StartState,
                        Enum3.Item1 => (Either<TState0, TState1, TState2>)machine1.StartState,
                        Enum3.Item2 => (Either<TState0, TState1, TState2>)machine2.StartState,
                        _ => throw new InvalidProgramException(),
                    });
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo SwitchTo0 = new(Enum3.Item0);
            public static readonly SwitchTo SwitchTo1 = new(Enum3.Item1);
            public static readonly SwitchTo SwitchTo2 = new(Enum3.Item2);

            public Enum3 Value { get; }

            private SwitchTo(Enum3 value)
            {
                Value = value;
            }
        }
    }
    public sealed class UnionStateMachine<TState0, TState1, TState2, TState3, TInput> :
    IStateMachine<Either<TState0, TState1, TState2, TState3>, Either<TInput, UnionStateMachine<TState0, TState1, TState2, TState3, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;

        public UnionStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3,
            Either<TState0, TState1, TState2, TState3> startState = null)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            StartState = startState ?? machine0.StartState;
        }

        public Either<TState0, TState1, TState2, TState3> StartState { get; }

        public bool IsFinalState(Either<TState0, TState1, TState2, TState3> state)
        {
            return state.Apply(machine0.IsFinalState, machine1.IsFinalState, machine2.IsFinalState, machine3.IsFinalState);
        }

        public Either<TState0, TState1, TState2, TState3> Transition(Either<TState0, TState1, TState2, TState3> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState0, TState1, TState2, TState3>>(innerState => machine0.Transition(innerState, inputProper), innerState => machine1.Transition(innerState, inputProper), innerState => machine2.Transition(innerState, inputProper), innerState => machine3.Transition(innerState, inputProper)),
                switchTo => switchTo.Value switch
                    {
                        Enum4.Item0 => (Either<TState0, TState1, TState2, TState3>)machine0.StartState,
                        Enum4.Item1 => (Either<TState0, TState1, TState2, TState3>)machine1.StartState,
                        Enum4.Item2 => (Either<TState0, TState1, TState2, TState3>)machine2.StartState,
                        Enum4.Item3 => (Either<TState0, TState1, TState2, TState3>)machine3.StartState,
                        _ => throw new InvalidProgramException(),
                    });
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo SwitchTo0 = new(Enum4.Item0);
            public static readonly SwitchTo SwitchTo1 = new(Enum4.Item1);
            public static readonly SwitchTo SwitchTo2 = new(Enum4.Item2);
            public static readonly SwitchTo SwitchTo3 = new(Enum4.Item3);

            public Enum4 Value { get; }

            private SwitchTo(Enum4 value)
            {
                Value = value;
            }
        }
    }
    public sealed class UnionStateMachine<TState0, TState1, TState2, TState3, TState4, TInput> :
    IStateMachine<Either<TState0, TState1, TState2, TState3, TState4>, Either<TInput, UnionStateMachine<TState0, TState1, TState2, TState3, TState4, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;
        private readonly IStateMachine<TState4, TInput> machine4;

        public UnionStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3, IStateMachine<TState4, TInput> machine4,
            Either<TState0, TState1, TState2, TState3, TState4> startState = null)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            this.machine4 = machine4;
            StartState = startState ?? machine0.StartState;
        }

        public Either<TState0, TState1, TState2, TState3, TState4> StartState { get; }

        public bool IsFinalState(Either<TState0, TState1, TState2, TState3, TState4> state)
        {
            return state.Apply(machine0.IsFinalState, machine1.IsFinalState, machine2.IsFinalState, machine3.IsFinalState, machine4.IsFinalState);
        }

        public Either<TState0, TState1, TState2, TState3, TState4> Transition(Either<TState0, TState1, TState2, TState3, TState4> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState0, TState1, TState2, TState3, TState4>>(innerState => machine0.Transition(innerState, inputProper), innerState => machine1.Transition(innerState, inputProper), innerState => machine2.Transition(innerState, inputProper), innerState => machine3.Transition(innerState, inputProper), innerState => machine4.Transition(innerState, inputProper)),
                switchTo => switchTo.Value switch
                    {
                        Enum5.Item0 => (Either<TState0, TState1, TState2, TState3, TState4>)machine0.StartState,
                        Enum5.Item1 => (Either<TState0, TState1, TState2, TState3, TState4>)machine1.StartState,
                        Enum5.Item2 => (Either<TState0, TState1, TState2, TState3, TState4>)machine2.StartState,
                        Enum5.Item3 => (Either<TState0, TState1, TState2, TState3, TState4>)machine3.StartState,
                        Enum5.Item4 => (Either<TState0, TState1, TState2, TState3, TState4>)machine4.StartState,
                        _ => throw new InvalidProgramException(),
                    });
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo SwitchTo0 = new(Enum5.Item0);
            public static readonly SwitchTo SwitchTo1 = new(Enum5.Item1);
            public static readonly SwitchTo SwitchTo2 = new(Enum5.Item2);
            public static readonly SwitchTo SwitchTo3 = new(Enum5.Item3);
            public static readonly SwitchTo SwitchTo4 = new(Enum5.Item4);

            public Enum5 Value { get; }

            private SwitchTo(Enum5 value)
            {
                Value = value;
            }
        }
    }
    public sealed class UnionStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TInput> :
    IStateMachine<Either<TState0, TState1, TState2, TState3, TState4, TState5>, Either<TInput, UnionStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;
        private readonly IStateMachine<TState4, TInput> machine4;
        private readonly IStateMachine<TState5, TInput> machine5;

        public UnionStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3, IStateMachine<TState4, TInput> machine4, IStateMachine<TState5, TInput> machine5,
            Either<TState0, TState1, TState2, TState3, TState4, TState5> startState = null)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            this.machine4 = machine4;
            this.machine5 = machine5;
            StartState = startState ?? machine0.StartState;
        }

        public Either<TState0, TState1, TState2, TState3, TState4, TState5> StartState { get; }

        public bool IsFinalState(Either<TState0, TState1, TState2, TState3, TState4, TState5> state)
        {
            return state.Apply(machine0.IsFinalState, machine1.IsFinalState, machine2.IsFinalState, machine3.IsFinalState, machine4.IsFinalState, machine5.IsFinalState);
        }

        public Either<TState0, TState1, TState2, TState3, TState4, TState5> Transition(Either<TState0, TState1, TState2, TState3, TState4, TState5> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState0, TState1, TState2, TState3, TState4, TState5>>(innerState => machine0.Transition(innerState, inputProper), innerState => machine1.Transition(innerState, inputProper), innerState => machine2.Transition(innerState, inputProper), innerState => machine3.Transition(innerState, inputProper), innerState => machine4.Transition(innerState, inputProper), innerState => machine5.Transition(innerState, inputProper)),
                switchTo => switchTo.Value switch
                    {
                        Enum6.Item0 => (Either<TState0, TState1, TState2, TState3, TState4, TState5>)machine0.StartState,
                        Enum6.Item1 => (Either<TState0, TState1, TState2, TState3, TState4, TState5>)machine1.StartState,
                        Enum6.Item2 => (Either<TState0, TState1, TState2, TState3, TState4, TState5>)machine2.StartState,
                        Enum6.Item3 => (Either<TState0, TState1, TState2, TState3, TState4, TState5>)machine3.StartState,
                        Enum6.Item4 => (Either<TState0, TState1, TState2, TState3, TState4, TState5>)machine4.StartState,
                        Enum6.Item5 => (Either<TState0, TState1, TState2, TState3, TState4, TState5>)machine5.StartState,
                        _ => throw new InvalidProgramException(),
                    });
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo SwitchTo0 = new(Enum6.Item0);
            public static readonly SwitchTo SwitchTo1 = new(Enum6.Item1);
            public static readonly SwitchTo SwitchTo2 = new(Enum6.Item2);
            public static readonly SwitchTo SwitchTo3 = new(Enum6.Item3);
            public static readonly SwitchTo SwitchTo4 = new(Enum6.Item4);
            public static readonly SwitchTo SwitchTo5 = new(Enum6.Item5);

            public Enum6 Value { get; }

            private SwitchTo(Enum6 value)
            {
                Value = value;
            }
        }
    }
    public sealed class UnionStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TState6, TInput> :
    IStateMachine<Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>, Either<TInput, UnionStateMachine<TState0, TState1, TState2, TState3, TState4, TState5, TState6, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState0, TInput> machine0;
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;
        private readonly IStateMachine<TState3, TInput> machine3;
        private readonly IStateMachine<TState4, TInput> machine4;
        private readonly IStateMachine<TState5, TInput> machine5;
        private readonly IStateMachine<TState6, TInput> machine6;

        public UnionStateMachine(IStateMachine<TState0, TInput> machine0, IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, IStateMachine<TState3, TInput> machine3, IStateMachine<TState4, TInput> machine4, IStateMachine<TState5, TInput> machine5, IStateMachine<TState6, TInput> machine6,
            Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6> startState = null)
        {
            this.machine0 = machine0;
            this.machine1 = machine1;
            this.machine2 = machine2;
            this.machine3 = machine3;
            this.machine4 = machine4;
            this.machine5 = machine5;
            this.machine6 = machine6;
            StartState = startState ?? machine0.StartState;
        }

        public Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6> StartState { get; }

        public bool IsFinalState(Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6> state)
        {
            return state.Apply(machine0.IsFinalState, machine1.IsFinalState, machine2.IsFinalState, machine3.IsFinalState, machine4.IsFinalState, machine5.IsFinalState, machine6.IsFinalState);
        }

        public Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6> Transition(Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>>(innerState => machine0.Transition(innerState, inputProper), innerState => machine1.Transition(innerState, inputProper), innerState => machine2.Transition(innerState, inputProper), innerState => machine3.Transition(innerState, inputProper), innerState => machine4.Transition(innerState, inputProper), innerState => machine5.Transition(innerState, inputProper), innerState => machine6.Transition(innerState, inputProper)),
                switchTo => switchTo.Value switch
                    {
                        Enum7.Item0 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine0.StartState,
                        Enum7.Item1 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine1.StartState,
                        Enum7.Item2 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine2.StartState,
                        Enum7.Item3 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine3.StartState,
                        Enum7.Item4 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine4.StartState,
                        Enum7.Item5 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine5.StartState,
                        Enum7.Item6 => (Either<TState0, TState1, TState2, TState3, TState4, TState5, TState6>)machine6.StartState,
                        _ => throw new InvalidProgramException(),
                    });
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo SwitchTo0 = new(Enum7.Item0);
            public static readonly SwitchTo SwitchTo1 = new(Enum7.Item1);
            public static readonly SwitchTo SwitchTo2 = new(Enum7.Item2);
            public static readonly SwitchTo SwitchTo3 = new(Enum7.Item3);
            public static readonly SwitchTo SwitchTo4 = new(Enum7.Item4);
            public static readonly SwitchTo SwitchTo5 = new(Enum7.Item5);
            public static readonly SwitchTo SwitchTo6 = new(Enum7.Item6);

            public Enum7 Value { get; }

            private SwitchTo(Enum7 value)
            {
                Value = value;
            }
        }
    }
}
