using Nintenlord.Graph.Colouring;
using Nintenlord.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nintenlord.Collections;
using Nintenlord.Collections.EqualityComparer;

namespace Nintenlord.StateMachines.Finite
{
    public static class FiniteStateMachineHelpers
    {
        public static List<HashSet<TState>> FindEquivalentStates<TState, TInput>(this IFiniteStateMachine<TState, TInput> machine, IEnumerable<TInput> inputs)
        {
            var graph = new StateMachineGraphEdgeColouring<TState, TInput>(machine, inputs);

            var reachableStates = graph.BreadthFirstTraversal(machine.StartState).ToList();

            var grouping = reachableStates.GroupBy(machine.IsFinalState).Select(x => x.ToHashSet()).ToList();

            List<HashSet<TState>> ToIterate(List<HashSet<TState>> grouping)
            {
                return inputs.Aggregate(grouping, (prev, input) =>
                {
                    return reachableStates.GroupBy(state =>
                    {
                        var next = machine.Transition(state, input);
                        return prev.First(group => group.Contains(next));
                    }).SelectMany(groupingTrans =>
                    {
                        return prev.Select(group2 =>
                        {
                            var t = group2.ToHashSet();
                            t.IntersectWith(groupingTrans);
                            return t;
                        });
                    }).Where(x => x.Count > 0).ToList();
                });
            }

            var t = EnumerableExtensions.Iterate(ToIterate, grouping).UntilNotDistinct(
                ByLengthComparer<HashSet<TState>>.Instance as IEqualityComparer<List<HashSet<TState>>>
                ).ToList();

            return EnumerableExtensions.Iterate(ToIterate, grouping).UntilNotDistinct(
                ByLengthComparer<HashSet<TState>>.Instance as IEqualityComparer<List<HashSet<TState>>>
                ).Last();
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
    }
}
