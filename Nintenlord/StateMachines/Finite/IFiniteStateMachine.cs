using System.Collections.Generic;

namespace Nintenlord.StateMachines.Finite
{
    public interface IFiniteStateMachine<TState, in TInput> : IStateMachine<TState, TInput>
    {
        IEnumerable<TState> States { get; }
    }
}
