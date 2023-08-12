using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public sealed class FunctionFolder<TIn, TState, TOut> : IFolder<TIn, TState, TOut>
    {
        readonly Func<TState, TIn, TState> folder;
        readonly Func<TState, TOut> transform;

        public FunctionFolder(TState start, Func<TState, TIn, TState> folder, Func<TState, TOut> transform)
        {
            this.folder = folder ?? throw new ArgumentNullException(nameof(folder));
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
            Start = start;
        }

        public TState Start { get; }

        public TState Fold(TState state, TIn input)
        {
            return folder(state, input);
        }

        public TOut Transform(TState state)
        {
            return transform(state);
        }
    }
}
