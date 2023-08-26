using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Nintenlord.Numerics
{
    static public class Formatting
    {
        public static string ToBinString<TNumber>(this TNumber number, string postfix)
            where TNumber : IBinaryInteger<TNumber>
        {
            return number.ToBinString("", postfix);
        }

        public static string ToBinString<TNumber>(this TNumber number, string prefix = "", string postfix = "b")
            where TNumber : IBinaryInteger<TNumber>
        {
            return prefix + number.ToString("B", CultureInfo.InvariantCulture) + postfix;
        }

        public static string ToHexString<TNumber>(this TNumber number, string prefix)
            where TNumber : IBinaryInteger<TNumber>
        {
            return number.ToHexString(prefix, "");
        }

        public static string ToHexString<TNumber>(this TNumber number, string prefix, string postfix)
            where TNumber : IBinaryInteger<TNumber>
        {
            return prefix + number.ToString("X", CultureInfo.InvariantCulture) + postfix;
        }

        public static string ToString<TNumber>(this TNumber[] numbers, int numbersPerWord)
            where TNumber : IBinaryInteger<TNumber>
        {
            var separators = Enumerable.Range(0, numbers.Length).Select(j => j % numbersPerWord == numbersPerWord - 1 ? " " : "");

            return numbers.ToString(separators);
        }

        public static string ToString<TNumber>(this TNumber[] numbers, IEnumerable<string> separators)
            where TNumber : IBinaryInteger<TNumber>
        {
            var length = TNumber.PopCount(TNumber.AllBitsSet);
            var lengthBase16 = length >> 4;
            if (lengthBase16 << 4 != length)
            {
                lengthBase16++;
            }
            var lengthInt = int.CreateChecked(lengthBase16);

            var result = new StringBuilder((numbers.Length + 1) * lengthInt);

            var t = numbers
                .Select(number => number.ToHexString("").PadLeft(lengthInt, '0'))
                .Zip(separators, (number, sep) => new[] { number, sep }).SelectMany(x => x)
                .SkipLast(1);

            foreach (var text in t)
            {
                result.Append(text);
            }

            return result.ToString();
        }

    }
}
