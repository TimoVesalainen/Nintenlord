using System.Reactive;

namespace Nintenlord.StateMachines
{
    public sealed class SingleStateMachine<T> : IStateMachine<Unit, T>
    {
        public Unit StartState => Unit.Default;

        public bool IsFinalState(Unit state)
        {
            return false;
        }

        public Unit Transition(Unit currentState, T input)
        {
            return currentState;
        }
    }
}
