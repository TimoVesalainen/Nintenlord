using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class ByLengthComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        readonly IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

        public static readonly ByLengthComparer<T> Instance = new();

        private ByLengthComparer()
        {
        }

        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            return comparer.Equals(x.Count(), y.Count());
        }

        public int GetHashCode([DisallowNull] IEnumerable<T> obj)
        {
            return obj.Count();
        }
    }
}
