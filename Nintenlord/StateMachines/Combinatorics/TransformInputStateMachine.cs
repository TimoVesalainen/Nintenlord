using System;

namespace Nintenlord.StateMachines
{
    public sealed class TransformInputStateMachine<TState, TInputIn, TInputOut> : IStateMachine<TState, TInputOut>
    {
        private readonly IStateMachine<TState, TInputIn> original;
        private readonly Func<TInputOut, TInputIn> inputConverter;

        public TransformInputStateMachine(IStateMachine<TState, TInputIn> original,
            Func<TInputOut, TInputIn> inputConverter)
        {
            this.original = original;
            this.inputConverter = inputConverter;
        }

        public TState StartState => original.StartState;

        public bool IsFinalState(TState state)
        {
            return original.IsFinalState(state);
        }

        public TState Transition(TState currentState, TInputOut input)
        {
            return original.Transition(currentState, inputConverter(input));
        }
    }
}
