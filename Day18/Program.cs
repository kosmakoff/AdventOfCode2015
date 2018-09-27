using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using static Common.Utils;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 18");

            var input = File.ReadAllText("Input.txt")
                .Where(c => c == '#' || c == '.')
                .Select(c => c == '#')
                .ToArray();

            var answer1 = CalculateAnswer1(input);
            var answer2 = CalculateAnswer2(input);

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(bool[] input)
        {
            var bitGrid = new BitGrid(100, 100, input);

            for (int step = 0; step < 100; step++)
            {
                bitGrid = MutateBitGrid1(bitGrid);
            }

            var answer = bitGrid.GetLightsOnCount();
            return answer;
        }

        private static int CalculateAnswer2(bool[] input)
        {
            var bitGrid = new BitGrid(100, 100, input);

            ForceCornersToOnState(bitGrid);
            for (int step = 0; step < 100; step++)
            {
                bitGrid = MutateBitGrid2(bitGrid);
                ForceCornersToOnState(bitGrid);
            }

            var answer = bitGrid.GetLightsOnCount();
            return answer;
        }

        private static BitGrid MutateBitGrid1(BitGrid bitGrid)
        {
            var newBitGrid = new BitGrid(bitGrid);

            for (int x = 0; x < bitGrid.Width; x++)
            for (int y = 0; y < bitGrid.Height; y++)
            {
                var neighborsLit = EnumerateNeighbors(x, y, bitGrid.Width, bitGrid.Height).Select(a => bitGrid[a.X, a.Y]).Count(b => b);
                
                if (newBitGrid[x, y] && !(neighborsLit == 2 || neighborsLit == 3))
                {
                    newBitGrid[x, y] = false;
                    continue;
                }

                if (!newBitGrid[x, y] && neighborsLit == 3)
                {
                    newBitGrid[x, y] = true;
                }
            }

            return newBitGrid;
        }

        private static BitGrid MutateBitGrid2(BitGrid bitGrid)
        {
            var newBitGrid = new BitGrid(bitGrid);

            for (int x = 0; x < bitGrid.Width; x++)
            for (int y = 0; y < bitGrid.Height; y++)
            {
                var neighborsLit = EnumerateNeighbors(x, y, bitGrid.Width, bitGrid.Height).Select(a => bitGrid[a.X, a.Y]).Count(b => b);
                
                if (newBitGrid[x, y] && !(neighborsLit == 2 || neighborsLit == 3))
                {
                    newBitGrid[x, y] = false;
                    continue;
                }

                if (!newBitGrid[x, y] && neighborsLit == 3)
                {
                    newBitGrid[x, y] = true;
                }
            }

            return newBitGrid;
        }

        private static void ForceCornersToOnState(BitGrid bitGrid)
        {
            bitGrid[0, 0] = true;
            bitGrid[0, bitGrid.Height - 1] = true;
            bitGrid[bitGrid.Width - 1, 0] = true;
            bitGrid[bitGrid.Width - 1, bitGrid.Height - 1] = true;
        }

        private static IEnumerable<(int X, int Y)> EnumerateNeighbors(int x, int y, int width, int height)
        {
            if (x > 0 && y > 0)
                yield return (x - 1, y - 1);

            if (y > 0)
                yield return (x, y - 1);

            if (x < width - 1 && y > 0)
                yield return (x + 1, y - 1);

            if (x > 0)
                yield return (x - 1, y);

            if (x < width - 1)
                yield return (x + 1, y);

            if (x > 0 && y < height - 1)
                yield return (x - 1, y + 1);

            if (y < height - 1)
                yield return (x, y + 1);

            if (x < width - 1 && y < height - 1)
                yield return (x + 1, y + 1);
        }
    }
}
