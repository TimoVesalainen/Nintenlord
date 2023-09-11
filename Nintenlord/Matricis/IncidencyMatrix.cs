using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Matricis
{
    public sealed class IncidencyMatrix<T> : IMatrix<int>
    {
        readonly Func<T, IEnumerable<T>> morphism;
        readonly T[] values;
        readonly IEqualityComparer<T> comparer;

        public IncidencyMatrix(Func<T, IEnumerable<T>> morphism, IEnumerable<T> values, IEqualityComparer<T> comparer = null)
        {
            this.morphism = morphism ?? throw new ArgumentNullException(nameof(morphism));
            this.values = values?.ToArray() ?? throw new ArgumentNullException(nameof(values));
            this.comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public int this[int x, int y] => morphism(values[y]).Count(item => comparer.Equals(values[x], item));

        public int Width => values.Length;

        public int Height => values.Length;
    }
}
