using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class SafeFailureCheckerParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> parser;
        private readonly string errorText;

        public SafeFailureCheckerParser(IParser<TIn, TOut> parserToCheck, string errorText = "Safety check failed. Failing parser advanced.")
        {
            this.parser = parserToCheck ?? throw new ArgumentNullException(nameof(parserToCheck));
            this.errorText = errorText ?? throw new ArgumentNullException(nameof(errorText));
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            long start = scanner.Offset;
            var result = parser.Parse(scanner, out match);

            if ((match.Success && match.Length != scanner.Offset - start) ||
                (!match.Success && start != scanner.Offset))
            {
                //match = new Match<TIn>(scanner, errorText);
                //return default(TOut);
                throw new InvalidOperationException(errorText);
            }

            return result;
        }
    }
}
