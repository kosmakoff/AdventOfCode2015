using System;
using System.IO;
using System.Linq;
using System.Text;
using static Common.Utils;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 10");

            const string input = "1113222113";

            var answer1 = CalculateAnswer1(input);
            var answer2 = CalculateAnswer2(input);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(string input) => CalculateAnswer(input, 40);
        
        private static int CalculateAnswer2(string input) => CalculateAnswer(input, 50);

        private static int CalculateAnswer(string input, int iterations)
        {
            return Enumerable.Range(0, iterations)
                .Aggregate(input, (s, i) => Transform(s))
                .Length;
        }

        private static string Transform(string input)
        {
            var sb = new StringBuilder();

            var index = 0;

            while (index < input.Length)
            {
                var count = 0;

                var charToRepeat = input[index];

                do
                {
                    index++;
                    count++;
                } while (index < input.Length && charToRepeat == input[index]);

                sb.Append(count);
                sb.Append(charToRepeat);
            }

            return sb.ToString();
        }
    }
}
