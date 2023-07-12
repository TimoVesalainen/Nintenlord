using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.ParserCombinators.BinaryParsers
{
    public sealed class OrParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut> first, second;

        public OrParser(IParser<TIn, TOut> first, IParser<TIn, TOut> second)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            var currentOffset = scanner.Offset;
            TOut result = first.Parse(scanner, out match);

            if (!match.Success)
            {
                if (currentOffset == scanner.Offset)
                {
                    result = second.Parse(scanner, out match);
                }
                else
                {
                    result = default;
                    match = new Match<TIn>(scanner, "First option consumed input.");
                }
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format("({0})|({1})", first, second);
        }
    }
}
