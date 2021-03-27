using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class ReturnParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly TOut toReturn;

        public ReturnParser(TOut toReturn)
        {
            this.toReturn = toReturn;
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner, 0);
            return toReturn;
        }
    }
}
