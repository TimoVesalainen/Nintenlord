using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    public sealed class AddRootTree<T> : ITree<T>
    {
        readonly IForest<T> originalForest;
        readonly T newNode;
        readonly IEqualityComparer<T> nodeComparer;
        readonly T[] rootChildren;

        public AddRootTree(IForest<T> originalForest, T newNode, IEnumerable<T> rootChildren, IEqualityComparer<T> nodeComparer = null)
        {
            this.originalForest = originalForest ?? throw new ArgumentNullException(nameof(originalForest));
            this.newNode = newNode;
            this.nodeComparer = nodeComparer ?? EqualityComparer<T>.Default;
            this.rootChildren = rootChildren?.ToArray() ?? throw new ArgumentNullException(nameof(rootChildren));
        }

        public T Root => newNode;

        public IEnumerable<T> GetChildren(T node)
        {
            if (nodeComparer.Equals(node, newNode))
            {
                return rootChildren;
            }
            else
            {
                return originalForest.GetChildren(node);
            }
        }
    }
}
