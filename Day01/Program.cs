using System;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day01");

            var input = File.ReadAllText("Input.txt");
            
            var answer1 = CalculateAnswer1(input);
            var answer2 = CalculateAnswer2(input);


            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(string input)
        {
            return input.Select(ParenToInt).Sum();
        }

        private static int CalculateAnswer2(string input)
        {
            int index = 1, level = 0;

            foreach (var change in input)
            {
                level += ParenToInt(change);
                if (level == -1)
                    break;

                index++;
            }

            return index;
        }

        private static int ParenToInt(char paren)
        {
            switch (paren)
            {
                case '(':
                    return 1;
                case ')':
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
