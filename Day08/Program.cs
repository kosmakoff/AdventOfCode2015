using System;
using System.IO;
using System.Linq;
using System.Text;
using static Common.Utils;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 08");

            var inputs = File.ReadAllLines("Input.txt");

            var answer1 = inputs
                .Select(s => (Original: s, Unescaped: UnescapeString(s)))
                .Select(a => a.Original.Length - a.Unescaped.Length)
                .Sum();

            var answer2 = inputs
                .Select(s => (Original: s, Escaped: EscapeString(s)))
                .Select(a => a.Escaped.Length - a.Original.Length)
                .Sum();

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static string UnescapeString(string inputString)
        {
            var sb = new StringBuilder();

            using (var stringReader = new StringReader(inputString))
            {
                var openingQuote = ReadChar(stringReader);
                if (openingQuote != '"')
                    throw new Exception("Unexpected character where opening quote was expected");

                bool advance = true;
                while (advance)
                {
                    var c = ReadChar(stringReader);
                    switch (c)
                    {
                        case '"':
                            advance = false;
                            break;
                        case '\\':
                            var nextChar = ReadChar(stringReader);
                            switch (nextChar)
                            {
                                case '\\':
                                case '"':
                                    sb.Append(nextChar);
                                    break;
                                case 'x':
                                    var nextChar1 = ReadChar(stringReader);
                                    var nextChar2 = ReadChar(stringReader);
                                    if (!IsHexDigit(nextChar1) || !IsHexDigit(nextChar2))
                                        throw new Exception("Non-hexadecimal char found");
                                    var oneByte = Convert.ToByte($"{nextChar1}{nextChar2}", 16);
                                    sb.Append((char) oneByte);
                                    break;
                                default:
                                    throw new Exception("Unexpected escape sequence");
                                
                            }
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                }

                if (stringReader.Read() != -1)
                    throw new Exception("Line didn't end after parsing finished");
            }

            return sb.ToString();
        }

        private static string EscapeString(string inputString)
        {
            var sb = new StringBuilder();

            sb.Append('"');

            foreach (var c in inputString)
            {
                switch (c)
                {
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            sb.Append('"');

            return sb.ToString();
        }

        private static bool IsHexDigit(char c)
        {
            return c >= 'a' && c <= 'f' || c >= '0' && c <= '9';
        }

        private static char ReadChar(TextReader reader)
        {
            var c = reader.Read();
            if (c == -1)
                throw new Exception("No more chars found.");

            return (char) c;
        }
    }
}
