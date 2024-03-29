﻿using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class ManyParser<TIn, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> toRepeat;

        public ManyParser(IParser<TIn, TOut> toRepeat)
        {
            this.toRepeat = toRepeat ?? throw new ArgumentNullException(nameof(toRepeat));
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            while (!scanner.IsAtEnd)
            {
                TOut prim = toRepeat.Parse(scanner, out Match<TIn> latestMatch);
                if (latestMatch.Success)
                {
                    InnerMatch += latestMatch;
                    yield return prim;
                }
                else
                {
                    yield break;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("({0})*", toRepeat);
        }
    }
}
