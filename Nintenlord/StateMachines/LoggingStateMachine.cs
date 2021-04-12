using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.StateMachines
{
    public sealed class LoggingStateMachine<TState, TInput> : IStateMachine<TState, TInput>
    {
        readonly IStateMachine<TState, TInput> stateMachine;
        readonly Action<TState, bool> logState;
        readonly Action<TInput> logInput;

        public LoggingStateMachine(IStateMachine<TState, TInput> stateMachine, Action<TState, bool> logState, Action<TInput> logInput)
        {
            this.stateMachine = stateMachine;
            this.logState = logState;
            this.logInput = logInput;
        }

        public TState StartState => stateMachine.StartState;

        public bool IsFinalState(TState state)
        {
            return stateMachine.IsFinalState(state);
        }

        public TState Transition(TState currentState, TInput input)
        {
            logState(currentState, false);
            var next = stateMachine.Transition(currentState, input);
            logInput(input);
            logState(next, true);
            return next;
        }
    }
}
