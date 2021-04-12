using Nintenlord.Trees;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.ProceduralGeneration
{
    /// <summary>
    /// Deterministic context free Lindenmayer system
    /// </summary>
    /// <typeparam name="TCharacter"></typeparam>
    public sealed class LindenmayerSystem<TCharacter> : ITree<TCharacter>
    {
        readonly IDictionary<TCharacter, TCharacter[]> productionRules;

        public LindenmayerSystem(TCharacter start, IEnumerable<(TCharacter, IEnumerable<TCharacter>)> rules)
        {
            if (rules is null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            Root = start;
            productionRules = rules.ToDictionary(rule => rule.Item1, rule => rule.Item2.ToArray());
        }

        public TCharacter Root { get; }

        public IEnumerable<TCharacter> GetChildren(TCharacter node)
        {
            return productionRules[node];
        }
    }

    public static class LindenmayerSystemHelpers
    {
        public static LindenmayerSystem<TCharacter> FromVariablesAndConstants<TCharacter>(
            TCharacter start, IEnumerable<(TCharacter, IEnumerable<TCharacter>)> variables, IEnumerable<TCharacter> constants)
        {
            if (constants is null)
            {
                throw new ArgumentNullException(nameof(constants));
            }

            if (variables is null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            return new LindenmayerSystem<TCharacter>(start,
                variables.Concat(constants.Select(cons => (cons, Enumerable.Repeat(cons, 1)))));
        }
        public static LindenmayerSystem<TCharacter> FromVariablesAndConstants<TCharacter>(
            TCharacter start, IEnumerable<(TCharacter, IEnumerable<TCharacter>)> variables, params TCharacter[] constants)
        {
            return FromVariablesAndConstants(start, variables, constants.AsEnumerable());
        }


        public static LindenmayerSystem<Either<TVar, TConstant>> FromVariablesAndConstants<TVar, TConstant>(
            TVar start, IEnumerable<(TVar, IEnumerable<Either<TVar, TConstant>>)> variables, IEnumerable<TConstant> constants)
        {
            if (constants is null)
            {
                throw new ArgumentNullException(nameof(constants));
            }

            if (variables is null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            return new LindenmayerSystem<Either<TVar, TConstant>>(start,
                variables.Select(pair => ((Either<TVar, TConstant>)pair.Item1, pair.Item2))
                .Concat(constants.Select(c => (Either<TVar, TConstant>)c)
                .Select(cons => (cons, Enumerable.Repeat(cons, 1)))));
        }

        public static readonly LindenmayerSystem<char> Algae =
            new LindenmayerSystem<char>('A', new [] {
                ('A', "AB".AsEnumerable()),
                ('B', "A")});

        public static readonly LindenmayerSystem<char> FractalTree =
            FromVariablesAndConstants('0', new[] {
                ('0', new[] { '1','[', '0', ']', '0' }.AsEnumerable()),
                ('1', new[] { '1', '1' })},
                '[',
                ']');

        public static readonly LindenmayerSystem<char> CantorSet =
            new LindenmayerSystem<char>('A', new[] {
                ('A', "ABA".AsEnumerable()),
                ('B', "BBB")});

        public static readonly LindenmayerSystem<char> KochCurve =
            FromVariablesAndConstants('F', new[] {
                ('F', "F+F-F-F+F".AsEnumerable())},
                '+', '-');

        //S is hack to allow starting from several characters
        public static readonly LindenmayerSystem<char> SierpinskiTriangle =
            FromVariablesAndConstants('S', new[] {
                ('S', "F-G-G".AsEnumerable()),
                ('F', "F-G+F+G-F".AsEnumerable()),
                ('G', "GG".AsEnumerable())},
                '+', '-');

        public static readonly LindenmayerSystem<char> SierpinskiTriangleArrowHead =
            FromVariablesAndConstants('A', new[] {
                ('A', "B-A-B".AsEnumerable()),
                ('B', "A+B+A".AsEnumerable())},
                '+', '-');

        public static readonly LindenmayerSystem<char> DragonCurve =
            FromVariablesAndConstants('F', new[] {
                ('F', "F+G".AsEnumerable()),
                ('G', "F-G".AsEnumerable())},
                '+', '-');

        public static readonly LindenmayerSystem<char> BarnsleyFern =
            FromVariablesAndConstants('F', new[] {
                ('X', "F+[[X]-X]-F[-FX]+X".AsEnumerable()),
                ('F', "FF".AsEnumerable())},
                '+', '-', '[', ']');
    }
}
