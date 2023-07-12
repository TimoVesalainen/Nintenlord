using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class SatisfyParser<T> : Parser<T, T>
    {
        private readonly Predicate<T> comparer;

        public SatisfyParser(Predicate<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        protected override T ParseMain(IScanner<T> scanner, out Match<T> match)
        {
            if (scanner.IsAtEnd)
            {
                match = new Match<T>(scanner, "No token to test.");
                return default;
            }
            T unit = scanner.Current;
            if (comparer(unit))
            {
                match = new Match<T>(scanner, 1);
                scanner.MoveNext();
                return unit;
            }
            else
            {
                match = new Match<T>(scanner, "Predicate failed, got {0}", unit.ToString());
                return default;
            }
        }

        public override string ToString()
        {
            return "Predicate";
        }
    }
}
