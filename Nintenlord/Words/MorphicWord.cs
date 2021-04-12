using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Words
{
    public sealed class MorphicWord<T> : IWord<T>
    {
        readonly Func<T, T[]> morphism;
        readonly List<T> generatedWords = new List<T>();
        readonly List<T> cache = new List<T>();

        public MorphicWord(Func<T, T[]> morphism, T start)
        {
            var startResult = morphism(start);
            if (startResult.Length < 2 || Equals(startResult[0], start))
            {
                throw new ArgumentException(nameof(morphism));
            }

            this.morphism = morphism;
            generatedWords.AddRange(startResult);
        }

        public T this[BigInteger index] => GetValue(index);

        public BigInteger? Length => null;

        public T GetValue(BigInteger index)
        {
            while (index >= generatedWords.Count)
            {
                foreach (var character in generatedWords)
                {
                    cache.AddRange(morphism(character));
                }
                generatedWords.AddRange(cache);
                cache.Clear();
            }
            return generatedWords[(int)index];
        }
    }
}
