using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser
{
    public sealed class FunctionParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly Func<IScanner<TIn>, Tuple<TOut, Match<TIn>>> parserFunction;

        public FunctionParser(Func<IScanner<TIn>, Tuple<TOut, Match<TIn>>> parserFunction)
        {
            this.parserFunction = parserFunction;
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            var parseResult = parserFunction(scanner);

            match = parseResult.Item2;
            return parseResult.Item1;
        }
    }
}
