using System.Collections.Generic;

namespace Nintenlord.Trees.Nodes
{
    public static class TreeNodeHelpers
    {
        public static IEnumerable<T> DepthFirstEnumerator<T, TChild>(this IValuedTreeNode<T, TChild> tree)
            where TChild : IValuedTreeNode<T, TChild>
        {
            foreach (var child in tree.GetChildren())
            {
                foreach (var value in child.DepthFirstEnumerator())
                {
                    yield return value.Value;
                }
            }
            if (tree.HasValue)
            {
                yield return tree.Value;
            }
        }

        public static IEnumerable<T> BreadthFirstEnumerator<T, TChild>(this IValuedTreeNode<T, TChild> tree)
            where TChild : IValuedTreeNode<T, TChild>
        {
            if (tree.HasValue)
            {
                yield return tree.Value;
            }
            foreach (var child in tree.GetChildren())
            {
                foreach (var value in child.BreadthFirstEnumerator())
                {
                    yield return value.Value;
                }
            }
        }

        public static IEnumerable<T> NthEnumerator<T, TChild>(this IValuedTreeNode<T, TChild> tree, int position)
            where TChild : IValuedTreeNode<T, TChild>
        {
            foreach (var child in tree.GetChildren())
            {
                if (position == 0 && tree.HasValue)
                {
                    yield return tree.Value;
                }
                foreach (var value in child.BreadthFirstEnumerator())
                {
                    yield return value.Value;
                }
                position--;
            }

            if (position >= 0 && tree.HasValue)
            {
                yield return tree.Value;
            }
        }


        public static IEnumerable<T> DepthFirstEnumerator<T>(this T tree)
            where T : ITreeNode<T>
        {
            foreach (var child in tree.GetChildren())
            {
                foreach (var value in child.DepthFirstEnumerator())
                {
                    yield return value;
                }
            }
            yield return tree;
        }

        public static IEnumerable<T> BreadthFirstEnumerator<T>(this T tree)
            where T : ITreeNode<T>
        {
            yield return tree;
            foreach (var child in tree.GetChildren())
            {
                foreach (var value in child.BreadthFirstEnumerator())
                {
                    yield return value;
                }
            }
        }

        public static IEnumerable<T> NthEnumerator<T>(this T tree, int position)
            where T : ITreeNode<T>
        {
            foreach (var child in tree.GetChildren())
            {
                if (position == 0)
                {
                    yield return tree;
                }
                foreach (var value in child.BreadthFirstEnumerator())
                {
                    yield return value;
                }
                position--;
            }

            if (position >= 0)
            {
                yield return tree;
            }
        }
    }
}
