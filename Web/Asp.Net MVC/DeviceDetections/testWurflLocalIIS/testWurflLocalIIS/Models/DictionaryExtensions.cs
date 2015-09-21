using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWurflLocalIIS.Models
{
    public static class DictionaryExtensions
    {
        // a utility function for merging a 'target' dictionary into the 'source'
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> target)
        {
            if (target == null || source == null) return;

            foreach (var key in target.Keys)
            {
                if (source.ContainsKey(key))
                {
                    source[key] = target[key];
                }
                else
                {
                    source.Add(key, target[key]);
                }
            }
        }

        public static void Merge(this IDictionary source, IDictionary target)
        {
            if (target == null || source == null) return;

            foreach (var key in target.Keys)
            {
                if (source.Contains(key))
                {
                    source[key] = target[key];
                }
                else
                {
                    source.Add(key, target[key]);
                }
            }
        }

        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            var value = default(TValue);

            if (source.TryGetValue(key, out value))
            {
                return value;
            }

            return default(TValue);
        }

        public static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, string value)
        {
            return source.ContainsKey(key) &&
                source[key].ToString().Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool ContainsValue(this IDictionary source, string key, string value)
        {
            return source.Contains(key) &&
                source[key].ToString().Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}