using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nintenlord.Words
{
    public interface IWord<TCharacter>
    {
        TCharacter this[BigInteger index] { get; }

        /// <summary>
        /// Length of the word for finite word, null for infinite word
        /// </summary>
        BigInteger? Length { get; }
    }

    public static class WordHelpers
    {
        public static IEnumerable<TCharacter> EnumerateCharacters<TCharacter>(this IWord<TCharacter> word)
        {
            if (word.Length is BigInteger length)
            {
                for (BigInteger index = 0; index < length; index++)
                {
                    yield return word[index];
                }
            }
            else
            {
                BigInteger index = 0;
                while (true)
                {
                    yield return word[index];
                    index++;
                }
            }
        }

        public static bool IsFinite<TCharacter>(this IWord<TCharacter> word) => word.Length != null;
        public static bool IsInfinite<TCharacter>(this IWord<TCharacter> word) => word.Length == null;

        public static TCharacter[] ToArray<TCharacter>(this IWord<TCharacter> word)
        {
            if (word is null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            if (word.Length is null)
            {
                throw new ArgumentException(nameof(word), "Can't put infinite word to array");
            }
            if (word.Length > int.MaxValue)
            {
                throw new ArgumentException(nameof(word), $"Length larger than int.MaxValue: {word.Length}");
            }

            var array = new TCharacter[(int)word.Length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = word[i];
            }

            return array;
        }

        public static TCharacter[] GetPrefix<TCharacter>(this IWord<TCharacter> word, int maxLength)
        {
            if (word is null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            TCharacter[] array = new TCharacter[
                word.Length != null && maxLength > word.Length
                ? (int)word.Length
                : maxLength];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = word[i];
            }

            return array;
        }
    }
}
