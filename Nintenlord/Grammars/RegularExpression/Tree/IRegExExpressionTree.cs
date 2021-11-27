using Nintenlord.Trees.Nodes;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public interface IRegExExpressionTree<TLetter> : ITreeNode<IRegExExpressionTree<TLetter>>
    {
        RegExNodeTypes Type { get; }
    }
}
