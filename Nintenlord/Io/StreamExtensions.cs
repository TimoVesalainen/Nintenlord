﻿using System.Collections.Generic;
using System.IO;

namespace Nintenlord.IO
{
    public static class StreamExtensions
    {
        public static bool IsAtEnd(this Stream current)
        {
            return current.Position >= current.Length;
        }

        public static IEnumerable<string> LineEnumerator(this TextReader reader)
        {
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                yield return line;
            }
        }

        public static string GetReaderName(this TextReader reader)
        {
            return reader is StreamReader streamReader
                              ? (streamReader.BaseStream is FileStream
                                     ? ((FileStream)streamReader.BaseStream).Name
                                     : streamReader.BaseStream.GetType().Name)
                              : "Standard input";
        }
    }
}
