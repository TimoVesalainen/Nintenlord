﻿using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class NothingParser<TIn, TOut> : Parser<TIn, TOut>
    {
        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner);
            return default;
        }
    }
}
