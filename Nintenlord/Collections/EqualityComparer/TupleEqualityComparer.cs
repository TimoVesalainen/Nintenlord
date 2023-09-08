using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nintenlord.Collections.EqualityComparer
{
    public sealed class TupleEqualityComparer<T0, T1> : IEqualityComparer<(T0, T1)>
    {
        readonly IEqualityComparer<T0> comparer0;
        readonly IEqualityComparer<T1> comparer1;

        public static IEqualityComparer<(T0, T1)> Create(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1)
        {
            if (comparer0 ==  EqualityComparer<T0>.Default && comparer1 ==  EqualityComparer<T1>.Default)
            {
                return EqualityComparer<(T0, T1)>.Default;
            }
            return new TupleEqualityComparer<T0, T1>(comparer0, comparer1);
        }

        private TupleEqualityComparer(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1)
        {
            this.comparer0 = comparer0 ?? throw new ArgumentNullException(nameof(comparer0));
            this.comparer1 = comparer1 ?? throw new ArgumentNullException(nameof(comparer1));
        }

        public bool Equals((T0, T1) x, (T0, T1) y)
        {
            return comparer0.Equals(x.Item1, y.Item1) && comparer1.Equals(x.Item2, y.Item2);
        }

        public int GetHashCode([DisallowNull] (T0, T1) obj)
        {
            return HashCode.Combine(comparer0.GetHashCode(obj.Item1), comparer1.GetHashCode(obj.Item2));
        }
    }
    public sealed class TupleEqualityComparer<T0, T1, T2> : IEqualityComparer<(T0, T1, T2)>
    {
        readonly IEqualityComparer<T0> comparer0;
        readonly IEqualityComparer<T1> comparer1;
        readonly IEqualityComparer<T2> comparer2;

        public static IEqualityComparer<(T0, T1, T2)> Create(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2)
        {
            if (comparer0 ==  EqualityComparer<T0>.Default && comparer1 ==  EqualityComparer<T1>.Default && comparer2 ==  EqualityComparer<T2>.Default)
            {
                return EqualityComparer<(T0, T1, T2)>.Default;
            }
            return new TupleEqualityComparer<T0, T1, T2>(comparer0, comparer1, comparer2);
        }

        private TupleEqualityComparer(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2)
        {
            this.comparer0 = comparer0 ?? throw new ArgumentNullException(nameof(comparer0));
            this.comparer1 = comparer1 ?? throw new ArgumentNullException(nameof(comparer1));
            this.comparer2 = comparer2 ?? throw new ArgumentNullException(nameof(comparer2));
        }

        public bool Equals((T0, T1, T2) x, (T0, T1, T2) y)
        {
            return comparer0.Equals(x.Item1, y.Item1) && comparer1.Equals(x.Item2, y.Item2) && comparer2.Equals(x.Item3, y.Item3);
        }

        public int GetHashCode([DisallowNull] (T0, T1, T2) obj)
        {
            return HashCode.Combine(comparer0.GetHashCode(obj.Item1), comparer1.GetHashCode(obj.Item2), comparer2.GetHashCode(obj.Item3));
        }
    }
    public sealed class TupleEqualityComparer<T0, T1, T2, T3> : IEqualityComparer<(T0, T1, T2, T3)>
    {
        readonly IEqualityComparer<T0> comparer0;
        readonly IEqualityComparer<T1> comparer1;
        readonly IEqualityComparer<T2> comparer2;
        readonly IEqualityComparer<T3> comparer3;

        public static IEqualityComparer<(T0, T1, T2, T3)> Create(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3)
        {
            if (comparer0 ==  EqualityComparer<T0>.Default && comparer1 ==  EqualityComparer<T1>.Default && comparer2 ==  EqualityComparer<T2>.Default && comparer3 ==  EqualityComparer<T3>.Default)
            {
                return EqualityComparer<(T0, T1, T2, T3)>.Default;
            }
            return new TupleEqualityComparer<T0, T1, T2, T3>(comparer0, comparer1, comparer2, comparer3);
        }

        private TupleEqualityComparer(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3)
        {
            this.comparer0 = comparer0 ?? throw new ArgumentNullException(nameof(comparer0));
            this.comparer1 = comparer1 ?? throw new ArgumentNullException(nameof(comparer1));
            this.comparer2 = comparer2 ?? throw new ArgumentNullException(nameof(comparer2));
            this.comparer3 = comparer3 ?? throw new ArgumentNullException(nameof(comparer3));
        }

        public bool Equals((T0, T1, T2, T3) x, (T0, T1, T2, T3) y)
        {
            return comparer0.Equals(x.Item1, y.Item1) && comparer1.Equals(x.Item2, y.Item2) && comparer2.Equals(x.Item3, y.Item3) && comparer3.Equals(x.Item4, y.Item4);
        }

        public int GetHashCode([DisallowNull] (T0, T1, T2, T3) obj)
        {
            return HashCode.Combine(comparer0.GetHashCode(obj.Item1), comparer1.GetHashCode(obj.Item2), comparer2.GetHashCode(obj.Item3), comparer3.GetHashCode(obj.Item4));
        }
    }
    public sealed class TupleEqualityComparer<T0, T1, T2, T3, T4> : IEqualityComparer<(T0, T1, T2, T3, T4)>
    {
        readonly IEqualityComparer<T0> comparer0;
        readonly IEqualityComparer<T1> comparer1;
        readonly IEqualityComparer<T2> comparer2;
        readonly IEqualityComparer<T3> comparer3;
        readonly IEqualityComparer<T4> comparer4;

        public static IEqualityComparer<(T0, T1, T2, T3, T4)> Create(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3, IEqualityComparer<T4> comparer4)
        {
            if (comparer0 ==  EqualityComparer<T0>.Default && comparer1 ==  EqualityComparer<T1>.Default && comparer2 ==  EqualityComparer<T2>.Default && comparer3 ==  EqualityComparer<T3>.Default && comparer4 ==  EqualityComparer<T4>.Default)
            {
                return EqualityComparer<(T0, T1, T2, T3, T4)>.Default;
            }
            return new TupleEqualityComparer<T0, T1, T2, T3, T4>(comparer0, comparer1, comparer2, comparer3, comparer4);
        }

        private TupleEqualityComparer(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3, IEqualityComparer<T4> comparer4)
        {
            this.comparer0 = comparer0 ?? throw new ArgumentNullException(nameof(comparer0));
            this.comparer1 = comparer1 ?? throw new ArgumentNullException(nameof(comparer1));
            this.comparer2 = comparer2 ?? throw new ArgumentNullException(nameof(comparer2));
            this.comparer3 = comparer3 ?? throw new ArgumentNullException(nameof(comparer3));
            this.comparer4 = comparer4 ?? throw new ArgumentNullException(nameof(comparer4));
        }

        public bool Equals((T0, T1, T2, T3, T4) x, (T0, T1, T2, T3, T4) y)
        {
            return comparer0.Equals(x.Item1, y.Item1) && comparer1.Equals(x.Item2, y.Item2) && comparer2.Equals(x.Item3, y.Item3) && comparer3.Equals(x.Item4, y.Item4) && comparer4.Equals(x.Item5, y.Item5);
        }

        public int GetHashCode([DisallowNull] (T0, T1, T2, T3, T4) obj)
        {
            return HashCode.Combine(comparer0.GetHashCode(obj.Item1), comparer1.GetHashCode(obj.Item2), comparer2.GetHashCode(obj.Item3), comparer3.GetHashCode(obj.Item4), comparer4.GetHashCode(obj.Item5));
        }
    }
    public sealed class TupleEqualityComparer<T0, T1, T2, T3, T4, T5> : IEqualityComparer<(T0, T1, T2, T3, T4, T5)>
    {
        readonly IEqualityComparer<T0> comparer0;
        readonly IEqualityComparer<T1> comparer1;
        readonly IEqualityComparer<T2> comparer2;
        readonly IEqualityComparer<T3> comparer3;
        readonly IEqualityComparer<T4> comparer4;
        readonly IEqualityComparer<T5> comparer5;

        public static IEqualityComparer<(T0, T1, T2, T3, T4, T5)> Create(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3, IEqualityComparer<T4> comparer4, IEqualityComparer<T5> comparer5)
        {
            if (comparer0 ==  EqualityComparer<T0>.Default && comparer1 ==  EqualityComparer<T1>.Default && comparer2 ==  EqualityComparer<T2>.Default && comparer3 ==  EqualityComparer<T3>.Default && comparer4 ==  EqualityComparer<T4>.Default && comparer5 ==  EqualityComparer<T5>.Default)
            {
                return EqualityComparer<(T0, T1, T2, T3, T4, T5)>.Default;
            }
            return new TupleEqualityComparer<T0, T1, T2, T3, T4, T5>(comparer0, comparer1, comparer2, comparer3, comparer4, comparer5);
        }

        private TupleEqualityComparer(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3, IEqualityComparer<T4> comparer4, IEqualityComparer<T5> comparer5)
        {
            this.comparer0 = comparer0 ?? throw new ArgumentNullException(nameof(comparer0));
            this.comparer1 = comparer1 ?? throw new ArgumentNullException(nameof(comparer1));
            this.comparer2 = comparer2 ?? throw new ArgumentNullException(nameof(comparer2));
            this.comparer3 = comparer3 ?? throw new ArgumentNullException(nameof(comparer3));
            this.comparer4 = comparer4 ?? throw new ArgumentNullException(nameof(comparer4));
            this.comparer5 = comparer5 ?? throw new ArgumentNullException(nameof(comparer5));
        }

        public bool Equals((T0, T1, T2, T3, T4, T5) x, (T0, T1, T2, T3, T4, T5) y)
        {
            return comparer0.Equals(x.Item1, y.Item1) && comparer1.Equals(x.Item2, y.Item2) && comparer2.Equals(x.Item3, y.Item3) && comparer3.Equals(x.Item4, y.Item4) && comparer4.Equals(x.Item5, y.Item5) && comparer5.Equals(x.Item6, y.Item6);
        }

        public int GetHashCode([DisallowNull] (T0, T1, T2, T3, T4, T5) obj)
        {
            return HashCode.Combine(comparer0.GetHashCode(obj.Item1), comparer1.GetHashCode(obj.Item2), comparer2.GetHashCode(obj.Item3), comparer3.GetHashCode(obj.Item4), comparer4.GetHashCode(obj.Item5), comparer5.GetHashCode(obj.Item6));
        }
    }
    public sealed class TupleEqualityComparer<T0, T1, T2, T3, T4, T5, T6> : IEqualityComparer<(T0, T1, T2, T3, T4, T5, T6)>
    {
        readonly IEqualityComparer<T0> comparer0;
        readonly IEqualityComparer<T1> comparer1;
        readonly IEqualityComparer<T2> comparer2;
        readonly IEqualityComparer<T3> comparer3;
        readonly IEqualityComparer<T4> comparer4;
        readonly IEqualityComparer<T5> comparer5;
        readonly IEqualityComparer<T6> comparer6;

        public static IEqualityComparer<(T0, T1, T2, T3, T4, T5, T6)> Create(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3, IEqualityComparer<T4> comparer4, IEqualityComparer<T5> comparer5, IEqualityComparer<T6> comparer6)
        {
            if (comparer0 ==  EqualityComparer<T0>.Default && comparer1 ==  EqualityComparer<T1>.Default && comparer2 ==  EqualityComparer<T2>.Default && comparer3 ==  EqualityComparer<T3>.Default && comparer4 ==  EqualityComparer<T4>.Default && comparer5 ==  EqualityComparer<T5>.Default && comparer6 ==  EqualityComparer<T6>.Default)
            {
                return EqualityComparer<(T0, T1, T2, T3, T4, T5, T6)>.Default;
            }
            return new TupleEqualityComparer<T0, T1, T2, T3, T4, T5, T6>(comparer0, comparer1, comparer2, comparer3, comparer4, comparer5, comparer6);
        }

        private TupleEqualityComparer(IEqualityComparer<T0> comparer0, IEqualityComparer<T1> comparer1, IEqualityComparer<T2> comparer2, IEqualityComparer<T3> comparer3, IEqualityComparer<T4> comparer4, IEqualityComparer<T5> comparer5, IEqualityComparer<T6> comparer6)
        {
            this.comparer0 = comparer0 ?? throw new ArgumentNullException(nameof(comparer0));
            this.comparer1 = comparer1 ?? throw new ArgumentNullException(nameof(comparer1));
            this.comparer2 = comparer2 ?? throw new ArgumentNullException(nameof(comparer2));
            this.comparer3 = comparer3 ?? throw new ArgumentNullException(nameof(comparer3));
            this.comparer4 = comparer4 ?? throw new ArgumentNullException(nameof(comparer4));
            this.comparer5 = comparer5 ?? throw new ArgumentNullException(nameof(comparer5));
            this.comparer6 = comparer6 ?? throw new ArgumentNullException(nameof(comparer6));
        }

        public bool Equals((T0, T1, T2, T3, T4, T5, T6) x, (T0, T1, T2, T3, T4, T5, T6) y)
        {
            return comparer0.Equals(x.Item1, y.Item1) && comparer1.Equals(x.Item2, y.Item2) && comparer2.Equals(x.Item3, y.Item3) && comparer3.Equals(x.Item4, y.Item4) && comparer4.Equals(x.Item5, y.Item5) && comparer5.Equals(x.Item6, y.Item6) && comparer6.Equals(x.Item7, y.Item7);
        }

        public int GetHashCode([DisallowNull] (T0, T1, T2, T3, T4, T5, T6) obj)
        {
            return HashCode.Combine(comparer0.GetHashCode(obj.Item1), comparer1.GetHashCode(obj.Item2), comparer2.GetHashCode(obj.Item3), comparer3.GetHashCode(obj.Item4), comparer4.GetHashCode(obj.Item5), comparer5.GetHashCode(obj.Item6), comparer6.GetHashCode(obj.Item7));
        }
    }
}
