using System;

namespace Nintenlord.StateMachines
{
    public sealed class TransformStateMachine<TStateIn, TStateOut, TInput> : IStateMachine<TStateOut, TInput>
    {
        private readonly IStateMachine<TStateIn, TInput> original;
        private readonly Func<TStateIn, TStateOut> stateConverter1;
        private readonly Func<TStateOut, TStateIn> stateConverter2;

        public TransformStateMachine(IStateMachine<TStateIn, TInput> original,
            Func<TStateIn, TStateOut> stateConverter1,
            Func<TStateOut, TStateIn> stateConverter2)
        {
            this.original = original;
            this.stateConverter1 = stateConverter1;
            this.stateConverter2 = stateConverter2;
        }

        public TStateOut StartState => stateConverter1(original.StartState);

        public bool IsFinalState(TStateOut state)
        {
            return original.IsFinalState(stateConverter2(state));
        }

        public TStateOut Transition(TStateOut currentState, TInput input)
        {
            return stateConverter1(original.Transition(stateConverter2(currentState), input));
        }
    }
}
