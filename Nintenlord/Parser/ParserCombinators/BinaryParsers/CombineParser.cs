using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.ParserCombinators.BinaryParsers
{
    public sealed class CombineParser<TIn, TMiddle1, TMiddle2, TOut> : Parser<TIn, TOut>
    {
        private readonly Func<TMiddle1, TMiddle2, TOut> combiner;
        private readonly IParser<TIn, TMiddle1> first;
        private readonly IParser<TIn, TMiddle2> second;

        public CombineParser(IParser<TIn, TMiddle1> first, IParser<TIn, TMiddle2> second,
            Func<TMiddle1, TMiddle2, TOut> combiner)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
            this.combiner = combiner ?? throw new ArgumentNullException(nameof(combiner));
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            var mid1 = first.Parse(scanner, out match);

            if (!match.Success)
            {
                return default;
            }

            var mid2 = second.Parse(scanner, out Match<TIn> secondMatch);
            match += secondMatch;

            return !match.Success ? default : combiner(mid1, mid2);
        }
    }

    public sealed class CombineParser<TIn, TMiddle1, TMiddle2, TMiddle3, TOut> : Parser<TIn, TOut>
    {
        private readonly Func<TMiddle1, TMiddle2, TMiddle3, TOut> combiner;
        private readonly IParser<TIn, TMiddle1> first;
        private readonly IParser<TIn, TMiddle2> second;
        private readonly IParser<TIn, TMiddle3> third;

        public CombineParser(
            IParser<TIn, TMiddle1> first,
            IParser<TIn, TMiddle2> second,
            IParser<TIn, TMiddle3> third,
            Func<TMiddle1, TMiddle2, TMiddle3, TOut> combiner)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
            this.third = third ?? throw new ArgumentNullException(nameof(third));
            this.combiner = combiner ?? throw new ArgumentNullException(nameof(combiner));
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            TMiddle1 mid1 = first.Parse(scanner, out match);

            if (!match.Success)
            {
                return default;
            }

            TMiddle2 mid2 = second.Parse(scanner, out Match<TIn> tempMatch);
            match += tempMatch;

            if (!match.Success)
            {
                return default;
            }

            TMiddle3 mid3 = third.Parse(scanner, out tempMatch);
            match += tempMatch;

            if (!match.Success)
            {
                return default;
            }

            return combiner(mid1, mid2, mid3);
        }
    }

    public sealed class CombineParser<TIn, TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut> : Parser<TIn, TOut>
    {
        private readonly Func<TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut> combiner;
        private readonly IParser<TIn, TMiddle1> first;
        private readonly IParser<TIn, TMiddle2> second;
        private readonly IParser<TIn, TMiddle3> third;
        private readonly IParser<TIn, TMiddle4> fourth;

        public CombineParser(
            IParser<TIn, TMiddle1> first,
            IParser<TIn, TMiddle2> second,
            IParser<TIn, TMiddle3> third,
            IParser<TIn, TMiddle4> fourth,
            Func<TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut> combiner)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
            this.third = third ?? throw new ArgumentNullException(nameof(third));
            this.fourth = fourth ?? throw new ArgumentNullException(nameof(fourth));
            this.combiner = combiner ?? throw new ArgumentNullException(nameof(combiner));
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            TMiddle1 mid1 = first.Parse(scanner, out match);

            if (!match.Success)
            {
                return default;
            }

            TMiddle2 mid2 = second.Parse(scanner, out Match<TIn> secondMatch);
            match += secondMatch;
            if (!match.Success)
            {
                return default;
            }

            TMiddle3 mid3 = third.Parse(scanner, out secondMatch);
            match += secondMatch;
            if (!match.Success)
            {
                return default;
            }

            TMiddle4 mid4 = fourth.Parse(scanner, out secondMatch);
            match += secondMatch;
            if (!match.Success)
            {
                return default;
            }

            return combiner(mid1, mid2, mid3, mid4);
        }
    }
}
