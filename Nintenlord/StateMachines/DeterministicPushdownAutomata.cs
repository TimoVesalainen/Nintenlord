using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public sealed class DeterministicPushdownAutomata<TState, TInput, TStack> : IStateMachine<(TState, ImmutableStack<TStack>), TInput>
    {
        private readonly TState startState;
        private readonly TStack startStackState;
        private readonly Func<TState, TStack, TInput, (TState, IEnumerable<TStack>)> transition;

        public DeterministicPushdownAutomata(TState startState, TStack startStackState, Func<TState, TStack, TInput, (TState, IEnumerable<TStack>)> transition)
        {
            this.startState = startState;
            this.startStackState = startStackState;
            this.transition = transition ?? throw new ArgumentNullException(nameof(transition));
        }

        public (TState, ImmutableStack<TStack>) StartState => (startState, ImmutableStack.Create(startStackState));

        public bool IsFinalState((TState, ImmutableStack<TStack>) state)
        {
            return state.Item2.IsEmpty;
        }

        public (TState, ImmutableStack<TStack>) Transition((TState, ImmutableStack<TStack>) currentState, TInput input)
        {
            if (currentState.Item2.IsEmpty)
            {
                return currentState;
            }

            var (newState, newStackSymbols) = transition(currentState.Item1, currentState.Item2.PeekRef(), input);

            var newStack = newStackSymbols.Aggregate(currentState.Item2.Pop(), (stack, symbol) => stack.Push(symbol));

            return (newState, newStack);
        }
    }
}
