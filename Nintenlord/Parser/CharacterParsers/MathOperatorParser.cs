﻿using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.CharacterParsers
{
    internal class MathOperatorParser : Parser<Char, Char>
    {
        protected override char ParseMain(IScanner<char> scanner, out Match<char> match)
        {
            char c = scanner.Current;
            switch (c)
            {
                case '*':
                case '/':
                case '+':
                case '-':
                case '|':
                case '&':
                case '^':
                case '~':
                    scanner.MoveNext();
                    match = new Match<char>(scanner, 1);
                    return c;
                default:
                    match = new Match<char>(scanner, "Expected math operator, got: " + c);
                    return '\0';
            }

            //if (Char.IsWhiteSpace(c))
            //{
            //    scanner.Advance();
            //    match = new Match<char>(scanner, scanner.Offset, 1);
            //    return c;
            //}
            //else
            //{
            //    match = new Match<char>(scanner, "Expected whitespace, got: " + c);
            //    return '\0';
            //}
        }
    }
}
