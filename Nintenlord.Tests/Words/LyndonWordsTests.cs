using Nintenlord.Words;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Tests.Words
{
    public class LyndonWordsTests
    {
        [Test]
        public void TestLength4()
        {
            Assert.AreEqual(new int[][] { 
                new int[] { 0 },
                new int[] { 0,0,0,1 },
                new int[] { 0,0,1 },
                new int[] { 0,0,1,1 },
                new int[] { 0,1 },
                new int[] { 0,1,1 },
                new int[] { 0,1,1,1 },
                new int[] { 1 } 
            },
                LyndonWords.GetLyndonWords(2, 4));
        }

        [TestCase(1,2)]
        [TestCase(2,19)]
        [TestCase(3,11)]
        [TestCase(4,7)]
        [TestCase(5,4)]
        [TestCase(6,2)]
        public void TestFirstLast(int length, int alphabetSize)
        {
            var words = LyndonWords.GetLyndonWords(alphabetSize, length).ToArray();

            Assert.AreEqual(new int[] { 0 }, words.First());
            Assert.AreEqual(new int[] { alphabetSize - 1 }, words.Last());
        }

        [Test]
        public void TestDecomposition() 
        {
            var word = new[] { 0, 1, 1, 0, 1, 0, 0, 1,0,1,1,1,0,0,0,0 };

            var lyndonWords = LyndonWords.GetLyndonWords(2, word.Length).ToArray();

            foreach (var item in LyndonWords.GetLyndonComposition(word))
            {
                var itemArray = item.ToArray();
                Assert.IsTrue(lyndonWords.Any(lWord =>
                {
                    if (lWord.Length != itemArray.Length)
                    {
                        return false;
                    }
                    for (int i = 0; i < lWord.Length; i++)
                    {
                        if (itemArray[i] != lWord[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }), $"Expected {string.Join(",", itemArray)} to be Lyndon word");
            }
        }
    }
}
