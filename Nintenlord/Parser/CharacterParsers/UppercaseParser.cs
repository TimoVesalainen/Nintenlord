using Nintenlord.IO.Scanners;
using System;

namespace Nintenlord.Parser.CharacterParsers
{
    internal class UppercaseParser : Parser<Char, Char>
    {
        protected override char ParseMain(IScanner<char> scanner, out Match<char> match)
        {
            char c = scanner.Current;
            if (Char.IsUpper(c))
            {
                scanner.MoveNext();
                match = new Match<char>(scanner, 1);
                return c;
            }
            else
            {
                match = new Match<char>(scanner, "Expected uppercase, got: " + c);
                return '\0';
            }
        }
    }
}
