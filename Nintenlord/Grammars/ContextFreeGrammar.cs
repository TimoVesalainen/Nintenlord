using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Grammars
{
    public sealed class ContextFreeGrammar<T>
    {
        private readonly IDictionary<T, T[][]> productions;
        private readonly T startingSymbol;
        private readonly T[] variables;
        private readonly T[] terminals;

        public T[][] this[T variable] => productions[variable];
        public T StartingSymbol => startingSymbol;
        public IEnumerable<T> Variables => variables;
        public IEnumerable<T> Terminals => terminals;

        public ContextFreeGrammar(IDictionary<T, T[][]> productions, T startingSymbol, T[] variables, T[] terminals)
        {
            this.productions = productions ?? throw new ArgumentNullException(nameof(productions));
            this.startingSymbol = startingSymbol;
            this.variables = variables ?? throw new ArgumentNullException(nameof(variables));
            this.terminals = terminals ?? throw new ArgumentNullException(nameof(terminals));
        }

        public T[] DeriveRandom(Random random)
        {
            List<T> word = new List<T>(20) { startingSymbol };
            while (true)
            {
                int i;
                for (i = 0; i < word.Count; i++)
                {
                    if (variables.Contains(word[i]))
                    {
                        break;
                    }
                }

                if (i == word.Count)
                {
                    break;
                }

                var rules = productions[word[i]];
                var ruleToUse = rules[random.Next(rules.Length)];

                word.RemoveAt(i);
                word.InsertRange(i, ruleToUse);
            }
            return word.ToArray();
        }
    }
}
