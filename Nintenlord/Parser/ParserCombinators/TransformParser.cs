using System;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class TransformParser<TIn, TMiddle, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TMiddle> parser;
        private readonly Converter<TMiddle, TOut> converter;

        public TransformParser(IParser<TIn, TMiddle> parser, Converter<TMiddle, TOut> converter)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            TMiddle middle = parser.Parse(scanner, out match);
            return match.Success ? converter(middle) : default;
        }

        public override string ToString()
        {
            return parser.ToString();
        }
    }
}
