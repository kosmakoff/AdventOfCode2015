using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 03");

            var input = File.ReadAllText("Input.txt");

            var answer1 = CalculateAnswer1(input);
            var answer2 = CalculateAnswer2(input);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(string input)
        {
            return input
                .Select(ParseDirection)
                .Aggregate(
                    (Set: new HashSet<Coords> {new Coords(0, 0)}, Coords: new Coords(0, 0)),
                    (data, direction) =>
                    {
                        var newCoords = data.Coords + direction;
                        data.Set.Add(newCoords);
                        return (Set: data.Set, Coords: newCoords);
                    },
                    data => data.Set)
                .Count;
        }

        private static int CalculateAnswer2(string input)
        {
            return input
                .Select((@char, index) => (Direction: ParseDirection(@char), IsFirst: index % 2 == 0))
                .Aggregate(
                    (Set: new HashSet<Coords> {new Coords(0, 0)}, Coords1: new Coords(0, 0), Coords2: new Coords(0, 0)),
                    (data, direction) =>
                    {
                        var newCoords1 = direction.IsFirst ? data.Coords1 + direction.Direction : data.Coords1;
                        var newCoords2 = !direction.IsFirst ? data.Coords2 + direction.Direction : data.Coords2;
                        data.Set.Add(newCoords1);
                        data.Set.Add(newCoords2);
                        return (Set: data.Set, Coords1: newCoords1, Coords2: newCoords2);
                    },
                    data => data.Set
                )
                .Count;
        }

        private static Direction ParseDirection(char direction)
        {
            switch (direction)
            {
                case '^':
                    return Direction.Up;
                case 'v':
                    return Direction.Down;
                case '<':
                    return Direction.Left;
                case '>':
                    return Direction.Right;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
