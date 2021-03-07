// -----------------------------------------------------------------------
// <copyright file="RegExExpressionTree.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    using Nintenlord.Collections.Trees;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRegExExpressionTree<TLetter> : ITree<IRegExExpressionTree<TLetter>>
    {
        RegExNodeTypes Type { get; }
    }
}
