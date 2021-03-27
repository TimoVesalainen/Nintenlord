// -----------------------------------------------------------------------
// <copyright file="PushdownAutomata.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Grammars
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeterministicPushdownAutomata<TStackSymbol, TState, TLetter>
    {
        private readonly Stack<TStackSymbol> stack;
        private readonly Dictionary<Tuple<TState, TLetter, TStackSymbol>, Tuple<TState, TStackSymbol[]>> transitions;
        private readonly Dictionary<Tuple<TState, TStackSymbol>, Tuple<TState, TStackSymbol[]>> epsilonTransitions;
        private readonly TStackSymbol stackStartSymbol;
        private readonly TState startingState;

        public DeterministicPushdownAutomata()
        {
            epsilonTransitions = new Dictionary<Tuple<TState, TStackSymbol>, Tuple<TState, TStackSymbol[]>>();
            transitions = new Dictionary<Tuple<TState, TLetter, TStackSymbol>, Tuple<TState, TStackSymbol[]>>();
            stack = new Stack<TStackSymbol>();
        }

        public bool IsAccepted(TLetter[] word)
        {
            int index = 0;
            stack.Clear();
            stack.Push(stackStartSymbol);
            TState currentState = startingState;

            while (stack.Count > 0)
            {
                TStackSymbol top = stack.Pop();

                if (epsilonTransitions.TryGetValue(Tuple.Create(currentState, top), out Tuple<TState, TStackSymbol[]> transition))
                {

                }
                else if (index < word.Length &&
                    transitions.TryGetValue(Tuple.Create(currentState, word[index], top), out transition))
                {
                    index++;
                }
                else
                {
                    return false;
                }

                currentState = transition.Item1;
                foreach (var stackSymbol in transition.Item2)
                {
                    stack.Push(stackSymbol);
                }
            }

            return index == word.Length;
        }
    }
}
