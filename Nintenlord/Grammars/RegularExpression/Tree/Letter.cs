using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Letter<TLetter> : IRegExExpressionTreeNode<TLetter>
    {
        public readonly TLetter LetterToMatch;

        public Letter(TLetter letter)
        {
            this.LetterToMatch = letter;
        }

        #region IRegExExpressionTree<T> Members

        public RegExNodeTypes Type => RegExNodeTypes.Letter;

        #endregion

        #region ITree<TLetter> Members

        public IEnumerable<IRegExExpressionTreeNode<TLetter>> GetChildren()
        {
            yield break;
        }

        #endregion
    }
}
