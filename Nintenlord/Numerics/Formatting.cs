using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Numerics
{
    static public class Formatting
    {
        public static string ToHexString(this int i, string prefix)
        {
            return i.ToHexString(prefix, "");
        }
        public static string ToHexString(this int i, string prefix, string postfix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper() + postfix;
        }

        public static string ToBinString(this int i, string postfix)
        {
            return i.ToBinString("", postfix);
        }
        public static string ToBinString(this int i, string prefix, string postfix)
        {
            return prefix + Convert.ToString(i, 2) + postfix;
        }

        public static string ToHexString(this byte i, string prefix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper();
        }


        public static string ToString(this byte[] i, int bytesPerWord)
        {
            StringBuilder result = new StringBuilder();
            for (int j = 0; j < i.Length; j++)
            {
                result.Append(i[j].ToHexString("").PadLeft(2, '0'));
                if (j % bytesPerWord == bytesPerWord - 1)
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
        }
    }
}
