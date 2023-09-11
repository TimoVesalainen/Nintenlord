using Nintenlord.Collections;
using Nintenlord.Collections.EqualityComparer;
using Nintenlord.Grammars.RegularExpression.Tree;
using Nintenlord.Graph;
using Nintenlord.StateMachines;
using Nintenlord.StateMachines.Finite;
using Nintenlord.Trees;
using Nintenlord.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Grammars.RegularExpression
{
    public static class RegExHelpers
    {
        public static bool IsEmpty<TLetter>(this ITree<IRegExExpressionNode<TLetter>> tree)
        {
            return tree.IsEmpty(tree.Root);
        }

        public static bool IsEmpty<TLetter>(this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> startNode)
        {
            bool IsEmptyInner(IRegExExpressionNode<TLetter> exp)
            {
                return exp.Type switch
                {
                    RegExNodeTypes.Letter => false,
                    RegExNodeTypes.EmptyWord => false,
                    RegExNodeTypes.Empty => true,
                    RegExNodeTypes.KleeneClosure => false,
                    RegExNodeTypes.Choise => forest.GetChildren(exp).All(IsEmptyInner),
                    RegExNodeTypes.Concatenation => forest.GetChildren(exp).Any(IsEmptyInner),
                    _ => throw new ArgumentOutOfRangeException(nameof(exp), exp.Type, "Unknown type"),
                };
            }

            return IsEmptyInner(startNode);
        }

        public static bool IsEmptyWord<TLetter>(this ITree<IRegExExpressionNode<TLetter>> tree)
        {
            return tree.IsEmptyWord(tree.Root);
        }

        public static bool IsEmptyWord<TLetter>(this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> startNode)
        {
            bool IsEmptyWordInner(IRegExExpressionNode<TLetter> exp)
            {
                return exp.Type switch
                {
                    RegExNodeTypes.Letter or RegExNodeTypes.Empty => false,
                    RegExNodeTypes.EmptyWord => true,
                    RegExNodeTypes.KleeneClosure => forest.GetChildren(exp).All(IsEmptyWordInner),
                    RegExNodeTypes.Choise => forest.GetChildren(exp).All(IsEmptyWordInner),
                    RegExNodeTypes.Concatenation => forest.GetChildren(exp).All(IsEmptyWordInner),
                    _ => throw new ArgumentOutOfRangeException(nameof(exp), exp.Type, "Unknown type"),
                };
            }

            return IsEmptyWordInner(startNode);
        }

        public static bool IsInfinite<TLetter>(this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> startNode)
        {
            return startNode.Type switch
            {
                RegExNodeTypes.Letter => false,
                RegExNodeTypes.EmptyWord => false,
                RegExNodeTypes.Empty => false,
                RegExNodeTypes.KleeneClosure => !(forest.GetChildren(startNode).All(forest.IsEmpty) || forest.GetChildren(startNode).All(forest.IsEmptyWord)),
                RegExNodeTypes.Choise => forest.GetChildren(startNode).Any(forest.IsInfinite),
                RegExNodeTypes.Concatenation => forest.GetChildren(startNode).All(x => !forest.IsEmpty(x)) && forest.GetChildren(startNode).Any(forest.IsInfinite),
                _ => throw new ArgumentOutOfRangeException(nameof(startNode), startNode, "Unknown type"),
            };
        }

        public static StatefulObject<BitArray, TLetter> GetDFA<TLetter>(this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> exp, IEnumerable<TLetter> alphabet)
        {
            return new StatefulObject<BitArray, TLetter>(CreateStateMachine(forest, exp, alphabet));
        }

        public static DictionaryStateMachine<BitArray, TLetter> CreateStateMachine<TLetter>(IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> exp, IEnumerable<TLetter> alphabet)
        {
            int n = 0;
            var epsNFA = forest.GetNFA(exp, () => n++);

            var startState = new BitArray(n);
            startState[epsNFA.startState] = true;

            var finalState = epsNFA.finalState;
            bool isFinal(BitArray x) => x[finalState];

            var transitions = alphabet.SelectMany(letter => GetPWSTransitionsWithLetter(n, epsNFA, letter))
                .ToDictionary(tuple => (tuple.Item1, tuple.Item2), tuple => tuple.Item3);

            return new DictionaryStateMachine<BitArray, TLetter>(transitions, isFinal, startState);
        }

        private static IEnumerable<Tuple<BitArray, TLetter, BitArray>> GetPWSTransitionsWithLetter<TLetter>(
            int n, EpsilonDFA<int, TLetter> epsNFA, TLetter letter)
        {
            BitArray[] reachable = new BitArray[n];
            for (int i = 0; i < n; i++)
            {
                var states = epsNFA.ReachableStates(i, letter);
                reachable[i] = new BitArray(n);
                foreach (var endState in states)
                {
                    reachable[i][endState] = true;
                }
            }

            foreach (var powerSet in AllStates(n))
            {
                var endPowerSet = new BitArray(n);
                for (int i = 0; i < n; i++)
                {
                    if (powerSet[i])
                    {
                        for (int j = 0; j < n; j++)
                        {
                            endPowerSet.Or(reachable[i]);
                        }
                    }
                }
                yield return Tuple.Create(powerSet, letter, endPowerSet);
            }
        }

        public static IEnumerable<BitArray> AllStates(int length)
        {
            BigInteger pow = BigInteger.One << length;

            for (BigInteger i = 0; i <= pow; i++)
            {
                var a = new BitArray(i.ToByteArray())
                {
                    Length = length
                };
                yield return a;
            }
        }

        private static EpsilonDFA<TState, TLetter> GetNFA<TState, TLetter>(
            this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> head, Func<TState> getNewState)
        {
            EpsilonDFA<TState, TLetter>.Builder GetNFAInner(IRegExExpressionNode<TLetter> node, IEnumerable<EpsilonDFA<TState, TLetter>.Builder> children)
            {
                return node.Type switch
                {
                    RegExNodeTypes.Letter => EpsilonDFA<TState, TLetter>.Builder.Letter(
                                                ((Letter<TLetter>)node).LetterToMatch,
                                                getNewState),
                    RegExNodeTypes.EmptyWord => EpsilonDFA<TState, TLetter>.Builder.EmptyWord(getNewState),
                    RegExNodeTypes.Empty => EpsilonDFA<TState, TLetter>.Builder.Empty(getNewState),

                    RegExNodeTypes.KleeneClosure => EpsilonDFA<TState, TLetter>.Builder.KleeneClosure(
                                                children.First(),
                                                getNewState),

                    RegExNodeTypes.Choise => EpsilonDFA<TState, TLetter>.Builder.Choise(
                                                children.First(),
                                                children.Last(),
                                                getNewState),

                    RegExNodeTypes.Concatenation => EpsilonDFA<TState, TLetter>.Builder.Concatenation(
                                                children.First(),
                                                children.Last(),
                                                getNewState),

                    _ => throw new ArgumentOutOfRangeException(nameof(node.Type), node.Type, "Unknown type"),
                };
            }

            return forest.Aggregate<EpsilonDFA<TState, TLetter>.Builder, IRegExExpressionNode<TLetter>>(GetNFAInner, head).Build();
        }

        public static ArrayTree<IRegExExpressionNode<TLetter>> Simplify<TLetter>(this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> head)
        {
            var indexTree = IndexBinaryTree.Instance;
            var newTree = new ArrayTree<IRegExExpressionNode<TLetter>>(indexTree);

            void SimplifyInner(IRegExExpressionNode<TLetter> exp, int index)
            {
                switch (exp.Type)
                {
                    case RegExNodeTypes.Letter:
                    case RegExNodeTypes.EmptyWord:
                    case RegExNodeTypes.Empty:
                        //No change
                        newTree.SetItemToIndex(index, exp);
                        break;

                    case RegExNodeTypes.KleeneClosure:
                        var child = forest.GetChildren(exp).First();
                        if (IsEmptyWord(forest, child))
                        {
                            //(epsilon)* = epsilon
                            newTree.SetItemToIndex(index, child);
                        }
                        else if (IsEmpty(forest, child))
                        {
                            //(empty)* = epsilon
                            newTree.SetItemToIndex(index, EmptyWord<TLetter>.Instance);
                        }
                        else if (child.Type == RegExNodeTypes.Choise)
                        {
                            var first = forest.GetChildren(child).First();
                            var second = forest.GetChildren(child).Last();

                            if (IsEmptyWord(forest, first))
                            {
                                //(epsilon + r)* = r*
                                newTree.SetItemToIndex(index, KleeneClosure<TLetter>.Instance);
                                SimplifyInner(second, indexTree.GetFirstChild(index));
                            }
                            else if (IsEmptyWord(forest, second))
                            {
                                //(r + epsilon)* = r*
                                newTree.SetItemToIndex(index, KleeneClosure<TLetter>.Instance);
                                SimplifyInner(first, indexTree.GetFirstChild(index));
                            }
                            else
                            {
                                // No change
                                newTree.SetItemToIndex(index, exp);
                                SimplifyInner(child, indexTree.GetFirstChild(index));
                            }
                        }
                        else
                        {
                            // No change
                            newTree.SetItemToIndex(index, exp);
                            SimplifyInner(child, indexTree.GetFirstChild(index));
                        }
                        break;

                    case RegExNodeTypes.Choise:
                        {
                            var first = forest.GetChildren(exp).First();
                            var second = forest.GetChildren(exp).Last();

                            if (IsEmpty(forest, first))
                            {
                                //empty + r = r
                                SimplifyInner(second, index);
                            }
                            else if (IsEmpty(forest, second))
                            {
                                //r + empty = r
                                SimplifyInner(first, index);
                            }
                            else
                            {
                                // No change
                                newTree.SetItemToIndex(index, exp);
                                SimplifyInner(first, indexTree.GetFirstChild(index));
                                SimplifyInner(second, indexTree.GetSecondChild(index));
                            }
                        }
                        break;
                    case RegExNodeTypes.Concatenation:
                        {
                            var first = forest.GetChildren(exp).First();
                            var second = forest.GetChildren(exp).Last();

                            if (IsEmpty(forest, first) || IsEmpty(forest, second))
                            {
                                //empty r = r empty = empty
                                newTree.SetItemToIndex(index, Empty<TLetter>.Instance);
                            }
                            else if (IsEmptyWord(forest, first))
                            {
                                //epsilon r = r
                                SimplifyInner(second, index);
                            }
                            else if (IsEmptyWord(forest, second))
                            {
                                //r epsilon = r
                                SimplifyInner(first, index);
                            }
                            else if (first.Type == RegExNodeTypes.Choise)
                            {
                                //(s + t) r = s r + t r
                                var first2 = forest.GetChildren(first).First();
                                var second2 = forest.GetChildren(first).Last();
                                var firstChildIndex = indexTree.GetFirstChild(index);
                                var secondChildIndex = indexTree.GetSecondChild(index);

                                newTree.SetItemToIndex(index, Choise<TLetter>.Instance);
                                newTree.SetItemToIndex(firstChildIndex, Concatenation<TLetter>.Instance);
                                newTree.SetItemToIndex(secondChildIndex, Concatenation<TLetter>.Instance);

                                SimplifyInner(first2, indexTree.GetFirstChild(firstChildIndex));
                                SimplifyInner(second, indexTree.GetSecondChild(firstChildIndex));
                                SimplifyInner(second2, indexTree.GetFirstChild(secondChildIndex));
                                SimplifyInner(second, indexTree.GetSecondChild(secondChildIndex));
                            }
                            else if (second.Type == RegExNodeTypes.Choise)
                            {
                                //r (s + t) r = r s + r t
                                var first2 = forest.GetChildren(second).First();
                                var second2 = forest.GetChildren(second).Last();
                                var firstChildIndex = indexTree.GetFirstChild(index);
                                var secondChildIndex = indexTree.GetSecondChild(index);

                                newTree.SetItemToIndex(index, Choise<TLetter>.Instance);
                                newTree.SetItemToIndex(firstChildIndex, Concatenation<TLetter>.Instance);
                                newTree.SetItemToIndex(secondChildIndex, Concatenation<TLetter>.Instance);

                                SimplifyInner(first, indexTree.GetFirstChild(firstChildIndex));
                                SimplifyInner(first2, indexTree.GetSecondChild(firstChildIndex));
                                SimplifyInner(first, indexTree.GetFirstChild(secondChildIndex));
                                SimplifyInner(second2, indexTree.GetSecondChild(secondChildIndex));
                            }
                            else
                            {
                                // No change
                                newTree.SetItemToIndex(index, exp);
                                SimplifyInner(first, indexTree.GetFirstChild(index));
                                SimplifyInner(second, indexTree.GetSecondChild(index));
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(exp.Type), exp.Type, "Unknown type");
                }
            }

            SimplifyInner(head, newTree.RootIndex);

            return newTree;
        }

        private class EpsilonDFA<TState, TLetter>
        {
            public sealed class Builder
            {
                public readonly IEnumerable<Tuple<TState, TLetter, TState>> transitions;
                public readonly IEnumerable<Tuple<TState, TState>> epsilonTransitions;
                public readonly TState startState;
                public readonly TState finalState;

                private Builder(
                    IEnumerable<Tuple<TState, TLetter, TState>> transitions,
                    IEnumerable<Tuple<TState, TState>> epsilonTransitions,
                    TState startState,
                    TState finalState)
                {
                    this.transitions = transitions;
                    this.epsilonTransitions = epsilonTransitions;
                    this.startState = startState;
                    this.finalState = finalState;
                }

                public static Builder Choise(
                    Builder first,
                    Builder second,
                    Func<TState> getNewState)
                {
                    var start = getNewState();
                    var final = getNewState();

                    var epsilonTransitions =
                        first.epsilonTransitions.Concat(
                        second.epsilonTransitions).Concat(
                            Tuple.Create(start, first.startState),
                            Tuple.Create(start, second.startState),
                            Tuple.Create(first.finalState, final),
                            Tuple.Create(second.finalState, final));

                    var transitions = first.transitions.Concat(second.transitions);

                    return new Builder(transitions, epsilonTransitions, start, final);
                }

                public static Builder Concatenation(
                    Builder first,
                    Builder second,
                    Func<TState> getNewState)
                {
                    var epsilonTransitions =
                        first.epsilonTransitions.Concat(
                        second.epsilonTransitions).Concat(
                            Tuple.Create(first.finalState, second.startState));

                    var transitions = first.transitions.Concat(second.transitions);

                    return new Builder(transitions, epsilonTransitions,
                        first.startState, second.finalState);
                }

                public static Builder KleeneClosure(
                    Builder toRepeat,
                    Func<TState> getNewState)
                {
                    var onlyNew = getNewState();

                    var epsilonTransitions =
                        toRepeat.epsilonTransitions.Concat(
                            Tuple.Create(onlyNew, toRepeat.startState),
                            Tuple.Create(toRepeat.finalState, onlyNew));

                    return new Builder(toRepeat.transitions, epsilonTransitions, onlyNew, onlyNew);
                }

                public static Builder Letter(TLetter letter, Func<TState> getNewState)
                {
                    var start = getNewState();
                    var final = getNewState();
                    return new Builder(
                        Tuple.Create(start, letter, final).GetArray(),
                        Enumerable.Empty<Tuple<TState, TState>>(),
                        start,
                        final);
                }

                public static Builder EmptyWord(Func<TState> getNewState)
                {
                    var only = getNewState();

                    return new Builder(
                        Enumerable.Empty<Tuple<TState, TLetter, TState>>(),
                        Enumerable.Empty<Tuple<TState, TState>>(),
                        only,
                        only);
                }

                public static Builder Empty(Func<TState> getNewState)
                {
                    var start = getNewState();
                    var final = getNewState();
                    return new Builder(
                        Enumerable.Empty<Tuple<TState, TLetter, TState>>(),
                        Enumerable.Empty<Tuple<TState, TState>>(),
                        start,
                        final);
                }

                public EpsilonDFA<TState, TLetter> Build()
                {
                    return new EpsilonDFA<TState, TLetter>(this);
                }
            }

            public readonly Dictionary<(TState, TLetter), TState[]> transitions;
            public readonly Dictionary<TState, IEnumerable<TState>> epsilonTransitions;
            public readonly TState startState;
            public readonly TState finalState;
            readonly IEqualityComparer<TState> stateComparison;
            readonly IEqualityComparer<TLetter> letterComparison;
            readonly DictionaryBasedGraph<TState> epsilonGraph;

            private EpsilonDFA(Builder builder, IEqualityComparer<TState> stateComparison = null, IEqualityComparer<TLetter> letterComparison = null)
            {
                stateComparison ??= EqualityComparer<TState>.Default;
                letterComparison ??= EqualityComparer<TLetter>.Default;
                this.stateComparison = stateComparison;
                this.letterComparison = letterComparison;

                transitions = builder.transitions
                    .GroupBy(tuple => (tuple.Item1, tuple.Item2))
                    .ToDictionary(
                    group => group.Key,
                    group => group.Select(t => t.Item3).ToArray(),
                    TupleEqualityComparer<TState, TLetter>.Create(stateComparison, letterComparison));

                epsilonTransitions = builder.epsilonTransitions
                    .GroupBy(tuple => tuple.Item1)
                    .ToDictionary(
                    group => group.Key,
                    group => group.Select(t => t.Item2).ToArray() as IEnumerable<TState>,
                    stateComparison);

                epsilonGraph = new DictionaryBasedGraph<TState>(epsilonTransitions);

                startState = builder.startState;
                finalState = builder.finalState;
            }

            public IEnumerable<TState> ReachableStates(TState state, TLetter letter)
            {
                var nodes = from start in EpsilonReachableStates(state)
                            from letterTransition in transitions.Keys
                            where stateComparison.Equals(start, letterTransition.Item1) && letterComparison.Equals(letter, letterTransition.Item2)
                            from end in transitions[letterTransition]
                            from end2 in EpsilonReachableStates(end)
                            select end2;

                return new HashSet<TState>(nodes, stateComparison);
            }

            public IEnumerable<TState> EpsilonReachableStates(TState state)
            {
                return epsilonGraph.BreadthFirstTraversal(state);
            }
        }
    }
}
