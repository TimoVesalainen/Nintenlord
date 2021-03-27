using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SeparatedByParser<TIn, TSeb, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TSeb> separator;
        private readonly IParser<TIn, TOut> results;

        public SeparatedByParser(IParser<TIn, TSeb> results, IParser<TIn, TOut> separated)
        {
            if (results == null)
            {
                throw new ArgumentNullException("results");
            }

            if (separated == null)
            {
                throw new ArgumentNullException("separated");
            }

            this.results = separated;
            this.separator = results;
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            while (true)
            {
                TOut prim = results.Parse(scanner, out Match<TIn> latestMatch);
                InnerMatch += latestMatch;

                if (latestMatch.Success)
                {
                    yield return prim;
                }
                else
                {
                    yield break;
                }

                separator.Parse(scanner, out latestMatch);
                if (latestMatch.Success)
                {
                    InnerMatch += latestMatch;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
