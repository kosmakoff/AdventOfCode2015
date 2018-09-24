using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 16");

            var input = File.ReadAllLines("Input.txt")
                .Select(SueData.Parse)
                .ToArray();

            var searchData = File.ReadAllLines("SearchData.txt")
                .Select(x =>
                {
                    var parts = x.Split(": ");
                    var name = parts[0];
                    var count = int.Parse(parts[1]);
                    return (Name: name, Count: count);
                })
                .ToDictionary(a => a.Name, a => a.Count);

            var answer1 = CalculateAnswer(input, searchData, Answer1Matcher);
            var answer2 = CalculateAnswer(input, searchData, Answer2Matcher);
            
            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static string CalculateAnswer(
            IEnumerable<SueData> input,
            Dictionary<string, int> searchData,
            Func<KeyValuePair<string, int>, int, bool> matcher)
        {
            var matchingInputs = input
                .Where(inputItem => searchData.All(sdKvp =>
                    !inputItem.Data.TryGetValue(sdKvp.Key, out var value) || matcher(sdKvp, value)));

            return matchingInputs.Single().Id;
        }
        
        private static bool Answer1Matcher(KeyValuePair<string, int> kvp, int value)
        {
            return kvp.Value == value;
        }

        private static bool Answer2Matcher(KeyValuePair<string, int> kvp, int value)
        {
            switch (kvp.Key)
            {
                    case "cats":
                    case "trees":
                        return kvp.Value <= value;

                    case "pomeranians":
                    case "goldfish":
                        return kvp.Value > value;

                    default:
                        return kvp.Value == value;
            }
        }
    }
}
