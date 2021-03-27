// -----------------------------------------------------------------------
// <copyright file="Choise.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Choise<TLetter> : IRegExExpressionTree<TLetter>
    {
        private readonly IRegExExpressionTree<TLetter> firstChoise;
        private readonly IRegExExpressionTree<TLetter> secondChoise;

        public Choise(
            IRegExExpressionTree<TLetter> firstChoise,
            IRegExExpressionTree<TLetter> secondChoise)
        {
            this.firstChoise = firstChoise;
            this.secondChoise = secondChoise;
        }

        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.Choise;

        #endregion

        #region ITree<IRegExExpressionTree<TLetter>> Members

        public IEnumerable<IRegExExpressionTree<TLetter>> GetChildren()
        {
            yield return firstChoise;
            yield return secondChoise;
        }

        #endregion
    }
}
