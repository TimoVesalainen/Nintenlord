using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Concatenation<TLetter> : IRegExExpressionNode<TLetter>
    {
        public readonly static Concatenation<TLetter> Instance = new();

        public Concatenation()
        {
        }

        public RegExNodeTypes Type => RegExNodeTypes.Concatenation;
    }
}
