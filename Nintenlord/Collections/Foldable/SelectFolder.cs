using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public sealed class SelectFolder<TIn, TState, TOut1, TOut2> : IFolder<TIn, TState, TOut2>
    {
        readonly IFolder<TIn, TState, TOut1> original;
        readonly Func<TOut1, TOut2> selector;

        public SelectFolder(IFolder<TIn, TState, TOut1> original, Func<TOut1, TOut2> selector)
        {
            this.original = original ?? throw new ArgumentNullException(nameof(original));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TState Start => original.Start;

        public TState Fold(TState state, TIn input)
        {
            return original.Fold(state, input);
        }

        public TOut2 Transform(TState state)
        {
            return selector(original.Transform(state));
        }
    }
}
