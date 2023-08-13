namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public interface IRegExExpressionNode<TLetter>
    {
        RegExNodeTypes Type { get; }
    }

}
