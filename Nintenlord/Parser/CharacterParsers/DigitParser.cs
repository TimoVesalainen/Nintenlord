﻿using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.CharacterParsers
{
    public sealed class DigitParser : Parser<Char, Char>
    {
        protected override char ParseMain(IScanner<char> scanner, out Match<char> match)
        {
            char c = scanner.Current;
            if (Char.IsDigit(c))
            {
                match = new Match<char>(scanner, 1);
                scanner.MoveNext();
                return c;
            }
            else
            {
                match = new Match<char>(scanner, "Expected digit, got: " + c);
                return '\0';
            }
        }
    }
}
