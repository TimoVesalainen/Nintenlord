using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.CharacterParsers
{
    class LetterParser : Parser<Char, Char>
    {
        protected override char ParseMain(IScanner<char> scanner, out Match<char> match)
        {
            char c = scanner.Current;
            if (Char.IsLetter(c))
            {
                match = new Match<char>(scanner, 1);
                scanner.MoveNext();
                return c;
            }
            else
            {
                match = new Match<char>(scanner, "Expected letter, got: " + c);
                return '\0';
            }
        }
    }
}
