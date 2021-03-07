using System;

namespace Nintenlord.Utility
{
    public static class TypeHelpers
    {
        public static bool InheritsFrom(this Type type, Type inheritsFrom)
        {
            while (type != null)
            {
                if (type == inheritsFrom)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

    }
}
