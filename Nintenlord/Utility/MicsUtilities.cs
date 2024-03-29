﻿using System;
using System.Collections.Generic;

namespace Nintenlord.Utility
{
    public static class MicsUtilities
    {
        public static void Swap<T>(ref T first, ref T second)
        {
            var temp = first;
            first = second;
            second = temp;
        }

        public static IEnumerable<int> GetRandomIntegers(this Random random, IEnumerable<int> maxValues)
        {
            foreach (var maxValue in maxValues)
            {
                yield return random.Next(maxValue);
            }
        }

        public static T Cast<T>(this object item)
        {
            return (T)item;
        }
    }
}
