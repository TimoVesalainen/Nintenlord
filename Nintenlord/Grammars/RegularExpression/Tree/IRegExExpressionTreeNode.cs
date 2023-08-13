using Nintenlord.Trees.Nodes;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public interface IRegExExpressionTreeNode<TLetter> : ITreeNode<IRegExExpressionTreeNode<TLetter>>
    {
        RegExNodeTypes Type { get; }
    }
}
