using System;

namespace Nintenlord.StateMachines
{
    public sealed class StateChangedEventArgs<TState, TInput> : EventArgs
    {
        public TState OldState { get; }
        public TState NewState { get; }
        public TInput Input { get; }

        public StateChangedEventArgs(TState oldState, TState newState, TInput input)
        {
            OldState = oldState;
            NewState = newState;
            Input = input;
        }
    }

    public class StatefulObject<TState, TInput> : ICloneable
    {
        readonly IStateMachine<TState, TInput> stateMachine;
        TState state;

        public TState CurrentState => state;

        public event EventHandler<StateChangedEventArgs<TState, TInput>> Changed;

        public StatefulObject(IStateMachine<TState, TInput> stateMachine)
        {
            this.stateMachine = stateMachine;
            state = stateMachine.StartState;
        }

        public void Update(TInput input)
        {
            var oldState = state;
            state = stateMachine.Transition(state, input);
            OnChanged(new StateChangedEventArgs<TState, TInput>(oldState, state, input));
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

        private void OnChanged(StateChangedEventArgs<TState, TInput> eventArgs)
        {
            Changed?.Invoke(this, eventArgs);
        }
    }
}
