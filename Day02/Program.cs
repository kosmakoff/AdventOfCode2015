using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day02");

            var input = File.ReadAllText("Input.txt");

            var answer1 = CalculateAnswer1(input);
            var answer2 = CalculateAnswer2(input);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(string input)
        {
            var separateLines = input.Split("\n");

            return separateLines
                .Select(ParseDimensions)
                .Select(dimensions => CalculateArea(dimensions) + CalculateSmallestSideArea(dimensions))
                .Sum();
        }

        private static int CalculateAnswer2(string input)
        {
            var separateLines = input.Split("\n");

            return separateLines
                .Select(ParseDimensions)
                .Select(dimensions => CalculateSmallestPerimeter(dimensions) + CalculateBowLength(dimensions))
                .Sum();
        }

        private static int CalculateArea((int L, int W, int H) dimensions)
        {
            return dimensions.L * dimensions.W * 2 +
                   dimensions.L * dimensions.H * 2 +
                   dimensions.W * dimensions.H * 2;
        }

        private static int CalculateSmallestSideArea((int L, int W, int H) dimensions)
        {
            var twoSmallest = new[] {dimensions.L, dimensions.W, dimensions.H}.OrderBy(x => x).Take(2).ToArray();
            return twoSmallest[0] * twoSmallest[1];
        }

        private static int CalculateSmallestPerimeter((int L, int W, int H) dimensions)
        {
            var twoSmallest = new[] {dimensions.L, dimensions.W, dimensions.H}.OrderBy(x => x).Take(2).ToArray();
            return (twoSmallest[0] + twoSmallest[1]) * 2;
        }

        private static int CalculateBowLength((int L, int W, int H) dimensions)
        {
            return dimensions.L * dimensions.W * dimensions.H;
        }

        private static (int L, int W, int H) ParseDimensions(string input)
        {
            var parts = input.Split("x").Select(int.Parse).ToArray();
            return (parts[0], parts[1], parts[2]);
        }
    }
}
