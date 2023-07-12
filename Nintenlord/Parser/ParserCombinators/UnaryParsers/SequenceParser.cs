using Nintenlord.IO.Scanners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Parser.ParserCombinators.UnaryParsers
{
    public sealed class SequenceParser<T, TOut> : Parser<T, TOut>
    {
        private readonly T[] sequence;
        private readonly IEqualityComparer<T> equalityComparer;

        public SequenceParser(IEnumerable<T> sequenceToFind, IEqualityComparer<T> equalityComparer)
        {
            if (sequenceToFind == null)
            {
                throw new ArgumentNullException(nameof(sequenceToFind));
            }

            this.sequence = sequenceToFind.ToArray();
            this.equalityComparer = equalityComparer ?? throw new ArgumentNullException(nameof(equalityComparer));
        }

        public SequenceParser(IEnumerable<T> sequenceToFind)
            : this(sequenceToFind, EqualityComparer<T>.Default)
        {

        }

        protected override TOut ParseMain(IScanner<T> scanner, out Match<T> match)
        {
            var startOffset = scanner.Offset;
            int i;
            for (i = 0; i < sequence.Length && !scanner.IsAtEnd; i++)
            {
                var current = scanner.Current;

                if (!equalityComparer.Equals(sequence[i], current))
                {
                    break;
                }
                scanner.MoveNext();
            }

            //Reached end without failing?
            match = (i == sequence.Length)
                  ? new Match<T>(scanner, startOffset, sequence.Length)
                  : new Match<T>(scanner, "Failed to match the sequence.");

            return default;
        }
    }
}
