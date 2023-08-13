using System.Collections.Generic;

namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class EmptyWord<TLetter> : IRegExExpressionNode<TLetter>
    {
        public static EmptyWord<TLetter> Instance = new();
        private EmptyWord() { }

        public RegExNodeTypes Type => RegExNodeTypes.EmptyWord;
    }
}
