using System;
using System.IO;
using Common;
using Sprache;
using static Common.Utils;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 06");

            var input = File.ReadAllText("Input.txt");

            var commands = LightingGrammar.Program.Parse(input);

            var bitGrid = new BitGrid(1000, 1000);
            var intGrid = new IntGrid(1000, 1000);

            foreach (var changeCommand in commands)
            {
                ChangeData(bitGrid, changeCommand.CoordsFrom, changeCommand.CoordsTo, changeCommand.ChangeMethod);
                ChangeData(bitGrid, changeCommand.CoordsFrom, changeCommand.CoordsTo, changeCommand.ChangeMethod);
            }

            var answer1 = bitGrid.GetLightsOnCount();
            var answer2 = intGrid.GetLightsTotalBrightness();


            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static void ChangeData(BitGrid bitGrid, Coords from, Coords to, ChangeMethod changeMethod)
        {
            for (ushort x = from.X; x <= to.X; x++)
            for (ushort y = from.Y; y <= to.Y; y++)
            {
                switch (changeMethod)
                {
                    case ChangeMethod.TurnOn:
                        bitGrid[x, y] = true;
                        break;
                    case ChangeMethod.TurnOff:
                        bitGrid[x, y] = false;
                        break;
                    case ChangeMethod.Toggle:
                        bitGrid[x, y] = !bitGrid[x, y];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changeMethod), changeMethod, null);
                }
            }
        }
    }
}
