using System;

namespace Nintenlord.Parser.CharacterParsers
{
    public sealed class CharacterParser : Parser<Char, Char>
    {
        private readonly char character;

        public CharacterParser(char c)
        {
            this.character = c;
        }

        protected override char ParseMain(IO.Scanners.IScanner<char> scanner, out Match<char> match)
        {
            char c = scanner.Current;
            if (c == character)
            {
                match = new Match<char>(scanner, 1);
                scanner.MoveNext();
                return c;
            }
            else
            {
                match = new Match<char>(scanner, "Expected {0}, got {1}", character, c);
                return '\0';
            }
        }
    }
}
