using System;

namespace Nintenlord.StateMachines
{
    public class StatefulObject<TState, TInput> : ICloneable
    {
        readonly IStateMachine<TState, TInput> stateMachine;
        TState state;

        public TState CurrentState => state;

        public StatefulObject(IStateMachine<TState, TInput> stateMachine)
        {
            this.stateMachine = stateMachine;
            state = stateMachine.StartState;
        }

        public void Update(TInput input)
        {
            state = stateMachine.Transition(state, input);
        }

        public bool IsFinished()
        {
            return stateMachine.IsFinalState(state);
        }
        public void Reset()
        {
            state = stateMachine.StartState;
        }

        public StatefulObject<TState, TInput> Clone()
        {
            return new StatefulObject<TState, TInput>(stateMachine)
            {
                state = this.state
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
