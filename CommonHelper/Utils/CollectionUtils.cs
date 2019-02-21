using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using FluentAssertions;

namespace CommonHelper.Utils
{
    public static class CollectionUtils
    {
        public static T GetSingleItem<T>(this IEnumerable<T> source, Func<T, bool> func, string message)
        {
            T item = source.SingleOrDefault(func);
            if (item == null)
            {
                throw new InvalidOperationException(message);
            }

            return item;
        }

        public static IEnumerable<T> Repeat<T>(Func<T> func, int count)
        {
            return Enumerable.Range(0, count).Select(_ => func());
        }

        public static void ValidateMatchRegexes(this IEnumerable<IEnumerable<string>> values, IEnumerable<IEnumerable<string>> patterns)
        {
            foreach (var pair in values.Zip(patterns, Tuple.Create))
            {
                ValidateMatchRegexes(pair.Item1, pair.Item2);
            }
        }

        public static void ValidateMatchRegexes(this IEnumerable<string> values, IEnumerable<string> patterns)
        {
            foreach (var pair in values.Zip(patterns, Tuple.Create))
            {
                pair.Item1.Should().MatchRegex(pair.Item2);
            }
        }

        public static string GetValue(this NameValueCollection collection, string key, bool throwExceptionOnFail = true)
        {
            string value = collection[key];
            if (value == null && throwExceptionOnFail)
            {
                throw new KeyNotFoundException($"'{key}' was not found!");
            }

            return value;
        }
    }
}
