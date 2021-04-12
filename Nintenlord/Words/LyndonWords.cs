using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Words
{
    public static class LyndonWords
    {
        public static IEnumerable<int[]> GetLyndonWords(int alphabetSize, int maxLength)
        {
            List<int> currentWord = new List<int>(maxLength);
            currentWord.Add(0);
            yield return currentWord.ToArray();

            var finalAlphabet = alphabetSize - 1;
            while (currentWord[0] != finalAlphabet)
            {
                var currentLength = currentWord.Count;
                for (int i = currentLength; i < maxLength; i++)
                {
                    currentWord.Add(currentWord[i % currentLength]);
                }
                while (currentWord[currentWord.Count - 1] == finalAlphabet)
                {
                    currentWord.RemoveAt(currentWord.Count - 1);
                }
                currentWord[currentWord.Count - 1] = currentWord[currentWord.Count - 1] + 1;
                yield return currentWord.ToArray();
            }
        }

        /// <returns>Non-increasing sequence of Lyndon words</returns>
        public static IEnumerable<IEnumerable<int>> GetLyndonDecomposition(IList<int> word)
        {
            int k = 0;
            int m = 1;
            int used = 0;
            IEnumerable<int> GetLyndonWord(int length)
            {
                for (int i = 0; i < length; i++)
                {
                    yield return word[i + used];
                }
            }

            while (m < word.Count && k < word.Count)
            {
                if (word[k] == word[m])
                {
                    k++;
                    m++;
                }
                else if (word[k] < word[m])
                {
                    m++;
                    k = used;
                }
                else //word[k] > word[m]
                {
                    var length = m - k;
                    yield return GetLyndonWord(length);

                    used += length;
                    m = used + 1;
                    k = used;
                }
                if (m == word.Count)
                {
                    var length = m - k;
                    yield return GetLyndonWord(length);

                    used += length;
                    m = used + 1;
                    k = used;
                }
            }
            yield return GetLyndonWord(word.Count - used);
        }
    }
}
