using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class OneOfParser<T> : Parser<T, T>
    {
        private readonly ICollection<T> validValues;

        public OneOfParser(ICollection<T> validValues)
        {
            this.validValues = validValues ?? throw new ArgumentNullException(nameof(validValues));
        }


        protected override T ParseMain(IO.Scanners.IScanner<T> scanner, out Match<T> match)
        {
            T val = scanner.Current;

            if (validValues.Contains(val))
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
