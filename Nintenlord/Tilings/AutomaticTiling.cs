using Nintenlord.Numerics;
using Nintenlord.StateMachines;
using System;
using System.Linq;

namespace Nintenlord.Tilings
{
    public sealed class AutomaticTiling<TState, TOutput> : ITiling<TOutput>
    {
        readonly IStateMachine<TState, int> positiveXstateMachine;
        readonly IStateMachine<TState, int> negativeXstateMachine;
        readonly IStateMachine<TState, int> positiveYstateMachine;
        readonly IStateMachine<TState, int> negativeYstateMachine;
        readonly int baseValue;
        readonly Func<TState, TState, TOutput> converter;

        public TOutput this[int x, int y] => GetTile(x, y);

        public AutomaticTiling(
            IStateMachine<TState, int> positiveXstateMachine,
            IStateMachine<TState, int> negativeXstateMachine,
            IStateMachine<TState, int> positiveYstateMachine,
            IStateMachine<TState, int> negativeYstateMachine,
            int baseValue,
            Func<TState, TState, TOutput> converter)
        {

            this.positiveXstateMachine = positiveXstateMachine;
            this.negativeXstateMachine = negativeXstateMachine;
            this.positiveYstateMachine = positiveYstateMachine;
            this.negativeYstateMachine = negativeYstateMachine;
            this.baseValue = baseValue;
            this.converter = converter;
        }

        private TOutput GetTile(int x, int y)
        {
            IStateMachine<TState, int> horizontalMachine;
            int numberHorizontal;
            if (x >= 0)
            {
                horizontalMachine = positiveXstateMachine;
                numberHorizontal = x;
            }
            else
            {
                horizontalMachine = negativeXstateMachine;
                numberHorizontal = -x;
            }
            TState horizontal = horizontalMachine.RunUntilFinalState(numberHorizontal.BaseNRepresentation(baseValue)).Last();

            IStateMachine<TState, int> verticalMachine;
            int numberVertical;
            if (y >= 0)
            {
                verticalMachine = positiveYstateMachine;
                numberVertical = y;
            }
            else
            {
                verticalMachine = negativeYstateMachine;
                numberVertical = -y;
            }
            TState vertical = verticalMachine.RunUntilFinalState(numberVertical.BaseNRepresentation(baseValue)).Last();

            return converter(horizontal, vertical);
        }
    }

}
