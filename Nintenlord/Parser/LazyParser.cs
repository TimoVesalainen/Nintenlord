﻿// -----------------------------------------------------------------------
// <copyright file="LazyParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Parser
{
    using Nintenlord.IO.Scanners;
    using System;

    /// <summary>
    /// Parser that's initialized only when it is used for actual parsing.
    /// </summary>
    public sealed class LazyParser<TIn, TOut> : Parser<TIn, TOut>
    {
        private readonly Lazy<IParser<TIn, TOut>> mainParser;

        public LazyParser(Lazy<IParser<TIn, TOut>> mainParser)
        {
            this.mainParser = mainParser;
        }

        public LazyParser(Func<IParser<TIn, TOut>> parserFactory)
        {
            this.mainParser = new Lazy<IParser<TIn, TOut>>(parserFactory);
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            return mainParser.Value.Parse(scanner, out match);
        }
    }
}
