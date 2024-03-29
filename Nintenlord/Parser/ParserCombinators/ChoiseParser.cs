﻿using Nintenlord.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class ChoiseParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly IParser<TIn, TOut>[] options;

        public ChoiseParser(IEnumerable<IParser<TIn, TOut>> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.options = options.ToArray();

            if (this.options.Any(x => x == null))
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            foreach (var item in options)
            {
                var result = item.Parse(scanner, out match);
                if (match.Success)
                {
                    return result;
                }
            }
            match = new Match<TIn>(scanner, "No match");
            return default;
        }

        public override string ToString()
        {
            return options.ToElementWiseString("|", "(", ")");
        }
    }
}
