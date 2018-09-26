using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
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

        public static IEnumerable<IList<TSource>> Permutations<TSource>(this IList<TSource> source)
        {
            return PermutationsImpl(source);
        }

        public static IEnumerable<IList<TSource>> Pairwise<TSource>(this IEnumerable<TSource> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentException("Sequence contained no elements.");

                var current = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    var next = enumerator.Current;
                    yield return new List<TSource> {current, next};
                    current = next;
                }
            }
        }

        public static IEnumerable<IList<TSource>> PermutationsWithoutRotations<TSource>(this IList<TSource> source)
        {
            if (!source.Any())
                throw new ArgumentException("Collection is empty");

            var head = source.First();
            var tail = source.Skip(1).ToList();

            return tail.Permutations()
                .Select(permutation => new[] {head}.Concat(permutation).ToList());
        }

        public static IEnumerable<IList<TSource>> CartesianProduct<TSource>(this IEnumerable<IList<TSource>> source)
        {
            return source
                .Aggregate((IEnumerable<IList<TSource>>) new IList<TSource>[] {new List<TSource>(0)},
                    (accumulator, sequence) =>
                        from accSeq in accumulator
                        from item in sequence
                        select accSeq.Concat(new[] {item}).ToList());
        }

        public static IEnumerable<IList<TSource>> Combinations<TSource>(this IList<TSource> source)
        {
            return Enumerable.Repeat(new[] {true, false}, source.Count)
                .CartesianProduct()
                .Where(bools => bools.Any(b => b))
                .Select(bools => bools
                    .Zip(source, (b, ts) => (B: b, Ts: ts))
                    .Where(a => a.B)
                    .Select(a => a.Ts)
                    .ToList());
        }

        private static IEnumerable<IList<TSource>> PermutationsImpl<TSource>(this IList<TSource> source)
        {
            if (source.Count == 1)
                return new[] {source};

            if (source.Count == 0)
                return new IList<TSource>[0];

            return source
                .Select((item, index) => (Item: item, OtherItems: source.Where((x, i) => i != index).ToList().PermutationsImpl()))
                .SelectMany(a => a.OtherItems.Select(oi => new[] {a.Item}.Concat(oi).ToList()))
                .ToList();
        }
    }
}
