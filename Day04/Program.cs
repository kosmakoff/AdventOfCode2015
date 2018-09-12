using System;
using System.Security.Cryptography;
using System.Text;
using static Common.Utils;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 04");

            const string input = "ckczppom";

            var answer1 = CalculateAnswer(input, CheckFiveLeadingZeroes);
            var answer2 = CalculateAnswer(input, CheckSixLeadingZeroes);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static uint CalculateAnswer(string input, Func<byte[], bool> checkZeroes)
        {
            var md5 = MD5.Create();

            for (uint i = 0; i < int.MaxValue; i++)
            {
                var fullInput = $"{input}{i}";
                var output = md5.ComputeHash(Encoding.ASCII.GetBytes(fullInput));
                if (checkZeroes(output))
                    return i;
            }

            throw new Exception("Failed to find the index");
        }

        private static bool CheckFiveLeadingZeroes(byte[] input)
        {
            return input[0] == 0 && input[1] == 0 && (input[2] & 0xF0) == 0;
        }

        private static bool CheckSixLeadingZeroes(byte[] input)
        {
            return input[0] == 0 && input[1] == 0 && input[2] == 0;
        }
    }
}
