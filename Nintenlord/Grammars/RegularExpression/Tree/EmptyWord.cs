using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class EmptyWord<TLetter> : IRegExExpressionTreeNode<TLetter>
    {
        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.EmptyWord;

        #endregion

        #region ITree<TLetter> Members

        public IEnumerable<IRegExExpressionTreeNode<TLetter>> GetChildren()
        {
            yield break;
        }

        #endregion
    }
}
