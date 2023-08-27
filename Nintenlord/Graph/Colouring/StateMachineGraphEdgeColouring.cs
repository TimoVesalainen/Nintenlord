using Nintenlord.StateMachines.Finite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Graph.Colouring
{
    public sealed class StateMachineGraphEdgeColouring<TState, TInput> : IEdgeColouring<TState, TInput>
    {
        readonly IFiniteStateMachine<TState, TInput> stateMachine;
        readonly IEnumerable<TInput> inputs;
        readonly IEqualityComparer<TState> comparer;

        public StateMachineGraphEdgeColouring(IFiniteStateMachine<TState, TInput> stateMachine, IEnumerable<TInput> inputs, IEqualityComparer<TState> comparer = null)
        {
            this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            this.inputs = inputs ?? throw new ArgumentNullException(nameof(inputs));
            this.comparer = comparer ?? EqualityComparer<TState>.Default;
        }

        public TInput this[TState startNode, TState endNode] => GetInputs(startNode, endNode).First();

        public IEnumerable<TState> Nodes => stateMachine.States;

        public IEnumerable<TInput> GetColours() => inputs;

        public IEnumerable<TState> GetNeighbours(TState node)
        {
            return inputs.Select(input => stateMachine.Transition(node, input)).Distinct(comparer);
        }

        public bool IsEdge(TState from, TState to)
        {
            return GetInputs(from, to).Any();
        }

        private IEnumerable<TInput> GetInputs(TState fromNode, TState toNode)
        {
            return from input in inputs
                   let next = stateMachine.Transition(fromNode, input)
                   where comparer.Equals(next, toNode)
                   select input;
        }
    }
}
