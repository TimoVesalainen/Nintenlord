﻿using Nintenlord.StateMachines.Finite;
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

        [Test]
        public static void TestEquivalenceFindingB()
        {
            var containsB = new DictionaryStateMachine<int, char>.Builder()
                .AddState(0)
                .AddState(1)
                .AddState(2)
                .AddState(3)
                .AddState(4)
                .AddState(5)
                .AddState(6)
                .AddState(7)
                .AddTransition(0, 'a', 1)
                .AddTransition(1, 'a', 2)
                .AddTransition(2, 'a', 3)
                .AddTransition(3, 'a', 0)
                .AddTransition(0, 'b', 4)
                .AddTransition(1, 'b', 5)
                .AddTransition(2, 'b', 6)
                .AddTransition(3, 'b', 7)
                .AddTransition(4, 'a', 5)
                .AddTransition(5, 'a', 6)
                .AddTransition(6, 'a', 7)
                .AddTransition(7, 'a', 4)
                .SetStartState(0)
                .SetIsFinalState(4, true)
                .SetIsFinalState(5, true)
                .SetIsFinalState(6, true)
                .SetIsFinalState(7, true)
                .Build();

            var equivalent = containsB.FindEquivalentStates("ab");

            Assert.AreEqual(equivalent, new[] {
                new[] { 0, 1, 2, 3 },
                new[] { 4, 5, 6, 7 }
            });
        }
    }
}
