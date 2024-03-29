﻿using System;
using System.Collections.Generic;

namespace Nintenlord.IO.Scanners
{
    public sealed class StoringScanner<T> : IStoringScanner<T>
    {
        private readonly IScanner<T> nonStoringScanner;
        private readonly List<T> generatedItems;
        private readonly long startOffset;
        private long offset;

        public StoringScanner(IScanner<T> scanner)
        {
            nonStoringScanner = scanner;
            startOffset = scanner.Offset;
            offset = scanner.Offset;
            generatedItems = new List<T>();
        }

        public T this[long offset]
        {
            get
            {

                if (!TryGetItem(offset, out T result))
                {
                    throw new InvalidOperationException();
                }

                return result;
            }
        }

        public bool IsStored(long offset)
        {
            return IsStored(offset, 1);
        }

        public bool IsStored(long offset, long length)
        {
            return startOffset <= offset && offset + length <= startOffset + generatedItems.Count;
        }

        public bool IsAtEnd
        {
            get;
            private set;
        }

        public long Offset
        {
            get => offset;
            set
            {
                offset = value;
                var result = GenerateItemsToOffset(offset);
                this.IsAtEnd = !result;
            }
        }

        public bool CanSeek => true;

        public T Current
        {
            get
            {

                if (!TryGetItem(offset, out T result))
                {
                    throw new InvalidOperationException();
                }

                return result;
            }
        }

        public bool MoveNext()
        {
            offset++;
            var result = GenerateItemsToOffset(offset);
            this.IsAtEnd = !result;
            return result;
        }

        private bool TryGetItem(long offset, out T result)
        {
            if (GenerateItemsToOffset(offset))
            {
                result = generatedItems[(int)(offset - startOffset)];
                return true;
            }
            else
            {
                result = default;
                return false;
            }
            //GenerateItemsToOffset(offset);

            //if (offset < generatedItems.Count)
            //{
            //    result = generatedItems[offset];
            //    return true;
            //}
            //else
            //{
            //    result = default(T);
            //    return false;
            //}
        }

        private bool GenerateItemsToOffset(long offset)
        {
            while (startOffset + generatedItems.Count <= offset && !this.nonStoringScanner.IsAtEnd)
            {
                this.generatedItems.Add(nonStoringScanner.Current);

                nonStoringScanner.MoveNext();
            }

            return startOffset + generatedItems.Count > offset;
        }
    }
}
