using Microsoft.Extensions.Logging;
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

    public sealed class LoggingStateMachine2<TState, TInput> : IStateMachine<TState, TInput>
    {
        readonly IStateMachine<TState, TInput> stateMachine;
        private readonly ILogger logger;
        private readonly Func<TInput, TState, TState, string> format;
        private readonly LogLevel level;

        public LoggingStateMachine2(IStateMachine<TState, TInput> stateMachine, ILogger logger,
            Func<TInput, TState, TState, string> format = null, LogLevel level = LogLevel.Information)
        {
            this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.format = format ?? ((i, s1, s2) => string.Format("{0}: {1} -> {2}", i, s1, s2));
            this.level = level;
        }

        public TState StartState => stateMachine.StartState;

        public bool IsFinalState(TState state)
        {
            return stateMachine.IsFinalState(state);
        }

        public TState Transition(TState currentState, TInput input)
        {
            var next = stateMachine.Transition(currentState, input);
            logger.Log(level, format(input, currentState, next));
            return next;
        }
    }
}
