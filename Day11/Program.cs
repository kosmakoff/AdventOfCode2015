using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Common.Utils;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 11");

            const string input = "vzbxkghb";

            var passwords = CalculateAnswers(input);

            var answer1 = passwords[0];
            var answer2 = passwords[1];

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static string[] CalculateAnswers(string input)
        {
            return EnumerateStringStarting(input)
                .Where(IsValidPassword)
                .Take(2)
                .ToArray();
        }

        private static readonly Regex TwoNonOverlappingPairsRegex = new Regex(@"(?<l1>\w)\1.*(?<l2>\w)\2", RegexOptions.Compiled);

        private static bool IsValidPassword(string password)
        {
            if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
                return false;

            var match = TwoNonOverlappingPairsRegex.Match(password);
            if (match.Success && match.Groups["l1"].Value == match.Groups["l2"].Value || !match.Success)
                return false;

            if (!HasThreeSuccessiveLetters(password))
                return false;

            return true;
        }

        private static bool HasThreeSuccessiveLetters(string password)
        {
            for (int i = 0; i < password.Length - 3; i++)
            {
                if (password[i + 1] - password[i] == 1 && password[i + 2] - password[i] == 2)
                    return true;
            }

            return false;
        }

        private static IEnumerable<string> EnumerateStringStarting(string start)
        {
            yield return start;

            var bytes = start.Select(c => c - 'a').ToArray();
            var length = bytes.Length;

            while (true)
            {
                bytes[length - 1]++;
                for (int index = length - 1; index >= 0; index--)
                {
                    if (bytes[index] > 25)
                    {
                        bytes[index] = 0;
                        bytes[index - 1]++;
                    }
                    else
                    {
                        break;
                    }
                }

                yield return Encoding.ASCII.GetString(bytes.Select(b => (byte)('a' + b)).ToArray());
            }
        }
    }
}
