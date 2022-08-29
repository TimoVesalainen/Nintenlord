using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Trees
{
    static public class ParentForest
    {
        public static IEnumerable<ITree<T>> ToTrees<T>(this IParentForest<T> tree, IEnumerable<T> leafs, IEqualityComparer<T> comparer = null)
        {
            if (tree is null)
            {
                throw new ArgumentNullException(nameof(tree));
            }

            if (leafs is null)
            {
                throw new ArgumentNullException(nameof(leafs));
            }

            if (!leafs.Any())
            {
                throw new ArgumentException("Leaf collection is empty", nameof(leafs));
            }

            comparer ??= EqualityComparer<T>.Default;

            var children = new Dictionary<T, HashSet<T>>(comparer);

            if (leafs.TryGetNonEnumeratedCount(out var leafCount))
            {
                leafCount = 4; //General default capacity in .Net
            }

            var queue = new Queue<T>(leafCount);
            foreach (var item in leafs)
            {
                queue.Enqueue(item);
            }
            var roots = new HashSet<T>(comparer);
            do
            {
                var item = queue.Dequeue();

                if (tree.TryGetParent(item, out var parent))
                {
                    HashSet<T> parentChildren;
                    if (!children.TryGetValue(parent, out parentChildren))
                    {
                        parentChildren = new HashSet<T>(comparer);
                        children[parent] = parentChildren;
                        queue.Enqueue(parent);
                    }
                    parentChildren.Add(item);
                }
                else
                {
                    roots.Add(item);
                }
            }
            while (queue.Count > 0);

            return roots.Select(root => new LambdaTree<T>(root, node => children[node]));
        }
    }
}
