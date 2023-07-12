using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SeparatedBy1Parser<TIn, TSeb, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TSeb> separator;
        private readonly IParser<TIn, TOut> results;

        public SeparatedBy1Parser(IParser<TIn, TSeb> separator, IParser<TIn, TOut> results)
        {
            this.results = results ?? throw new ArgumentNullException(nameof(results));
            this.separator = separator ?? throw new ArgumentNullException(nameof(separator));
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            TOut prim = results.Parse(scanner, out Match<TIn> latestMatch);
            InnerMatch += latestMatch;
            if (latestMatch.Success)
            {
                yield return prim;
                while (true)
                {
                    separator.Parse(scanner, out latestMatch);
                    if (latestMatch.Success)
                    {
                        InnerMatch += latestMatch;
                    }
                    else
                    {
                        yield break;
                    }

                    prim = results.Parse(scanner, out latestMatch);
                    InnerMatch += latestMatch;

                    if (latestMatch.Success)
                    {
                        yield return prim;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }
    }
}
