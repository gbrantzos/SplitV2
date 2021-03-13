using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Split.Common.Utilities
{
    public static class Extensions
    {
        // https://stackoverflow.com/a/9314733

        /// <summary>
        /// Get hierarchical structure (parent - child)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="nextItem"></param>
        /// <param name="canContinue"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
                yield return current;
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
            => FromHierarchy(source, nextItem, s => s != null);

        /// <summary>
        /// Get all distinct messages from inner <see cref="Exception"/> hierarchy
        /// </summary>
        /// <param name="x">Root <see cref="Exception"/></param>
        /// <returns><see cref="IEnumerable{sting}"/> with all distinct messages</returns>
        public static IEnumerable<string> GetAllMessages(this Exception x)
            => x.FromHierarchy(x => x.InnerException)
                .Select(x => x.Message)
                .Distinct()
                .ToList();
        
        /// <summary>
        /// Convert any object to JSON.
        /// </summary>
        /// <param name="object">Object to convert</param>
        /// <param name="serializerSettings">Json serializer settings</param>
        /// <returns></returns>
        public static string AsJson(this object @object, JsonSerializerSettings serializerSettings = null)
        {
            var settings = serializerSettings ?? new JsonSerializerSettings();
            return JsonConvert.SerializeObject(@object, settings);
        }
    }
}