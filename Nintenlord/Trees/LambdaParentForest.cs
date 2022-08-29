using System;

namespace Nintenlord.Trees
{
    public sealed class LambdaParentForest<T> : IParentForest<T>
    {
        private readonly Func<T, (bool hasParent, T parent)> tryGetParent;

        public LambdaParentForest(Func<T, (bool, T)> tryGetParent)
        {
            this.tryGetParent = tryGetParent;
        }

        public bool TryGetParent(T child, out T parent)
        {
            var t = tryGetParent(child);

            parent = t.parent;
            return t.hasParent;
        }
    }
}
