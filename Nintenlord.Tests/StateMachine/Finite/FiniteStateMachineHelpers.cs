using Nintenlord.StateMachines.Finite;
using NUnit.Framework;

namespace Nintenlord.Tests.StateMachine.Finite
{
    internal class FiniteStateMachineHelpers
    {
        [Test]
        public static void TestEquivalenceFindingEvenNumberOfAs()
        {
            var evenAmountOfCharacterAs = new DictionaryStateMachine<int, char>.Builder()
                .AddState(0)
                .AddState(1)
                .AddState(2)
                .AddState(3)
                .AddTransition(0, 'a', 1)
                .AddTransition(1, 'a', 2)
                .AddTransition(2, 'a', 3)
                .AddTransition(3, 'a', 0)
                .SetStartState(0)
                .SetIsFinalState(0, true)
                .SetIsFinalState(2, true)
                .Build();

            var equivalent = evenAmountOfCharacterAs.FindEquivalentStates("a");

            Assert.AreEqual(equivalent, new[] { new[] { 0, 2 }, new[] { 1, 3 } });
        }
    }
}
