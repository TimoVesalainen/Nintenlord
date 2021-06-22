using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Concatenation<TLetter> : IRegExExpressionTree<TLetter>
    {
        private readonly IRegExExpressionTree<TLetter> first;
        private readonly IRegExExpressionTree<TLetter> second;

        public Concatenation(
            IRegExExpressionTree<TLetter> first,
            IRegExExpressionTree<TLetter> second)
        {
            this.first = first;
            this.second = second;
        }

        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.Concatenation;

        #endregion

        #region ITree<IRegExExpressionTree<TLetter>> Members

        public IEnumerable<IRegExExpressionTree<TLetter>> GetChildren()
        {
            yield return first;
            yield return second;
        }

        #endregion
    }
}
