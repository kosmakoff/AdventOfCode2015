using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Common.Utils;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 12");

            var answer1 = CalculateAnswer1();
            var answer2 = CalculateAnswer2();

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static long CalculateAnswer2()
        {
            long x;

            using (var streamReader = File.OpenText("Input.txt"))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                x = CountNumbers(jsonReader);
            }

            return x;
        }

        private static long CalculateAnswer1()
        {
            var sum = 0L;

            using (var streamReader = File.OpenText("Input.txt"))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.Integer)
                    {
                        sum += (long) jsonReader.Value;
                    }
                }
            }

            return sum;
        }

        private static long CountNumbers(JsonReader jsonReader, bool isObject = false)
        {
            var sum = 0L;

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.String && (string) jsonReader.Value == "red" && isObject)
                {
                    ReadUntilEndObject(jsonReader);

                    return 0;
                }

                if (jsonReader.TokenType == JsonToken.EndObject || jsonReader.TokenType == JsonToken.EndArray)
                    break;

                if (jsonReader.TokenType == JsonToken.Integer)
                {
                    sum += (long) jsonReader.Value;
                    continue;
                }

                if (jsonReader.TokenType == JsonToken.StartObject)
                {
                    sum += CountNumbers(jsonReader, isObject: true);
                    continue;
                }

                if (jsonReader.TokenType == JsonToken.StartArray)
                {
                    sum += CountNumbers(jsonReader);
                }
            }

            return sum;
        }

        private static void ReadUntilEndObject(JsonReader jsonReader)
        {
            var stack = new Stack<JsonToken>();

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.EndObject && !stack.Any())
                    break;

                if (jsonReader.TokenType == JsonToken.StartObject || jsonReader.TokenType == JsonToken.StartArray)
                {
                    stack.Push(jsonReader.TokenType);
                    continue;
                }

                if (jsonReader.TokenType == JsonToken.EndObject &&
                    stack.TryPop(out var endObjectToken) && endObjectToken == JsonToken.StartObject)
                    continue;

                if (jsonReader.TokenType == JsonToken.EndArray &&
                    stack.TryPop(out var endArrayToken) && endArrayToken == JsonToken.StartArray)
                    continue;
            }
        }
    }
}
