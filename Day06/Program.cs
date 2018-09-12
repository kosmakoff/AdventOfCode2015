using System;
using System.IO;
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
                bitGrid.ChangeData(changeCommand.CoordsFrom, changeCommand.CoordsTo, changeCommand.ChangeMethod);
                intGrid.ChangeData(changeCommand.CoordsFrom, changeCommand.CoordsTo, changeCommand.ChangeMethod);
            }

            var answer1 = bitGrid.GetLightsOnCount();
            var answer2 = intGrid.GetLightsTotalBrightness();


            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }
    }
}
