namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Letter<TLetter> : IRegExExpressionNode<TLetter>
    {
        public readonly TLetter LetterToMatch;

        public Letter(TLetter letter)
        {
            this.LetterToMatch = letter;
        }

        public RegExNodeTypes Type => RegExNodeTypes.Letter;
    }
}
