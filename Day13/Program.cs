using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Extensions;
using static Common.Utils;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 13");

            var input = File.ReadAllLines("Input.txt")
                .Select(ParseRelationString)
                .ToArray();

            var peopleNames = input
                .SelectMany(relation => new[] {relation.NameFrom, relation.NameTo})
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .ToArray();

            var happinessDict = input
                .GroupBy(a => a.NameFrom)
                .ToDictionary(
                    grp => grp.Key,
                    grp => grp.ToDictionary(
                        a => a.NameTo,
                        a => a.HappinessGain,
                        StringComparer.InvariantCultureIgnoreCase),
                    StringComparer.InvariantCultureIgnoreCase);

            var (answer1, answer2) = CalculateAnswer(peopleNames, happinessDict);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static (int TotalHappiness, int AltTotalHappiness) CalculateAnswer(string[] peopleNames, Dictionary<string, Dictionary<string, int>> happinessDict)
        {
            var peoplePermutations = peopleNames.PermutationsWithoutRotations();
            var happiestPermutationAndTheirHappiness = peoplePermutations
                .Select(permutation => (Permutation: permutation, TotalHappiness: CalculatePermutationHappiness(permutation, happinessDict)))
                .OrderByDescending(a => a.TotalHappiness)
                .First();

            var happiestPermutation = happiestPermutationAndTheirHappiness.Permutation;

            var altHappinessPermutations = Enumerable.Range(0, happiestPermutation.Count)
                .Select(index =>
                {
                    var newPermutation = new List<string>(happiestPermutation);
                    newPermutation.Insert(index, "Me");
                    return (Permutation: newPermutation, TotalHappiness: CalculatePermutationHappiness(newPermutation, happinessDict));
                })
                .OrderByDescending(a => a.TotalHappiness)
                .First();

            return (happiestPermutationAndTheirHappiness.TotalHappiness, altHappinessPermutations.TotalHappiness);
        }

        private static int CalculatePermutationHappiness(IList<string> permutation, Dictionary<string, Dictionary<string, int>> happinessDict)
        {
            return permutation
                .Select((name, index) =>
                {
                    var leftNeighborName = permutation[(index + permutation.Count - 1) % permutation.Count];
                    var rightNeighborName = permutation[(index + 1) % permutation.Count];

                    var leftGain = happinessDict.TryGetValue(name, out var leftNeighborDict)
                        ? (leftNeighborDict.TryGetValue(leftNeighborName, out var valueLeft) ? valueLeft : 0)
                        : 0;

                    var rightGain = happinessDict.TryGetValue(name, out var rightNeighborDict)
                        ? (rightNeighborDict.TryGetValue(rightNeighborName, out var valueRight) ? valueRight : 0)
                        : 0;

                    return leftGain + rightGain;
                })
                .Sum();
        }


        private static readonly Regex RelationRegex = new Regex(@"(?<nameTo>\w+) would (?<change>gain|lose) (?<amount>\d+) happiness units by sitting next to (?<nameFrom>\w+)\.", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static (string NameFrom, string NameTo, int HappinessGain) ParseRelationString(string relationString)
        {
            var match = RelationRegex.Match(relationString);
            if (!match.Success)
                throw new Exception("Relation is not correctly formatted, obviously, or regex is bad");

            var nameFrom = match.Groups["nameFrom"].Value;
            var nameTo = match.Groups["nameTo"].Value;
            var happinessGain = int.Parse(match.Groups["amount"].Value);
            if (match.Groups["change"].Value == "lose")
                happinessGain = -happinessGain;

            return (nameFrom, nameTo, happinessGain);
        }
    }
}
