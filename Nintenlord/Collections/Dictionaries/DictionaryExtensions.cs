using Nintenlord.Collections.DataChange;
using System;
using System.Collections.Generic;

namespace Nintenlord.Collections.Dictionaries
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            TKey key, TValue def = default)
        {
            if (!dict.TryGetValue(key, out TValue value))
            {
                value = def;
            }
            return value;
        }

        public static TValue GetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            TKey key,
            TValue defaultVal = default)
        {
            return dict.TryGetValue(key, out TValue val) ? val : defaultVal;
        }


        public static IndexOverlay GetOverlay<T>(this IDictionary<int, T> dict, Func<T, int> measurement)
        {
            IndexOverlay result = new IndexOverlay();

            foreach (var item in dict)
            {
                int length = measurement(item.Value);
                result.AddIndexes(item.Key, length);
            }

            return result;
        }

        public static bool CanFit<T>(this IDictionary<int, T> dict, Func<T, int> measurement,
            int index, T item)
        {
            int lastIndex = index + measurement(item);

            for (int i = index; i < lastIndex; i++)
            {
                if (dict.ContainsKey(i))
                {
                    return false;
                }
            }
            for (int i = index - 1; i >= 0; i--)
            {
                if (dict.TryGetValue(i, out T oldItem) && i + measurement(oldItem) > index)
                {
                    return false;
                }
            }

            return true;
        }

        public static void AddAll<TKey, Tvalue>(this IDictionary<TKey, Tvalue> a,
            IEnumerable<KeyValuePair<TKey, Tvalue>> values)
        {
            foreach (var item in values)
            {
                a.Add(item.Key, item.Value);
            }
        }

        public static TValue GetOldOrSetNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (!dict.TryGetValue(key, out TValue value))
            {
                value = new TValue();
                dict[key] = value;
            }
            return value;
        }
    }
}
