using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Nintenlord.Words
{
    public sealed class AsyncMorphicWord<T> : IWord<Task<T>>
    {
        readonly Func<T, Task<T[]>> morphism;
        readonly List<T> generatedWords = new List<T>();
        readonly List<T> cache = new List<T>();
        readonly SemaphoreSlim lockSemaphore = new SemaphoreSlim(1, 1);

        public AsyncMorphicWord(Func<T, Task<T[]>> morphism, T start)
        {
            this.morphism = morphism;
            generatedWords.Add(start);
        }

        public Task<T> this[BigInteger index] => GetValue(index);

        public BigInteger? Length => null;

        public async Task<T> GetValue(BigInteger index)
        {
            if (index >= generatedWords.Count)
            {
                await lockSemaphore.WaitAsync();

                try
                {
                    while (index >= generatedWords.Count)
                    {
                        foreach (var character in generatedWords)
                        {
                            cache.AddRange(await morphism(character));
                        }
                        generatedWords.AddRange(cache);
                        cache.Clear();
                    }
                }
                finally
                {
                    lockSemaphore.Release();
                }
            }
            return generatedWords[(int)index];
        }
    }
}
