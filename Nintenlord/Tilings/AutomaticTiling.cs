using Nintenlord.StateMachines;
using Nintenlord.Utility.Primitives;
using System;
using System.Linq;

namespace Nintenlord.Tilings
{
    public sealed class AutomaticTiling<TState, TOutput> : ITiling<TOutput>
    {
        readonly IStateMachine<TState, (int, int)> stateMachine;
        readonly int baseValue;
        readonly Func<TState, TOutput> converter;

        public TOutput this[int x, int y] => GetTile(x, y);

        public AutomaticTiling(IStateMachine<TState, (int, int)> stateMachine, int baseValue, Func<TState, TOutput> converter)
        {
            this.stateMachine = stateMachine;
            this.baseValue = baseValue;
            this.converter = converter;
        }

        private TOutput GetTile(int x, int y)
        {
            var endState = stateMachine.RunUntilFinalState(
                x.BaseNRepresentation(baseValue).Zip(y.BaseNRepresentation(baseValue), (xn,yn) => (xn, yn))
                ).Last();

            return converter(endState);
        }
    }

}
