using System;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SkipManyParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> toRepeat;

        public SkipManyParser(IParser<TIn, TOut> toRepeat)
        {
            if (toRepeat == null)
            {
                throw new ArgumentNullException("toRepeat");
            }

            this.toRepeat = toRepeat;
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner, 0);
            while (true)
            {
                toRepeat.Parse(scanner, out Match<TIn> latestMatch);
                if (latestMatch.Success)
                {
                    match += latestMatch;
                }
                else
                {
                    break;
                }
            }

            return default(TOut);
        }
    }
}
