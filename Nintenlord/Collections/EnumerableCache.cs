using System;
using System.Collections.Generic;

namespace Nintenlord.Collections
{
    /// <summary>
    /// Caches values from potentially infinite enumerable
    /// </summary>
    public sealed class EnumerableCache<T> : IDisposable
    {
        readonly List<T> cachedItems = new();
        readonly IEnumerator<T> enumerator;

        private bool disposedValue;

        public T this[int index]
        {
            get
            {
                while (index <= cachedItems.Count)
                {
                    if (enumerator.MoveNext())
                    {
                        cachedItems.Add(enumerator.Current);
                    }
                    else
                    {
                        throw new InvalidOperationException("Enumerator doesn't have enough items");
                    }
                }
                return cachedItems[index];
            }
        }

        public EnumerableCache(IEnumerable<T> items)
        {
            enumerator = items.GetEnumerator();
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    enumerator.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
