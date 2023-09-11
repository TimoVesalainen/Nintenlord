using Nintenlord.Numerics;
using Nintenlord.StateMachines;
using System;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Words
{
    public sealed class AutomaticWord<TState, TOut> : IWord<TOut>
    {
        readonly IStateMachine<TState, BigInteger> stateMachine;
        readonly BigInteger baseValue;
        readonly Func<TState, TOut> converter;

        public AutomaticWord(IStateMachine<TState, BigInteger> stateMachine, BigInteger baseValue, Func<TState, TOut> converter)
        {
            this.stateMachine = stateMachine;
            this.baseValue = baseValue;
            this.converter = converter;
        }

        public TOut this[BigInteger index] => GetCharacter(index);

        public BigInteger? Length => null;

        private TOut GetCharacter(BigInteger index)
        {
            var endState = stateMachine.RunUntilFinalState(index.BaseNRepresentation(baseValue)).Last();

            return converter(endState);
        }
    }
}
