using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class ManyTillParser<TIn, TEnd, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> results;
        private readonly IParser<TIn, TEnd> ender;

        public ManyTillParser(IParser<TIn, TOut> results, IParser<TIn, TEnd> ender)
        {
            if (results == null)
            {
                throw new ArgumentNullException("results");
            }

            if (ender == null)
            {
                throw new ArgumentNullException("ender");
            }

            this.results = results;
            this.ender = ender;
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            //Is this correct?
            while (true)
            {
                var pos = scanner.Offset;
                ender.Parse(scanner, out Match<TIn> latestMatch);
                if (latestMatch.Success)
                {
                    InnerMatch += latestMatch;
                    yield break;
                }
                if (pos != scanner.Offset)
                {
                    throw new InvalidOperationException(string.Format("Parser {0} advanced the stream.", ender));
                }

                TOut outRes = results.Parse(scanner, out latestMatch);
                InnerMatch += latestMatch;

                if (latestMatch.Success)
                {
                    yield return outRes;
                }
                else
                {
                    yield break;
                }
            }

            //InnerMatch += latestMatch;
        }
    }
}
