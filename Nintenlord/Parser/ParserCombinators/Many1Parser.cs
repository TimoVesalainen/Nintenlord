using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class Many1Parser<TIn, TOut> : RepeatingParser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> toRepeat;

        public Many1Parser(IParser<TIn, TOut> toRepeat)
        {
            if (toRepeat == null)
            {
                throw new ArgumentNullException("toRepeat");
            }

            this.toRepeat = toRepeat;
        }

        protected override IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {

            TOut prim = toRepeat.Parse(scanner, out Match<TIn> latestMatch);
            if (!latestMatch.Success)
            {
                InnerMatch += latestMatch;
            }
            else
            {
                InnerMatch += latestMatch;
                yield return prim;

                while (true)
                {
                    prim = toRepeat.Parse(scanner, out latestMatch);
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
        }

        public override string ToString()
        {
            return string.Format("({0})+", toRepeat);
        }
    }
}
