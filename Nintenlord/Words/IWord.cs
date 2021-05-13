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
    }
}
