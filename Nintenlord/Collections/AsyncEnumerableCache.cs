using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nintenlord.Collections
{
    /// <summary>
    /// Caches values from potentially infinite async enumerable
    /// </summary>
    public sealed class AsyncEnumerableCache<T> : IAsyncDisposable
    {
        readonly List<T> cachedItems = new();
        readonly IAsyncEnumerator<T> enumerator;
        readonly SemaphoreSlim semaphore = new(1, 1);

        public Task<T> this[int index]
        {
            get
            {
                if (index <= cachedItems.Count)
                {
                    async Task<T> CacheAndReturn()
                    {
                        await semaphore.WaitAsync();
                        await CacheValues(index);
                        semaphore.Release();

                        return cachedItems[index];
                    }

                    return CacheAndReturn();
                }
                else
                {
                    return Task.FromResult(cachedItems[index]);
                }

            }
        }

        async Task CacheValues(int toIndex)
        {
            while (toIndex <= cachedItems.Count)
            {
                if (await enumerator.MoveNextAsync())
                {
                    cachedItems.Add(enumerator.Current);
                }
                else
                {
                    throw new InvalidOperationException("Enumerator doesn't have enough items");
                }
            }
        }

        public AsyncEnumerableCache(IAsyncEnumerable<T> items)
        {
            enumerator = items.GetAsyncEnumerator();
        }

        public async ValueTask DisposeAsync()
        {
            await enumerator.DisposeAsync();
        }
    }
}
