using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class EndByParser<TIn, TEnd, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> results;
        private readonly IParser<TIn, TEnd> separator;

        public EndByParser(IParser<TIn, TOut> results, IParser<TIn, TEnd> separator)
        {
            if (results == null)
            {
                throw new ArgumentNullException("results");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            this.results = results;
            this.separator = separator;
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            while (true)
            {
                TOut outRes = results.Parse(scanner, out Match<TIn> latestMatch);
                if (latestMatch.Success)
                {
                    InnerMatch += latestMatch;
                    yield return outRes;
                }
                else
                {
                    yield break;
                }

                separator.Parse(scanner, out latestMatch);
                InnerMatch += latestMatch;

                if (!latestMatch.Success)
                {
                    yield break;
                }
            }
        }
    }
}
