using Nintenlord.Collections;
using Nintenlord.Distributions;
using Nintenlord.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nintenlord.Numerics
{
    internal sealed class BinomialCoefficientRowEnumerable<TNumber> : IEnumerable<IEnumerable<TNumber>>
        where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>,
        IMultiplicativeIdentity<TNumber, TNumber>
    {
        public static readonly BinomialCoefficientRowEnumerable<TNumber> Instance = new();

        private BinomialCoefficientRowEnumerable()
        {

        }

        private struct Enumerator : IEnumerator<IEnumerable<TNumber>>
        {
            List<TNumber> cache1 = new();
            List<TNumber> cache2 = new();

            public Enumerator()
            {
            }

            public readonly IEnumerable<TNumber> Current => cache1;

            readonly object IEnumerator.Current => Current;

            public void Dispose()
            {
                cache1 = null;
                cache2 = null;
            }

            public bool MoveNext()
            {
                cache2.Clear();
                cache2.Add(TNumber.MultiplicativeIdentity);
                cache2.AddRange(cache1.GetSequentialPairs().Select(t => t.current + t.next));
                cache2.Add(TNumber.MultiplicativeIdentity);
                (cache2, cache1) = (cache1, cache2);
                return true;
            }

            public readonly void Reset()
            {
                cache1.Clear();
                cache2.Clear();
            }
        }

        public IEnumerator<IEnumerable<TNumber>> GetEnumerator()
        {
            return new Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
