// -----------------------------------------------------------------------
// <copyright file="Leaf.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Collections.Trees
{
    using System.Collections.Generic;

    public sealed class Leaf<T> : IValuedTree<T>
    {
        private readonly T value;

        public Leaf(T value)
        {
            this.value = value;
        }

        #region ITree<T> Members

        public IEnumerable<IValuedTree<T>> GetChildren()
        {
            yield break;
        }

        public T Value => value;

        public bool HasValue => true;

        #endregion
    }
}
