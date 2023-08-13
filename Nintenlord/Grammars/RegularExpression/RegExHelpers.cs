using Nintenlord.Collections;
using Nintenlord.Grammars.RegularExpression.Tree;
using Nintenlord.Graph;
using Nintenlord.Graph.PathFinding;
using Nintenlord.StateMachines;
using Nintenlord.StateMachines.Finite;
using Nintenlord.Trees;
using Nintenlord.Utility;
using Nintenlord.Utility.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static StatefulObject<bool[], TLetter> GetDFA<TLetter>(this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> exp, IEnumerable<TLetter> alphabet)
        {
            return new StatefulObject<bool[], TLetter>(CreateStateMachine(forest, exp, alphabet));
        }

        public static DictionaryStateMachine<bool[], TLetter> CreateStateMachine<TLetter>(IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> exp, IEnumerable<TLetter> alphabet)
        {
            int n = 0;
            var epsNFA = GetNFA(forest, exp, () => n++);

            bool[] startState = new bool[n];
            startState[epsNFA.startState] = true;

            var finalState = epsNFA.finalState;
            Predicate<bool[]> isFinal = x => x[finalState];


            var transitions = alphabet.SelectMany(letter => GetPWSTransitionsWithLetter(n, epsNFA, letter))
                .ToDictionary(tuple => (tuple.Item1, tuple.Item2), tuple => tuple.Item3);

            return new DictionaryStateMachine<bool[], TLetter>(transitions, isFinal, startState);
        }

        private static IEnumerable<Tuple<bool[], TLetter, bool[]>> GetPWSTransitionsWithLetter<TLetter>(
            int n, EpsilonDFA<int, TLetter> epsNFA, TLetter letter)
        {
            bool[][] reachable = new bool[n][];
            for (int i = 0; i < n; i++)
            {
                var states = epsNFA.ReachableStates(i, letter);
                reachable[i] = new bool[n];
                foreach (var endState in states)
                {
                    reachable[i][endState] = true;
                }
            }

            foreach (var powerSet in AllStates(n))
            {
                bool[] endPowerSet = new bool[n];
                for (int i = 0; i < n; i++)
                {
                    if (powerSet[i])
                    {
                        for (int j = 0; j < n; j++)
                        {
                            endPowerSet[j] |= reachable[i][j];//So many things can go wrong here.
                        }
                    }
                }
                yield return Tuple.Create(powerSet, letter, endPowerSet);
            }
        }

        public static IEnumerable<bool[]> AllStates(int length)
        {
            bool[] values = new bool[length];

            if (length < 32)
            {
                int pow = 1 << length;
                yield return values.Clone() as bool[];

                for (int i = 1; i <= pow; i++)
                {
                    int index = i.TrailingZeroCount();
                    values[index] ^= true;
                    yield return values.Clone() as bool[];
                }
            }
            else if (length <= 64)
            {
                long pow = (long)1 << length;
                yield return values.Clone() as bool[];

                for (long i = 1; i <= pow; i++)
                {
                    int index = i.TrailingZeroCount();
                    values[index] ^= true;
                    yield return values.Clone() as bool[];
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static EpsilonDFA<TState, TLetter> GetNFA<TState, TLetter>(
            this IForest<IRegExExpressionNode<TLetter>> forest, IRegExExpressionNode<TLetter> head, Func<TState> getNewState)
        {
            EpsilonDFA<TState, TLetter> GetNFAInner(IRegExExpressionNode<TLetter> node)
            {
                return node.Type switch
                {
                    RegExNodeTypes.Letter => EpsilonDFA<TState, TLetter>.Letter(
                                                ((Letter<TLetter>)node).LetterToMatch,
                                                getNewState),
                    RegExNodeTypes.EmptyWord => EpsilonDFA<TState, TLetter>.EmptyWord(getNewState),
                    RegExNodeTypes.Empty => EpsilonDFA<TState, TLetter>.Empty(getNewState),
                    RegExNodeTypes.KleeneClosure => EpsilonDFA<TState, TLetter>.KleeneClosure(
                                                GetNFAInner(forest.GetChildren(node).First()),
                                                getNewState),
                    RegExNodeTypes.Choise => EpsilonDFA<TState, TLetter>.Choise(
                                                GetNFAInner(forest.GetChildren(node).First()),
                                                GetNFAInner(forest.GetChildren(node).Last()),
                                                getNewState),
                    RegExNodeTypes.Concatenation => EpsilonDFA<TState, TLetter>.Concatenation(
                                                GetNFAInner(forest.GetChildren(node).First()),
                                                GetNFAInner(forest.GetChildren(node).Last()),
                                                getNewState),
                    _ => throw new ArgumentException(),
                };
            }

            return GetNFAInner(head);
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

        private class EpsilonDFA<TState, TLetter> : IGraph<TState>
        {
            public readonly IEnumerable<Tuple<TState, TLetter, TState>> transitions;
            public readonly IEnumerable<Tuple<TState, TState>> epsilonTransitions;
            public readonly TState startState;
            public readonly TState finalState;

            public IEnumerable<TState> Nodes => throw new NotImplementedException();

            private EpsilonDFA(
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

            public static EpsilonDFA<TState, TLetter> Choise(
                EpsilonDFA<TState, TLetter> first,
                EpsilonDFA<TState, TLetter> second,
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

                return new EpsilonDFA<TState, TLetter>(transitions, epsilonTransitions, start, final);
            }

            public static EpsilonDFA<TState, TLetter> Concatenation(
                EpsilonDFA<TState, TLetter> first,
                EpsilonDFA<TState, TLetter> second,
                Func<TState> getNewState)
            {
                var epsilonTransitions =
                    first.epsilonTransitions.Concat(
                    second.epsilonTransitions).Concat(
                        Tuple.Create(first.finalState, second.startState));

                var transitions = first.transitions.Concat(second.transitions);

                return new EpsilonDFA<TState, TLetter>(transitions, epsilonTransitions,
                    first.startState, second.finalState);
            }

            public static EpsilonDFA<TState, TLetter> KleeneClosure(
                EpsilonDFA<TState, TLetter> toRepeat,
                Func<TState> getNewState)
            {
                var onlyNew = getNewState();

                var epsilonTransitions =
                    toRepeat.epsilonTransitions.Concat(
                        Tuple.Create(onlyNew, toRepeat.startState),
                        Tuple.Create(toRepeat.finalState, onlyNew));

                return new EpsilonDFA<TState, TLetter>(toRepeat.transitions, epsilonTransitions, onlyNew, onlyNew);
            }

            public static EpsilonDFA<TState, TLetter> Letter(TLetter letter, Func<TState> getNewState)
            {
                var start = getNewState();
                var final = getNewState();
                return new EpsilonDFA<TState, TLetter>(
                    Tuple.Create(start, letter, final).GetArray(),
                    Enumerable.Empty<Tuple<TState, TState>>(),
                    start,
                    final);
            }

            public static EpsilonDFA<TState, TLetter> EmptyWord(Func<TState> getNewState)
            {
                var only = getNewState();

                return new EpsilonDFA<TState, TLetter>(
                    Enumerable.Empty<Tuple<TState, TLetter, TState>>(),
                    Enumerable.Empty<Tuple<TState, TState>>(),
                    only,
                    only);
            }

            public static EpsilonDFA<TState, TLetter> Empty(Func<TState> getNewState)
            {
                var start = getNewState();
                var final = getNewState();
                return new EpsilonDFA<TState, TLetter>(
                    Enumerable.Empty<Tuple<TState, TLetter, TState>>(),
                    Enumerable.Empty<Tuple<TState, TState>>(),
                    start,
                    final);
            }

            public HashSet<TState> EpsClosure(TState state)
            {
                return Dijkstra_algorithm.GetConnectedNodes(state, this, EqualityComparer<TState>.Default);
            }

            public HashSet<TState> ReachableStates(TState state, TLetter letter)
            {
                throw new NotImplementedException();
            }

            #region IGraph<TState> Members

            public IEnumerable<TState> GetNeighbours(TState node)
            {
                return from item in epsilonTransitions
                       where EqualityComparer<TState>.Default.Equals(item.Item1, node)
                       select item.Item2;
            }

            public bool IsEdge(TState node1, TState node2)
            {
                return epsilonTransitions.Contains(Tuple.Create(node1, node2));
            }

            #endregion
        }
    }
}
