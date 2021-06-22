using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
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
