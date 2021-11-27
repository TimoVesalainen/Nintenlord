using System.Text;

namespace Nintenlord.StateMachines
{
    //TODO: Templatify
    public sealed class TwoStepStateMachine<TState, TInput> : IStateMachine<TState, (TInput, TInput)>
    {
        readonly IStateMachine<TState, TInput> original;

        public TwoStepStateMachine(IStateMachine<TState, TInput> original)
        {
            this.original = original;
        }

        public TState StartState => original.StartState;

        public bool IsFinalState(TState state)
        {
            return original.IsFinalState(state);
        }

        public TState Transition(TState currentState, (TInput, TInput) input)
        {
            return original.Transition(
                original.Transition(currentState, input.Item1), input.Item2);
        }
    }
}
