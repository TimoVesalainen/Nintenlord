using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{

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
