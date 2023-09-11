namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class KleeneClosure<TLetter> : IRegExExpressionNode<TLetter>
    {
        public static readonly KleeneClosure<TLetter> Instance = new();

        private KleeneClosure()
        {

        }


        public RegExNodeTypes Type => RegExNodeTypes.KleeneClosure;
    }
}
