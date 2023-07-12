using System;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class OptionalParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly TOut defaultVat;
        private readonly IParser<TIn, TOut> parser;

        public OptionalParser(IParser<TIn, TOut> parser, TOut defaultVat = default)
        {
            this.defaultVat = defaultVat;
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            var result = parser.Parse(scanner, out match);
            if (!match.Success)
            {
                result = defaultVat;
                match = new Match<TIn>(scanner, 0);
            }
            return result;
        }
    }
}
