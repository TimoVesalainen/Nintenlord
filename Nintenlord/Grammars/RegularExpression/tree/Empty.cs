using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Empty<TLetter> : IRegExExpressionTree<TLetter>
    {
        #region IRegExExpressionTree<TLetter> Members

        public RegExNodeTypes Type => RegExNodeTypes.Empty;

        #endregion

        #region ITree<TLetter> Members

        public IEnumerable<IRegExExpressionTree<TLetter>> GetChildren()
        {
            yield break;
        }

        #endregion
    }
}
