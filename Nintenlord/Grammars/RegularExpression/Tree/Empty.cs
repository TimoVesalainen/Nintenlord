namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Empty<TLetter> : IRegExExpressionNode<TLetter>
    {
        public static readonly Empty<TLetter> Instance = new();

        private Empty() { }

        public RegExNodeTypes Type => RegExNodeTypes.Empty;
    }
}
