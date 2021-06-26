using System;
using System.Collections.Generic;

namespace Nintenlord.Matricis
{
    public sealed class MultiplicationMatrix<TIn1, TIn2, TOut> : IMatrix<TOut>
    {
        readonly IMatrix<TIn1> first;
        readonly IMatrix<TIn2> second;
        readonly Func<TIn1, TIn2, TOut> multiplication;
        readonly Func<IEnumerable<TOut>, TOut> sum;

        public MultiplicationMatrix(IMatrix<TIn1> first, IMatrix<TIn2> second, Func<TIn1, TIn2, TOut> multiplication, Func<IEnumerable<TOut>, TOut> sum)
        {
            if (first.Width != second.Height)
            {
                throw new ArgumentException("Invalid size matricis", nameof(second));
            }
            this.first = first;
            this.second = second;
            this.multiplication = multiplication;
            this.sum = sum;
        }

        public TOut this[int x, int y] => GetElement(x, y);

        public int Width => second.Width;

        public int Height => first.Height;

        private TOut GetElement(int x, int y)
        {
            IEnumerable<TOut> GetMults()
            {
                for (int i = 0; i < first.Width; i++)
                {
                    yield return multiplication(first[i, y], second[x, i]);
                }
            }

            return sum(GetMults());
        }
    }
}