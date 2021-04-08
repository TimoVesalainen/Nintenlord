// -----------------------------------------------------------------------
// <copyright file="RegExExpressionTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using Nintenlord.Collections.Trees;

namespace Nintenlord.Grammars.RegularExpression.Tree
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRegExExpressionTree<TLetter> : ITreeNode<IRegExExpressionTree<TLetter>>
    {
        RegExNodeTypes Type { get; }
    }
}
