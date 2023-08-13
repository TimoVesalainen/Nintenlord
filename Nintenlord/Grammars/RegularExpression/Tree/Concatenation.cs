using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Concatenation<TLetter> : IRegExExpressionTreeNode<TLetter>
    {
        private readonly IRegExExpressionTreeNode<TLetter> first;
        private readonly IRegExExpressionTreeNode<TLetter> second;

        public Concatenation(
            IRegExExpressionTreeNode<TLetter> first,
            IRegExExpressionTreeNode<TLetter> second)
        {
            this.first = first;
            this.second = second;
        }

        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.Concatenation;

        #endregion

        #region ITree<IRegExExpressionTree<TLetter>> Members

        public IEnumerable<IRegExExpressionTreeNode<TLetter>> GetChildren()
        {
            yield return first;
            yield return second;
        }

        #endregion
    }
}
