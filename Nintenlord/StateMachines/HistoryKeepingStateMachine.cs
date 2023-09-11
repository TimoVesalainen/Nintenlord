using Nintenlord.Utility;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public sealed class HistoryKeepingStateMachine<TState, TInput>
        : IStateMachine<TState[], Either<TInput, HistoryKeepingStateMachine<TState, TInput>.GoBack>>
    {
        public static readonly GoBack GoBackInput = GoBack.Only;

        public sealed class GoBack
        {
            public static readonly GoBack Only = new GoBack();

            private GoBack()
            {
            }
        }

        private readonly TState[] startState;
        private readonly IStateMachine<TState, TInput> stateMachine;

        public HistoryKeepingStateMachine(IStateMachine<TState, TInput> stateMachine)
        {
            this.stateMachine = stateMachine;
            startState = new[] { stateMachine.StartState };
        }

        #region IStateMachine<TState[],TInput> Members

        public TState[] StartState => startState;

        public bool IsFinalState(TState[] state)
        {
            return stateMachine.IsFinalState(state[state.Length - 1]);
        }

        public TState[] Transition(TState[] currentState, Either<TInput, GoBack> input)
        {
            TState[] InnerTransition(TInput actualInput)
            {
                if (currentState.Length == 0)
                {
                    return new[] { stateMachine.StartState };
                }
                return currentState.Concat(
                    new[] { stateMachine.Transition(currentState.Last(), actualInput) }).ToArray();
            }

            TState[] GoBack(GoBack _)
            {
                if (currentState.Length == 0)
                {
                    return currentState;
                }
                return currentState.Take(currentState.Length - 1).ToArray();
            }

            return input.Apply(InnerTransition, GoBack);
        }

        #endregion
    }
}
