using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class CountParser<TIn, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> parser;
        private readonly int count;

        public CountParser(IParser<TIn, TOut> parser, int count)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.count = count;
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            for (int i = 0; i < count; i++)
            {
                var temp = parser.Parse(scanner, out Match<TIn> latestMatch);
                InnerMatch += latestMatch;

                if (!latestMatch.Success)
                {
                    yield break;
                }

                yield return temp;
            }
        }
    }
}
