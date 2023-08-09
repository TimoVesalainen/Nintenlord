using System.Collections.Generic;

namespace Nintenlord.StateMachines
{
    public interface IFiniteStateMachine<TState, in TInput> : IStateMachine<TState, TInput>
    {
        IEnumerable<TState> States { get; }
    }
}
