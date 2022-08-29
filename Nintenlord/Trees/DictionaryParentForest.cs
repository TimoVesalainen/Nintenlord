using System;
using System.Collections.Generic;

namespace Nintenlord.Trees
{
    public sealed class DictionaryParentForest<T> : IParentForest<T>
    {
        readonly IReadOnlyDictionary<T, T> relations;

        public DictionaryParentForest(IReadOnlyDictionary<T, T> relations)
        {
            this.relations = relations ?? throw new ArgumentNullException(nameof(relations));
        }

        public bool TryGetParent(T child, out T parent)
        {
            return relations.TryGetValue(child, out parent);
        }
    }
}
