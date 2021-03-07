﻿// -----------------------------------------------------------------------
// <copyright file="ExceptionHelpers.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Utility
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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
