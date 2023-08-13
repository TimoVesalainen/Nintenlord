using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Choise<TLetter> : IRegExExpressionTreeNode<TLetter>
    {
        private readonly IRegExExpressionTreeNode<TLetter> firstChoise;
        private readonly IRegExExpressionTreeNode<TLetter> secondChoise;

        public Choise(
            IRegExExpressionTreeNode<TLetter> firstChoise,
            IRegExExpressionTreeNode<TLetter> secondChoise)
        {
            this.firstChoise = firstChoise;
            this.secondChoise = secondChoise;
        }

        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.Choise;

        #endregion

        #region ITree<IRegExExpressionTree<TLetter>> Members

        public IEnumerable<IRegExExpressionTreeNode<TLetter>> GetChildren()
        {
            yield return firstChoise;
            yield return secondChoise;
        }

        #endregion
    }
}
