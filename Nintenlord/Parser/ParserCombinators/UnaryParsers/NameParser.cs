using System;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class NameParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly string name;
        private readonly IParser<TIn, TOut> parser;

        public NameParser(IParser<TIn, TOut> parser, string name)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            TOut result = parser.Parse(scanner, out match);
            if (!match.Success)
            {
                match = new Match<TIn>(scanner,
                    result == null ? "Expected {0}" : "Expected {0}, got {1}", name, result);
                result = default;
            }

            return result;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
