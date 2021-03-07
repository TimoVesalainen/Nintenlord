namespace Nintenlord.Utility.Primitives
{
    public static class CharacterExtensions
    {
        public static bool IsHexDigit(this char c)
        {
            if (char.IsNumber(c))
            {
                return true;
            }
            switch (c)
            {
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return true;
                default:
                    return false;
            }
        }
    }
}
