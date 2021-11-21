using Nintenlord.Collections.Lists;
using Nintenlord.Trees;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nintenlord.Collections.Lists.Matching;

namespace Nintenlord.Tests.Collections.Lists
{
    public class MatchingTests
    {
        private ITree<Match> AlingSequencesInner<T>(IReadOnlyList<T> sequence1, IReadOnlyList<T> sequence2)
        {
            static int Replace(T first, T second)
            {
                if (EqualityComparer<T>.Default.Equals(first, second))
                {
                    return 1;
                }
                else
                {
                    return -10;
                }
            }

            return AlingSequences(sequence1, sequence2, _ => -10, _ => -10, Replace);
        }

        private void TestContinous(IEnumerable<Match> matches, int length1, int length2)
        {
            int seq1 = 0;
            int seq2 = 0;

            foreach (var item in matches)
            {
                if (seq1 != item.Start1)
                {
                    Assert.Fail();
                }
                if (seq2 != item.Start2)
                {
                    Assert.Fail();
                }
                seq1 += item.Length1;
                seq2 += item.Length2;
            }
            if (seq1 != length1)
            {
                Assert.Fail();
            }
            if (seq2 != length2)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestMatchingSame()
        {
            var s1 = "a".ToList();
            var s2 = "a".ToList();

            var leaves = AlingSequencesInner(s1, s2).GetLeaves().ToList();

            TestContinous(leaves, s1.Count, s2.Count);
            Assert.AreEqual(new[] { new Match(0, 1, 0, 1) }, leaves);
        }

        [Test]
        public void TestMatchingDifferent()
        {
            var s1 = "a".ToList();
            var s2 = "b".ToList();

            var leaves = AlingSequencesInner(s1, s2).GetLeaves().ToList();

            TestContinous(leaves, s1.Count, s2.Count);
            Assert.AreEqual(new[] { new Match(0, 1, 0, 1) }, leaves);
        }

        [TestCase(1, 1, 1)]
        [TestCase(1, 10, 1)]
        [TestCase(10, 1, 10)]
        public void TestMatchingInserted(int start, int middle, int end)
        {
            var s1 = Enumerable.Repeat('a', start).Concat(Enumerable.Repeat('b', end)).ToList();
            var s2 = Enumerable.Repeat('a', start).Concat(Enumerable.Repeat('c', middle)).Concat(Enumerable.Repeat('b', end)).ToList();

            var tree = AlingSequencesInner(s1, s2);
            var leaves = tree.GetLeaves().ToList();

            TestContinous(leaves, s1.Count, s2.Count);

            var expected = Enumerable.Range(0, start).Select(i => new Match(i, 1, i, 1))
                .Append(new Match(start, 0, start, middle))
                .Concat(Enumerable.Range(start, end).Select(i => new Match(i, 1, i + middle, 1)));

            Assert.AreEqual(expected, leaves);
        }

        [Test]
        public void MatchExample()
        {
            //https://en.wikipedia.org/wiki/Hirschberg%27s_algorithm#Example
            var s1 = "AGTACGCA".ToList();
            var s2 = "TATGC".ToList();

            var result = s1.MatchSequences(s2, '-',
                _ => -2,
                _ => -2,
                (x, y) => x == y ? 2 : -1);

            Assert.AreEqual(new[]
            {
                ('A', '-'),
                ('G', '-'),
                ('T', 'T'),
                ('A', 'A'),
                ('C', 'T'),
                ('G', 'G'),
                ('C', 'C'),
                ('A', '-'),
            }, result);
        }
    }
}
