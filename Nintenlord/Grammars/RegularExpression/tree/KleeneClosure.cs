// -----------------------------------------------------------------------
// <copyright file="KleeneClosure.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class KleeneClosure<TLetter> : IRegExExpressionTree<TLetter>
    {
        private readonly IRegExExpressionTree<TLetter> toRepeat;

        public KleeneClosure(IRegExExpressionTree<TLetter> toRepeat)
        {
            this.toRepeat = toRepeat;
        }

        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.KleeneClosure;

        #endregion

        #region ITree<TLetter> Members

        public IEnumerable<IRegExExpressionTree<TLetter>> GetChildren()
        {
            yield return toRepeat;
        }

        #endregion
    }
}
