using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Graph;
using Nintenlord.Graph.Colouring;
using Nintenlord.Collections;
using Nintenlord.Collections.EqualityComparer;
using Nintenlord.Distributions;
using Nintenlord.Distributions.Discrete;
using Nintenlord.Collections.Comparers;

namespace Nintenlord.StateMachines.Finite
{
    public static class FiniteStateMachineHelpers
    {
        public static List<HashSet<TState>> FindEquivalentStates<TState, TInput>(this IFiniteStateMachine<TState, TInput> machine, IEnumerable<TInput> inputs)
        {
            var graph = new StateMachineGraphEdgeColouring<TState, TInput>(machine, inputs);

            var reachableStates = graph.BreadthFirstTraversal(machine.StartState).ToList();

            var partition = new Partition<TState>(reachableStates);

            if (!partition.Split(Comparer<bool>.Default.Select<bool, TState>(machine.IsFinalState)))
            {
                return reachableStates.Select(x => new HashSet<TState>{ x }).ToList();
            }

            var partitionDict = new Dictionary<TState, int>(partition.Count);

            IComparer<TState> CreateComparer(TInput input)
            {
                foreach (var (items, index) in partition.GetPartitions()
                    .Select((partitionItems, index) => (partitionItems, index)))
                {
                    foreach (var item in items)
                    {
                        partitionDict[item] = index;
                    }
                }
                return Comparer<int>.Default.Select<int, TState>(state => partitionDict[machine.Transition(state, input)]);
            }

            var continueSplitting = true;
            while (continueSplitting)
            {
                foreach (var input in inputs)
                {
                    if (!partition.Split(CreateComparer(input)))
                    {
                        continueSplitting = false;
                        break;
                    }

                }
            }
            return partition.GetPartitions().Select(partition => partition.ToHashSet()).ToList();
        }

        /// <summary>
        /// Constructs state machine such that
        /// foreach input list <code>w</code> made of <paramref name="inputs"/>
        /// <paramref name="machine"/>.IsAccepted(w) == returnValue.IsAccepted(w)
        /// </summary>
        public static IFiniteStateMachine<int, TInput> FindMinimumStateMachine<TState, TInput>(this IFiniteStateMachine<TState, TInput> machine, IEnumerable<TInput> inputs)
        {
            var groupings = machine.FindEquivalentStates(inputs);

            int FindIndex(TState state)
            {
                var (_, stateNew) = groupings.Select((set, index) => (set.Contains(state), index)).First(t => t.Item1);
                return stateNew;
            }

            var startState = FindIndex(machine.StartState);
            var finalStates = groupings.Select((set) => (set.Contains(machine.IsFinalState))).ToArray();

            var transitions = from state in Enumerable.Range(0, groupings.Count)
                              from input in inputs
                              let toTransition = FindIndex(machine.Transition(groupings[state].First(), input))
                              select (state, input, toTransition);

            var transitionsDictionary = transitions.ToDictionary(t => (t.state, t.input), t => t.toTransition);

            return new DictionaryStateMachine<int, TInput>(transitionsDictionary, x => finalStates[x], startState);
        }

        public static IDistribution<IFiniteStateMachine<TState, TInput>> Create<TState, TInput>(
            IEnumerable<TState> states,
            IEnumerable<TInput> inputs,
            int transitionCount
            )
        {
            IFiniteStateMachine<TState, TInput> Build(
                TState startState,
                bool[] finalsStates,
                (TState start, TInput letter, TState end)[] transitions)
            {
                var randomMachineBuilder = new DictionaryStateMachine<TState, TInput>.Builder();

                foreach (var (state, isFinal) in states.Zip(finalsStates, (a,b) => (a,b)))
                {
                    randomMachineBuilder.AddState(state);
                    randomMachineBuilder.SetIsFinalState(state, isFinal);
                }
                randomMachineBuilder.SetStartState(startState);

                foreach (var (start, letter, end) in transitions)
                {
                    randomMachineBuilder.AddTransition(start, letter, end);
                }

                return randomMachineBuilder.Build();
            }

            var randomState = states.RandomItem();
            var randomInput = inputs.RandomItem();
            var isFinal = BernuelliDistribution.Create(1, 1).Select(x => x == 0);
            var transition = from start in randomState
                             from end in randomState
                             from letter in randomInput
                             select (start, letter, end);
            var finalStates = isFinal.ArrayDistribution(states.Count());
            var transitions = transition.ArrayDistribution(transitionCount);

            return from startState in randomState
                   from isFinalState in finalStates
                   from transitionsToUse in transitions
                   select Build(startState, isFinalState, transitionsToUse);
        }
    }
}
