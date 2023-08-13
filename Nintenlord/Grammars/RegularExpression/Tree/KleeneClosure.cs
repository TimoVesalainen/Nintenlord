using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{

    public sealed class KleeneClosure<TLetter> : IRegExExpressionTreeNode<TLetter>
    {
        private readonly IRegExExpressionTreeNode<TLetter> toRepeat;

        public KleeneClosure(IRegExExpressionTreeNode<TLetter> toRepeat)
        {
            this.toRepeat = toRepeat;
        }

        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.KleeneClosure;

        #endregion

        #region ITree<TLetter> Members

        public IEnumerable<IRegExExpressionTreeNode<TLetter>> GetChildren()
        {
            yield return toRepeat;
        }

        #endregion
    }
}
