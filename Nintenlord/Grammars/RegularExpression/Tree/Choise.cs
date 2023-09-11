﻿namespace Nintenlord.Grammars.RegularExpression.Tree
{
    public sealed class Choise<TLetter> : IRegExExpressionNode<TLetter>
    {
        public static readonly Choise<TLetter> Instance = new Choise<TLetter>();

        private Choise()
        {
        }

        public RegExNodeTypes Type => RegExNodeTypes.Choise;
    }
}
