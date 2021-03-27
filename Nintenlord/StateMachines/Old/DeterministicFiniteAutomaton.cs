// -----------------------------------------------------------------------
// <copyright file="DeterministicFiniteAutomata.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.StateMachines.Old
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Deterministic finite automaton
    /// </summary>
    public sealed class DeterministicFiniteAutomaton<TState, TAlphabet>
    {
        private readonly Dictionary<Tuple<TState, TAlphabet>, TState> transition;
        private readonly Predicate<TState> finalStatePredicate;
        private readonly TState startState;
        private TState currentState;

        /// <summary>
        /// If the DFA is in final state.
        /// </summary>
        public bool IsInFinalState
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new DFA.
        /// </summary>
        /// <param name="transitions">Needs to be complete aka have all possible transitions</param>
        /// <param name="finalStates">All final states of DFA</param>
        /// <param name="startState">The start state of DFA</param>
        public DeterministicFiniteAutomaton(IEnumerable<Tuple<TState, TAlphabet, TState>> transitions,
            Predicate<TState> finalStates, TState startState)
        {
            transition = new Dictionary<Tuple<TState, TAlphabet>, TState>();
            foreach (var item in transitions)
            {
                transition[Tuple.Create(item.Item1, item.Item2)] = item.Item3;
            }
            finalStatePredicate = finalStates;
            this.startState = startState;

            Reset();
        }

        private DeterministicFiniteAutomaton(DeterministicFiniteAutomaton<TState, TAlphabet> cloneMe)
            : this(cloneMe.transition, cloneMe.finalStatePredicate, cloneMe.startState, cloneMe.currentState)
        {

        }

        private DeterministicFiniteAutomaton(
            Dictionary<Tuple<TState, TAlphabet>, TState> transition,
            Predicate<TState> finalStates,
            TState startState,
            TState currentState)
        {
            finalStatePredicate = finalStates;
            this.startState = startState;
            this.transition = transition;
            this.currentState = currentState;
        }

        /// <summary>
        /// Moves the DFA to the next state.
        /// </summary>
        /// <param name="nextAlphabet">Next input alphabet</param>
        public void Move(TAlphabet nextAlphabet)
        {
            currentState = transition[Tuple.Create(currentState, nextAlphabet)];
            IsInFinalState = finalStatePredicate(currentState);
        }

        /// <summary>
        /// Sets the DFA to starting state.
        /// </summary>
        public void Reset()
        {
            currentState = startState;
            IsInFinalState = finalStatePredicate(currentState);
        }

        /// <summary>
        /// Creates a clone with same current state.
        /// </summary>
        /// <returns></returns>
        public DeterministicFiniteAutomaton<TState, TAlphabet> Clone()
        {
            return new DeterministicFiniteAutomaton<TState, TAlphabet>(this);
        }

        /// <summary>
        /// Creates a clone that uses current state asd it's start state.
        /// </summary>
        /// <returns></returns>
        public DeterministicFiniteAutomaton<TState, TAlphabet> CloneWithCurrentStateAsStart()
        {
            return new DeterministicFiniteAutomaton<TState, TAlphabet>(
                transition,
                finalStatePredicate,
                currentState,
                currentState);
        }
    }
}
