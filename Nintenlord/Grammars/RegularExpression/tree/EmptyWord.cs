﻿// -----------------------------------------------------------------------
// <copyright file="EmptyWord.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class EmptyWord<TLetter> : IRegExExpressionTree<TLetter>
    {
        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type
        {
            get { return RegExNodeTypes.EmptyWord; }
        }

        #endregion

        #region ITree<TLetter> Members

        public IEnumerable<IRegExExpressionTree<TLetter>> GetChildren()
        {
            yield break;
        }

        #endregion
    }
}
