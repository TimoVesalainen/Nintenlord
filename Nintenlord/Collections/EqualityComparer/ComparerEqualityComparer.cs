using System.Collections.Generic;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class ComparerEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly IComparer<T> baseComparer;
        private readonly T defaulValue;

        public ComparerEqualityComparer(IComparer<T> baseComparer, T defaulValue = default)
        {
            this.baseComparer = baseComparer;
            this.defaulValue = defaulValue;
        }

        public bool Equals(T x, T y)
        {
            return baseComparer.Compare(x, y) == 0;
        }

        public int GetHashCode(T obj)
        {
            return baseComparer.Compare(defaulValue, obj);
        }
    }
}
