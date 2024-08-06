using Nintenlord.Utility;
using System;

namespace Nintenlord.StateMachines
{
    public sealed class AddCommandsStateMachine<TState, TInput1, TInput2> : IStateMachine<TState, Either<TInput1, TInput2>>
    {
        readonly IStateMachine<TState, TInput1> original;
        readonly Func<TInput2, TState, TState> newTransitions;

        public AddCommandsStateMachine(IStateMachine<TState, TInput1> original, Func<TInput2, TState, TState> newTransitions)
        {
            this.original = original;
            this.newTransitions = newTransitions;
        }

        public TState StartState => original.StartState;

        public bool IsFinalState(TState state)
        {
            return original.IsFinalState(state);
        }

        public TState Transition(TState currentState, Either<TInput1, TInput2> input)
        {
            return input.Apply(
                originalInput => original.Transition(currentState, originalInput),
                newInput => newTransitions(newInput, currentState)
                );

        }
    }
}
