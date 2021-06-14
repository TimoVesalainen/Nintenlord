using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Collections.Comparers
{
    public sealed class AlphabeticComparer<T> : IComparer<IEnumerable<T>>
    {
        readonly LexicographicComparer<T> lexicographicComparer;

        public AlphabeticComparer(IComparer<T> ordering)
        {
            lexicographicComparer = new LexicographicComparer<T>(ordering);
        }

        public int Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
            var xLength = x.Count();
            var yLength = y.Count();

            if (xLength < yLength)
            {
                return -1;
            }
            else if (xLength > yLength)
            {
                return 1;
            }
            else
            {
                return lexicographicComparer.Compare(x, y);
            }
        }
    }
}
