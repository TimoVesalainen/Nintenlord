using System;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class BetweenParser<TIn, TStart, TEnd, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> valueParser;
        private readonly IParser<TIn, TStart> startParser;
        private readonly IParser<TIn, TEnd> endParser;

        public BetweenParser(IParser<TIn, TStart> startParser, IParser<TIn, TOut> valueParser, IParser<TIn, TEnd> endParser)
        {
            this.valueParser = valueParser ?? throw new ArgumentNullException(nameof(valueParser));
            this.startParser = startParser ?? throw new ArgumentNullException(nameof(startParser));
            this.endParser = endParser ?? throw new ArgumentNullException(nameof(endParser));
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner);

            startParser.Parse(scanner, out Match<TIn> latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default;
            }

            TOut result = valueParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default;
            }

            endParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default;
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format("({1}{0}{2})", valueParser, startParser, endParser);
        }
    }
}
