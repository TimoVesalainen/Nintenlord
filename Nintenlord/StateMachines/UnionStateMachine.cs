using Nintenlord.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.StateMachines
{
    //TODO: Templatify
    public sealed class UnionStateMachine<TState1, TState2, TInput> : IStateMachine<Either<TState1, TState2>, Either<TInput,
        UnionStateMachine<TState1, TState2, TInput>.SwitchTo>>
    {
        private readonly IStateMachine<TState1, TInput> machine1;
        private readonly IStateMachine<TState2, TInput> machine2;

        public UnionStateMachine(IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2,
            Either<TState1, TState2> startState = null)
        {
            this.machine1 = machine1;
            this.machine2 = machine2;
            StartState = startState ?? machine1.StartState;
        }

        public Either<TState1, TState2> StartState { get; }

        public bool IsFinalState(Either<TState1, TState2> state)
        {
            return state.Apply(machine1.IsFinalState, machine2.IsFinalState);
        }

        public Either<TState1, TState2> Transition(Either<TState1, TState2> currentState, Either<TInput, SwitchTo> input)
        {
            return input.Apply(
                inputProper => currentState.Apply<Either<TState1, TState2>>(
                    innerState => machine1.Transition(innerState, inputProper),
                    innerState => machine2.Transition(innerState, inputProper)
                    ),
                switchTo => switchTo == SwitchTo.First
                ? (Either<TState1, TState2>)machine1.StartState
                : machine2.StartState
                );
        }

        public sealed class SwitchTo
        {
            public static readonly SwitchTo First = new();
            public static readonly SwitchTo Second = new();

            private SwitchTo()
            {

            }
        }
    }
}
