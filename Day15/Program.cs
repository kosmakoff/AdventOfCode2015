using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using static Common.Utils;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 15");

            var input = File.ReadAllLines("Input.txt")
                .Select(ParseIngredient)
                .ToArray();

            var (answer1, answer2) = CalculateAnswer(input);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static (int MaxPoints, int MaxPointsWith500Calories) CalculateAnswer(Ingredient[] ingredients)
        {
            if (ingredients.Length != 4)
                throw new Exception("This code is not good for processing arbitrary length ingredients lists :(");

            var enumeration =
                from a in Enumerable.Range(0, 101)
                from b in Enumerable.Range(0, 101 - a)
                from c in Enumerable.Range(0, 101 - a - b)
                from d in Enumerable.Range(0, 101 - a - b - c)
                where a + b + c + d == 100
                select new[] {a, b, c, d};

            var maxPoints = enumeration
                .Select(arr => arr
                    .Zip(ingredients, (amount, ingredient) => ingredient.Stats * amount)
                    .Aggregate(new IngredientStats(), (acc, stats) => acc + stats, stats => stats.Capped()))
                .Max(stats => stats.Capacity * stats.Durability * stats.Flavor * stats.Texture);

            var maxPointsWith500Calories = enumeration
                .Select(arr => arr
                    .Zip(ingredients, (amount, ingredient) => ingredient.Stats * amount)
                    .Aggregate(new IngredientStats(), (acc, stats) => acc + stats, stats => stats.Capped()))
                .Where(stats => stats.Calories == 500)
                .Max(stats => stats.Capacity * stats.Durability * stats.Flavor * stats.Texture);

            return (maxPoints, maxPointsWith500Calories);
        }

        private static readonly Regex IngredientDescriptionRegex = new Regex(@"(?<name>\w+): capacity (?<capacity>-?\d+), durability (?<durability>-?\d+), flavor (?<flavor>-?\d+), texture (?<texture>-?\d+), calories (?<calories>-?\d+)", RegexOptions.Compiled);

        private static Ingredient ParseIngredient(string ingredient)
        {
            var match = IngredientDescriptionRegex.Match(ingredient);
            if (!match.Success)
                throw new Exception($"Failed to parse ingredient description: {ingredient}");

            var name = match.Groups["name"].Value;
            var capacity = int.Parse(match.Groups["capacity"].Value);
            var durability = int.Parse(match.Groups["durability"].Value);
            var flavor = int.Parse(match.Groups["flavor"].Value);
            var texture = int.Parse(match.Groups["texture"].Value);
            var calories = int.Parse(match.Groups["calories"].Value);

            return new Ingredient(name, new IngredientStats(capacity, durability, flavor, texture, calories));
        }
    }
}
