using System;
using System.Collections.Generic;

namespace Day01.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> Generate<TSource, TResult>(
            this IEnumerable<TSource> source,
            TResult initial,
            Func<TResult, TSource, TResult> generator)
        {
            var current = initial;

            foreach (var sourceItem in source)
            {
                current = generator(current, sourceItem);
                yield return current;
            }
        }
    }
}
