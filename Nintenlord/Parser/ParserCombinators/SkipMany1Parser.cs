using System;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SkipMany1Parser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> toRepeat;

        public SkipMany1Parser(IParser<TIn, TOut> toRepeat)
        {
            this.toRepeat = toRepeat ?? throw new ArgumentNullException(nameof(toRepeat));
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner, 0);

            toRepeat.Parse(scanner, out Match<TIn> latestMatch);
            if (!latestMatch.Success)
            {
                match = latestMatch;
            }
            else
            {
                while (true)
                {
                    toRepeat.Parse(scanner, out latestMatch);
                    if (latestMatch.Success)
                    {
                        match += latestMatch;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return default;
        }
    }
}
