using System.IO;
using System.Linq;
using Common.Extensions;
using static Common.Utils;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 17");

            var items = File.ReadAllLines("Input.txt")
                .Select(int.Parse)
                .ToList();

            var answer1 = items
                .Combinations()
                .Count(combination => combination.Sum() == 150);

            var answer2 = items
                .Combinations()
                .Where(combination => combination.Sum() == 150)
                .Select(combination => (Combination: combination, Length: combination.Count))
                .GroupBy(a => a.Length)
                .OrderBy(grp => grp.Key)
                .First().Count();

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }
    }
}
