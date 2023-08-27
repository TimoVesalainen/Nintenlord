using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.EqualityComparer
{
    // TODO: Templatify
    public sealed class TupleEqualityComparer<T1, T2> : IEqualityComparer<(T1, T2)>
    {
        readonly IEqualityComparer<T1> first;
        readonly IEqualityComparer<T2> second;

        public static IEqualityComparer<(T1, T2)> Create(IEqualityComparer<T1> first, IEqualityComparer<T2> second)
        {
            if (first == EqualityComparer<T1>.Default && second == EqualityComparer<T2>.Default)
            {
                return EqualityComparer<(T1, T2)>.Default;
            }
            return new TupleEqualityComparer<T1, T2>(first, second);
        }

        private TupleEqualityComparer(IEqualityComparer<T1> first, IEqualityComparer<T2> second)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public bool Equals((T1, T2) x, (T1, T2) y)
        {
            return first.Equals(x.Item1, y.Item1) && second.Equals(x.Item2, y.Item2);
        }

        public int GetHashCode([DisallowNull] (T1, T2) obj)
        {
            return HashCode.Combine(first.GetHashCode(obj.Item1), second.GetHashCode(obj.Item2));
        }
    }
}
