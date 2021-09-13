using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.Collections
{
    public static class CombinedHelpers
    {
        public static TValue GetValue<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            if (scopes is null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

            TValue result = default(TValue);

            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.TryGetValue(kay, out result))
                {
                    break;
                }
            }
            return result;
        }

        public static bool ContainsKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            if (scopes is null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

            return scopes.Any(item => item.ContainsKey(kay));
        }

        public static bool TryGetKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay, out TValue value)
        {
            if (scopes is null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

            bool result = false;
            value = default(TValue);

            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.TryGetValue(kay, out value))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
