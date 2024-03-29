﻿using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class TryParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> parserToTry;

        public TryParser(IParser<TIn, TOut> parserToTry)
        {
            this.parserToTry = parserToTry ?? throw new ArgumentNullException(nameof(parserToTry));
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            if (!scanner.CanSeek)
            {
                throw new ArgumentException("Scanner can't seek.");
            }

            long offset = scanner.Offset;
            TOut result = parserToTry.Parse(scanner, out match);
            if (!match.Success)
            {
                scanner.Offset = offset;
            }
            return result;
        }
    }
}
