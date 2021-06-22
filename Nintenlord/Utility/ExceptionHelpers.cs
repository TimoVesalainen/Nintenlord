using System;

namespace Nintenlord.Utility
{
    public static class ExceptionHelpers
    {
        public static Exception GetInmost(this Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            return e;
        }
    }
}
