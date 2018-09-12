using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static Common.Utils;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 05");

            var input = File.ReadAllText("Input.txt");

            var answer1 = CalculateAnswer(input, IsNiceString1);
            var answer2 = CalculateAnswer(input, IsNiceString2);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer(string input, Func<string, bool> nicenessChecker)
        {
            return input
                .Split("\n")
                .Where(nicenessChecker)
                .Count();
        }

        private static readonly Regex ThreeVowelsRegex = new Regex(@".*?[aeiou].*?[aeiou].*?[aeiou].*?", RegexOptions.Compiled);
        private static readonly Regex DoubleLetterRegex = new Regex(@"([a-z])\1", RegexOptions.Compiled);
        private static readonly Regex ForbiddenPatternsRegex = new Regex(@"ab|cd|pq|xy", RegexOptions.Compiled);

        private static bool IsNiceString1(string arg)
        {
            return ThreeVowelsRegex.IsMatch(arg) && DoubleLetterRegex.IsMatch(arg) && !ForbiddenPatternsRegex.IsMatch(arg);
        }

        private static readonly Regex DoubleTwoLettersRegex = new Regex(@"([a-z][a-z]).*\1", RegexOptions.Compiled);
        private static readonly Regex TwoLettersWithOneInBetweenRegex = new Regex(@"([a-z]).\1", RegexOptions.Compiled);

        private static bool IsNiceString2(string arg)
        {
            return DoubleTwoLettersRegex.IsMatch(arg) && TwoLettersWithOneInBetweenRegex.IsMatch(arg);
        }
    }
}
