using System.Collections.Generic;

namespace Nintenlord.Algebra
{
    public interface IGroup<T>
    {
        IEnumerable<T> GetItems();

        T Inverse(T item);

        T Operation(T left, T right);
    }
}
