using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class NoneOfParser<T> : Parser<T, T>
    {
        private readonly ICollection<T> invalidValues;

        public NoneOfParser(ICollection<T> invalidValues)
        {
            this.invalidValues = invalidValues ?? throw new ArgumentNullException(nameof(invalidValues));
        }


        protected override T ParseMain(IO.Scanners.IScanner<T> scanner, out Match<T> match)
        {
            T val = scanner.Current;

            if (!invalidValues.Contains(val))
            {
                match = new Match<T>(scanner, 1);
                scanner.MoveNext();
            }
            else
            {
                match = new Match<T>(scanner, "Invalid value {0}", val);
                val = default;
            }

            return val;
        }
    }
}
