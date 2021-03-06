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
            if (startParser == null)
            {
                throw new ArgumentNullException("startParser");
            }

            if (valueParser == null)
            {
                throw new ArgumentNullException("valueParser");
            }

            if (endParser == null)
            {
                throw new ArgumentNullException("endParser");
            }

            this.valueParser = valueParser;
            this.startParser = startParser;
            this.endParser = endParser;
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner);

            startParser.Parse(scanner, out Match<TIn> latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default(TOut);
            }

            TOut result = valueParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default(TOut);
            }

            endParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default(TOut);
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format("({1}{0}{2})", valueParser, startParser, endParser);
        }
    }
}
